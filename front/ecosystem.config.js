// PM2 ecosystem config — place this file at ~/ecosystem.config.js on the Ubuntu server
// Run:    pm2 start ecosystem.config.js
// Reload: pm2 reload ecosystem.config.js --update-env

module.exports = {
  apps: [

    // ─── PRODUCTION  (port 4001) ──────────────────────────────────────
    {
      name: "ProdFront",
      script: "./preptm/FrontEnd/Prod/proxy-server.mjs",
      instances: 1,
      exec_mode: "fork",
      env: {
        NODE_ENV: "production",
        PORT: 4001
      },
      error_file: "./preptm/logs/prod-error.log",
      out_file:   "./preptm/logs/prod-out.log",
      log_date_format: "YYYY-MM-DD HH:mm:ss"
    },

    // ─── STAGING  (port 4003) ─────────────────────────────────────────
    {
      name: "StageFront",
      script: "./preptm/FrontEnd/Stage/proxy-server.mjs",
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
