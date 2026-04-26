import { APP_BASE_HREF } from '@angular/common';
import { CommonEngine } from '@angular/ssr/node';
import express from 'express';
import { fileURLToPath } from 'node:url';
import { basename, dirname, join, resolve } from 'node:path';
import { LOCALE_ID } from '@angular/core';
import bootstrap from 'src/main.server';
import { SERVER_RESPONSE } from 'src/app/core/tokens/ssr.tokens';
import { request as httpsRequest } from 'node:https';

// ── SSR page-level cache ─────────────────────────────────────────────────────
// Caches the fully-rendered HTML for anonymous (non-personalised) requests.
// This eliminates the repeated ~3 s API wait on every SSR render, bringing
// TTFB down to < 100 ms for cache hits.
//
// Cache is per-locale (one instance per server.ts execution context).
// TTL: 60 s — short enough to reflect content updates, long enough to
//   absorb traffic bursts and Google crawl storms.
// Max entries: 200 — prevents unbounded memory growth on large sites.
// ────────────────────────────────────────────────────────────────────────────
interface SsrCacheEntry { html: string; expiresAt: number; }
const ssrCache = new Map<string, SsrCacheEntry>();
const SSR_CACHE_TTL_MS  = 60_000;   // 60 seconds
const SSR_CACHE_MAX     = 200;       // max distinct URLs cached per locale

function getSsrCache(key: string): string | null {
  const entry = ssrCache.get(key);
  if (!entry) return null;
  if (Date.now() > entry.expiresAt) { ssrCache.delete(key); return null; }
  return entry.html;
}

function setSsrCache(key: string, html: string): void {
  if (ssrCache.size >= SSR_CACHE_MAX) {
    // Evict the oldest entry (Map preserves insertion order)
    const oldest = ssrCache.keys().next().value;
    if (oldest) ssrCache.delete(oldest);
  }
  ssrCache.set(key, { html, expiresAt: Date.now() + SSR_CACHE_TTL_MS });
}

/** Returns true when the request carries a user session — skip cache for personalised pages. */
function isAuthenticatedRequest(req: express.Request): boolean {
  // JWT sent as Bearer token (used by the Angular app on API calls)
  if (req.headers['authorization']) return true;
  // Cookie-based session (future-proof)
  const cookie = req.headers['cookie'] ?? '';
  if (cookie.includes('auth_token=') || cookie.includes('preptm_session=')) return true;
  return false;
}
// ─────────────────────────────────────────────────────────────────────────────

