// PrepTM — Single-port proxy server (EN + HI on one port)
// Placed at the root of each deployed environment folder:
//   ~/preptm/FrontEnd/Prod/proxy-server.mjs   → PORT 4001
//   ~/preptm/FrontEnd/Stage/proxy-server.mjs  → PORT 4003
//
// PM2 runs this file. Nginx proxies the port.

import express from 'express';
import { app as serverEn } from './server/en/server.mjs';
import { app as serverHi } from './server/hi/server.mjs';

const port = process.env['PORT'] ?? 4001;

const server = express();

// Hindi locale — must be registered BEFORE English
server.use('/hi', serverHi());

// English locale (default) — catches everything else
server.use('/', serverEn());

server.listen(port, () => {
  console.log(`PrepTM server listening on http://localhost:${port}`);
});
