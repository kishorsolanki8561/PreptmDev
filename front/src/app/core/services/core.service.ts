import { Injectable, PLATFORM_ID, Inject, Renderer2 } from '@angular/core';
import { ApiService } from './api.service';
import { MetaData, ddl, ddlLookup } from '../models/core.models';
import { API_ROUTES } from '../api.routes';
import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { Meta, MetaDefinition, Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { shareReplay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CoreService {


  constructor(@Inject(PLATFORM_ID) private platformId: Object,
    private _apiService: ApiService,
    private _title: Title,
    private _meta: Meta,
    @Inject(DOCUMENT) private document: any
  ) { }

  getCurrentLang(): 'en' | 'hi' {
    // Use optional chaining so this is safe on both browser and SSR server
    const href: string = this.document?.location?.href ?? '';
    return href.split('/')[3] === 'hi' ? 'hi' : 'en';
  }

  getGetSearchTag() {
    const href: string = this.document?.location?.href ?? '';
    return this.parseQueryString(href);
  }
  parseQueryString(url: string): any {
    // Extract the query string from the URL
    let queryString = url.split('?')[1];
    if (queryString) {
      queryString = queryString.replaceAll("https://", "").replaceAll("http://", "");
      let pairs = queryString.split('&');

      // Initialize an empty object to store the parameters
      let params: any = {};

      // Loop through each pair and add it to the params object
      pairs.forEach((pair: any) => {
        let [key, value] = pair.split('=');
        params[key] = value;
      });
      return params;
    }
    return null;
    // Split the query string into key-value pairs
  }

  setLang(lang: 'en' | 'hi') {
    if (this.getCurrentLang() != lang) {
      if (lang == 'en') {
        if (isPlatformBrowser(this.platformId)) {
          let urlArr = this.document.location.href.split('/')
          urlArr.splice(3, 1)
          this.document.location.href = urlArr.join('/')
        }
      } else if (lang == 'hi') {
        if (isPlatformBrowser(this.platformId)) {
          let urlArr = this.document.location.href.split('/')
          urlArr.splice(3, 0, 'hi')
          this.document.location.href = urlArr.join('/')
        }
      }
    }
  }


  setLocalStorage(key: string, value: any) {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem(key, JSON.stringify(value));
    }
  }

  getLocalStorage(key: string): any {
    if (isPlatformBrowser(this.platformId)) {
      if (localStorage.getItem(key)) {
        return JSON.parse(localStorage.getItem(key) || '');
      }
    }
  }

  clearLocalStorageVal() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.clear();
    }
  }
  remmoveFromLocalStorage(key: string) {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(key);
    }
  }
  deleteLocalStorageVal(key: string) {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(key);
    }
  }
  checkIsClientSide(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  private readonly _ddlCache = new Map<string, Observable<any>>();
  private readonly _ddlLookupCache = new Map<string, Observable<any>>();

  getDdl(ddlKeys: string): Observable<any> {
    if (!this._ddlCache.has(ddlKeys)) {
      this._ddlCache.set(
        ddlKeys,
        this._apiService.get<ddl>(API_ROUTES.ddl + ddlKeys).pipe(shareReplay(1))
      );
    }
    return this._ddlCache.get(ddlKeys)!;
  }

  GetDDLLookupData(SlugUrl: string = '', LookupType: string = '', LookupTypeId: string = ''): Observable<any> {
    const cacheKey = `${SlugUrl}|${LookupType}|${LookupTypeId}`;
    if (!this._ddlLookupCache.has(cacheKey)) {
      this._ddlLookupCache.set(
        cacheKey,
        this._apiService.get<ddlLookup>(API_ROUTES.getDDLLookupDataBy, { SlugUrl, LookupType, LookupTypeId }).pipe(shareReplay(1))
      );
    }
    return this._ddlLookupCache.get(cacheKey)!;
  }

  jwtDecode(token: string) {
    return JSON.parse(atob(token.split('.')[1]))
  }
  setPageTitle(title: string) {
    this._title.setTitle(title)
  }




  titleCase(str: string): string {
    if (!str) return '';
    return str.replace(/\w\S*/g,
      function (txt) {
        return txt.charAt(0).toUpperCase() +
          txt.substr(1).toLowerCase();
      });
  }

  copyTextToClipboard(text: string) {
    if (isPlatformBrowser(this.platformId)) {
      navigator.clipboard.writeText(text);
    }
  }
  dateString(date: string) {
    let dt = new Date(date)

    const monthNames = ["Jan", "Feb", "Mar", "Apr",
      "May", "Jun", "Jul", "Aug",
      "Sep", "Oct", "Nov", "Dec"];

    const day = dt.getDate();

    const monthIndex = dt.getMonth();
    const monthName = monthNames[monthIndex];

    const year = dt.getFullYear();

    return `${day < 10 ? ('0' + day) : day}-${monthName}-${year}`;
  }

  manageMetaTags(metadataTags: MetaDefinition[], renderer: Renderer2) {

    //remove extra tags
    let newTagsName = metadataTags.map(x => x.property);
    let oldTags = this._meta.getTags('property');

    let notRemovableTagNames = ["viewport", "og:site_name", "robots"]

    for (let i = 0; i < oldTags.length; i++) {
      let oldTagPropertyName = oldTags[i].attributes.getNamedItem('property')?.value || ''
      if (!newTagsName.includes(oldTagPropertyName) && !notRemovableTagNames.includes(oldTagPropertyName)) {
        this._removeMetaTag(oldTagPropertyName);
      }
    }

    for (let i = 0; i < metadataTags.length; i++) {
      let tag = metadataTags[i];

      // add name descripton
      if (tag.property === "description") {
        if (this._isMetaTagExists(tag['property'] || '','name')) {
          this._updateMetaTag(tag,'name')
        } else {
          this._addMetaTag({
            name:tag.property,
            content:tag.content

          } as MetaDefinition);
        }
      }

      if (this._isMetaTagExists(tag['property'] || '')) {
        this._updateMetaTag(tag)
      } else {
        this._addMetaTag(tag);
      }
    }

    this.addCommonTags(renderer);
  }

  addCommonTags(renderer: Renderer2) {
    this.addAlternetLink(renderer);
    // this.addCanonicalTag(renderer);
    this.addUrlTag(renderer);
    this.addLocalTag(renderer);

  }

  private _addMetaTag(tag: MetaDefinition) {
    this._meta.addTag(tag);
  }

  private _updateMetaTag(tag: MetaDefinition,key:string='property') {
    this._meta.updateTag(tag, `${key}='${tag.property}'`)
  }

  private _removeMetaTag(tagName: string) {
    this._meta.removeTag(`property='${tagName}'`);
  }
  private _isMetaTagExists(tagName: string, key: string = 'property'): boolean {
    return !!this._meta?.getTag(`${key}='${tagName}'`);
  }

  addAlternetLink(renderer: Renderer2) {
    const arr = Array.from(this.document.head.children);
    const altLinks = arr.filter((e: any) => e.rel === 'alternate' &&
      (e.hreflang === 'en' || e.hreflang === 'hi' || e.hreflang === 'x-default')) as any[];
    const canonicalEle = arr.find((x: any) => x.rel === 'canonical') as any;

    const linkEltEn = altLinks.find((e: any) => e.hreflang === 'en') as any;
    const linkEltHi = altLinks.find((e: any) => e.hreflang === 'hi') as any;
    const linkEltDefault = altLinks.find((e: any) => e.hreflang === 'x-default') as any;

    // Build absolute canonical URL (strip query string and hash — canonical must be clean)
    const rawHref: string = this.document?.location?.href ?? '';
    const cleanUrl = rawHref.split('?')[0].split('#')[0];

    // Ensure canonical always starts with the production domain (handles localhost / staging)
    const BASE = 'https://www.preptm.com';
    const isHindi = this.getCurrentLang() === 'hi';

    // Extract the path segment after the origin (strip lang prefix for calculations)
    let pathAfterOrigin = cleanUrl.replace(/^https?:\/\/[^/]+/, '') || '/';

    // Derive the two absolute language URLs
    const enUrl = isHindi
      ? `${BASE}${pathAfterOrigin.replace(/^\/hi\//, '/')}` // strip /hi/ prefix
      : `${BASE}${pathAfterOrigin}`;

    const hiUrl = isHindi
      ? `${BASE}${pathAfterOrigin}`                         // current URL already has /hi/
      : `${BASE}/hi${pathAfterOrigin === '/' ? '' : pathAfterOrigin}`;

    // Canonical = current language URL (each page is canonical in its own language)
    const canonicalUrl = isHindi ? hiUrl : enUrl;
    // x-default always points to English
    const defaultUrl = enUrl;

    if (linkEltEn && linkEltHi && linkEltDefault) {
      renderer.setAttribute(linkEltEn, 'href', enUrl);
      renderer.setAttribute(linkEltHi, 'href', hiUrl);
      renderer.setAttribute(linkEltDefault, 'href', defaultUrl);
    } else {
      const elEn = renderer.createElement('link');
      renderer.setAttribute(elEn, 'rel', 'alternate');
      renderer.setAttribute(elEn, 'hreflang', 'en');
      renderer.setAttribute(elEn, 'href', enUrl);
      renderer.appendChild(this.document.head, elEn);

      const elHi = renderer.createElement('link');
      renderer.setAttribute(elHi, 'rel', 'alternate');
      renderer.setAttribute(elHi, 'hreflang', 'hi');
      renderer.setAttribute(elHi, 'href', hiUrl);
      renderer.appendChild(this.document.head, elHi);

      const elDefault = renderer.createElement('link');
      renderer.setAttribute(elDefault, 'rel', 'alternate');
      renderer.setAttribute(elDefault, 'hreflang', 'x-default');
      renderer.setAttribute(elDefault, 'href', defaultUrl);
      renderer.appendChild(this.document.head, elDefault);
    }

    if (canonicalEle) {
      renderer.setAttribute(canonicalEle, 'href', canonicalUrl);
    } else {
      const canEle = renderer.createElement('link');
      renderer.setAttribute(canEle, 'rel', 'canonical');
      renderer.setAttribute(canEle, 'href', canonicalUrl);
      renderer.appendChild(this.document.head, canEle);
    }
  }

  addCanonicalTag(renderer: Renderer2) {

    let arr = Array.from(this.document.head.children);
    let linkElt = arr.find((e: any) => e.rel == "canonical");
    let link = 'https://preptm.com/' + ((this.getCurrentLang() == 'en') ? 'hi/' : '') + this.document.location.href.split('/').splice(3).join('/')
    if (linkElt) {
      //udpate
      renderer.setAttribute(linkElt, 'href', link);
    } else {
      // add
      linkElt = renderer.createElement('link');
      renderer.setAttribute(linkElt, 'rel', 'canonical');
      renderer.setAttribute(linkElt, 'href', link);
      renderer.appendChild(this.document.head, linkElt);
    }

  }


  addUrlTag(renderer: Renderer2) {
    // og:url must be the clean canonical production URL (no query string / hash,
    // and always on www.preptm.com regardless of the environment serving the page)
    const BASE = 'https://www.preptm.com';
    const rawHref: string = this.document?.location?.href ?? '';
    const cleanPath = rawHref.split('?')[0].split('#')[0].replace(/^https?:\/\/[^/]+/, '') || '/';
    const link = `${BASE}${cleanPath}`;

    let arr = Array.from(this.document.head.children);
    let linkElt = arr.find((e: any) => e.getAttribute?.('property') === 'og:url');
    if (linkElt) {
      renderer.setAttribute(linkElt, 'content', link);
    } else {
      linkElt = renderer.createElement('meta');
      renderer.setAttribute(linkElt, 'property', 'og:url');
      renderer.setAttribute(linkElt, 'content', link);
      renderer.appendChild(this.document.head, linkElt);
    }
  }

  addLocalTag(renderer: Renderer2) {
    // adding og:locale
    let arr = Array.from(this.document.head.children);
    let linkElt = arr.find((e: any) => e.property == "og:locale");
    let content = this.getCurrentLang() == 'en' ? "en_IN" : "hi_IN"
    if (linkElt) {
      //udpate
      renderer.setAttribute(linkElt, 'content', content);
    } else {
      // add
      linkElt = renderer.createElement('meta');
      renderer.setAttribute(linkElt, 'property', 'og:locale');
      renderer.setAttribute(linkElt, 'content', content);
      renderer.appendChild(this.document.head, linkElt);
    }

    // adding og:locale:alternate
    arr = Array.from(this.document.head.children);
    linkElt = arr.find((e: any) => e.property == "og:locale:alternate");
    content = this.getCurrentLang() == 'hi' ? "en_IN" : "hi_IN"
    if (linkElt) {
      //udpate
      renderer.setAttribute(linkElt, 'content', content);
    } else {
      // add
      linkElt = renderer.createElement('meta');
      renderer.setAttribute(linkElt, 'property', 'og:locale:alternate');
      renderer.setAttribute(linkElt, 'content', content);
      renderer.appendChild(this.document.head, linkElt);
    }
  }

}