// The Express app is exported so that it can be used by serverless Functions.
export function app(): express.Express {

  
  const server = express();
  const serverDistFolder = dirname(fileURLToPath(import.meta.url));

  // get the language from the corresponding folder
  const lang = basename(serverDistFolder);

  // set the route for static content and APP_BASE_HREF
  const langPath = `/${lang}/`;

  // Note that the 'browser' folder is located two directories above 'server/{lang}/'
  const browserDistFolder = resolve(serverDistFolder, `../../browser/${lang}`);
  const indexHtml = join(serverDistFolder, 'index.server.html');

  const commonEngine = new CommonEngine({
    // Angular 19 requires an explicit allowlist of hostnames.
    // Without this, CommonEngine silently falls back to empty CSR HTML for every request.
    allowedHosts: [
      'stageui.preptm.com',
      'stage.preptm.com',
      'www.preptm.com',
      'preptm.com',
      'localhost',
      '127.0.0.1',
    ],
  });

  server.set('view engine', 'html');
  server.set('views', browserDistFolder);

  const apiBaseUrl = process.env['API_URL'] ?? 'https://api.preptm.com/api';

  function proxySitemapXml(apiPath: string, res: express.Response): void {
    const url = new URL(`${apiBaseUrl}/front/Dashboard/${apiPath}`);
    httpsRequest(url, (apiRes) => {
      let data = '';
      apiRes.on('data', (chunk: Buffer) => { data += chunk.toString(); });
      apiRes.on('end', () => {
        res.setHeader('Content-Type', 'application/xml');
        res.setHeader('Cache-Control', 'public, max-age=3600');
        res.status(200).send(data);
      });
    }).on('error', () => res.status(500).send('')).end();
  }

  // Sitemap routes — served before static files so they are not cached as HTML
  server.get('/sitemap.xml', (_req, res) => {
    proxySitemapXml(lang === 'hi' ? 'GetSiteMap/hi' : 'GetSiteMap', res);
  });

  // ── URL Normalisation: 301 redirects ──────────────────────────────────────
  // Strip trailing slash (except root "/"), lowercase the path.
  // Runs before static-file serving so duplicate URLs are never cached by CDN.
  server.use((req, res, next) => {
    const rawPath = req.path;

    // Skip root, static assets, sitemaps, and API paths
    if (
      rawPath === '/' ||
      rawPath.startsWith('/assets/') ||
      rawPath.match(/\.(js|css|ico|png|jpg|webp|svg|woff2?|ttf|json|xml|txt)$/i) ||
      rawPath === '/sitemap.xml' ||
      rawPath === '/robots.txt'
    ) {
      return next();
    }

    let normalised = rawPath;

    // 1. Strip trailing slash
    if (normalised.length > 1 && normalised.endsWith('/')) {
      normalised = normalised.slice(0, -1);
    }

    // 2. Lowercase
    const lower = normalised.toLowerCase();
    if (lower !== normalised) {
      normalised = lower;
    }

    if (normalised !== rawPath) {
      const qs = req.url.includes('?') ? req.url.slice(req.url.indexOf('?')) : '';
      return res.redirect(301, normalised + qs);
    }

    return next();
  });
  // ─────────────────────────────────────────────────────────────────────────

  // Example Express Rest API endpoints
  // server.get('/api/**', (req, res) => { });
  // Serve static files from /browser

  // complete the route for static content by concatenating the language
  server.get('**', express.static(browserDistFolder, {
    maxAge: '1y',
    index: false,   // Never serve index.html for directory requests — Angular SSR handles every HTML route
  }));

  // All regular routes use the Angular engine
  server.get('**', (req, res, next) => {

    const { protocol, originalUrl, headers } = req;

    // ── SSR cache: serve cached HTML for anonymous requests ──────────────────
    const cacheable = !isAuthenticatedRequest(req);
    if (cacheable) {
      const cached = getSsrCache(originalUrl);
      if (cached) {
        res.setHeader('X-SSR-Cache', 'HIT');
        return res.send(cached);
      }
    }
    // ─────────────────────────────────────────────────────────────────────────

    return commonEngine
      .render({
        bootstrap: bootstrap,
        documentFilePath: indexHtml,
        url: `${protocol}://${headers.host}${originalUrl}`,
        publicPath: resolve(serverDistFolder, `../../browser/${lang}`),
        providers: [
          { provide: APP_BASE_HREF, useValue: langPath.includes('en') ? '/' : langPath },
          { provide: LOCALE_ID, useValue: lang },
          { provide: SERVER_RESPONSE, useValue: res },
        ],
      })
      .then((html) => {
        // Only cache 200 responses — a component may call serverResponse.status(404)
        // for deleted/inactive posts, and caching that HTML would serve it as 200 on
        // subsequent requests (soft 404 as seen by Google Search Console).
        if (cacheable && res.statusCode === 200) {
          setSsrCache(originalUrl, html);
          res.setHeader('X-SSR-Cache', 'MISS');
        }
        res.send(html);
      })
      .catch((err) => next(err));
  });

  return server;
}

function run(): void {
  const port = process.env['PORT'] || 4000;
  app().listen(port, () => {
    console.log(`Node Express server listening on http://localhost:${port}`);
  });
}

// Only start server when executed directly — NOT when imported by proxy-server.mjs
if (process.argv[1] === fileURLToPath(import.meta.url)) {
  run();
}
