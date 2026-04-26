import { app as serverEn } from './preptm/server/en/server.mjs'
import { app as serverHi } from './preptm/server/hi/server.mjs'

const express = require("express");
const useragent = require('express-useragent');

function run() {
    // const port = 4003;    // staging
  const port = 4001; // prod
    const server = express();




    // #region <Serve GZIP>

    server.get('*.js', function (req, res, next) {
        if (!req.query['ngsw-cache-bust']) {

            req.url = req.url + '.gz';
            res.set('Content-Encoding', 'gzip');
            res.set('Content-Type', 'text/javascript');
        }
        next();
    });

    server.get('*.css', function (req, res, next) {
        req.url = req.url + '.gz';
        res.set('Content-Encoding', 'gzip');
        res.set('Content-Type', 'text/css');
        next();
    });

    server.get('*.png', function (req, res, next) {
        req.url = req.url + '.gz';
        res.set('Content-Encoding', 'gzip');
        res.set('Content-Type', 'image/png');
        next();
    });
    server.get('*.svg', function (req, res, next) {
        req.url = req.url + '.gz';
        res.set('Content-Encoding', 'gzip');
        res.set('Content-Type', 'image/svg+xml');
        next();
    });
    server.get('*.webp', function (req, res, next) {
        req.url = req.url + '.gz';
        res.set('Content-Encoding', 'gzip');
        res.set('Content-Type', 'image/webp');
        next();
    });

    //#endregion <Serve GZIP>




    server.use('/hi', express.static('public'))
    server.use('/', express.static('public'))

    server.use('/hi',(req, resp, next) => {
        req['isMobile'] = useragent.parse(req.headers['user-agent']).isMobile
        next();
    }, serverHi());

    server.use('/',(req, resp, next) => {
        req['isMobile'] = useragent.parse(req.headers['user-agent']).isMobile
        next();
    }, serverEn());

    server.listen(port, () => {
        console.log(`server listening on localhost:${port}`)
    })
}
run();
