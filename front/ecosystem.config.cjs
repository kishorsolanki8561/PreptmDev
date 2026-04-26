// PM2 ecosystem config
//
// DEPLOY INSTRUCTIONS:
//   1. Copy this file to your home directory:
//        cp /path/to/deployed/ecosystem.config.cjs ~/ecosystem.config.cjs
//   2. Run PM2 from your home directory (~):
//        cd ~
//        pm2 start ecosystem.config.cjs      # first time
//        pm2 reload ecosystem.config.cjs --update-env   # subsequent updates
//
// NOTE: .cjs extension is required — the deployed folder's package.json has
//       "type":"module" which breaks PM2's require()-based config loader.
//
// FOLDER STRUCTURE ON SERVER (paths relative to ~ ):
//   preptm/FrontEnd/Prod/client/preptm/   ← production build output
//   preptm/FrontEnd/Stage/client/preptm/  ← staging build output

module.exports = {
  apps: [
    // ─── STAGING  (port 4003) ─────────────────────────────────────────
    {
      name: "StageFront",
      script: "./preptm/FrontEnd/Stage/client/preptm/proxy-server.mjs",
      instances: 1,
      exec_mode: "fork",
      env: {
        NODE_ENV: "production",
        PORT: 4003
      },
      error_file: "./preptm/logs/stage-error.log",
      out_file:   "./preptm/logs/stage-out.log",
      log_date_format: "YYYY-MM-DD HH:mm:ss"
    }

  ]
};
