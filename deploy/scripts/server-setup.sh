#!/usr/bin/env bash
# One-time server setup for PrepTM staging environment.
# Run once on your Ubuntu server as your deploy user (NOT root):
#   bash deploy/scripts/server-setup.sh
set -euo pipefail

DEPLOY_USER=$(whoami)
HOME_DIR=$HOME

echo "══════════════════════════════════════════════"
echo " PrepTM — Server Setup"
echo " User : $DEPLOY_USER"
echo " Home : $HOME_DIR"
echo "══════════════════════════════════════════════"

# ── 1. Directory structure ────────────────────────────────────────────────────
echo ""
echo "[1/7] Creating directory structure..."
mkdir -p "$HOME_DIR/preptm/FrontEnd/Stage/client/preptm"
mkdir -p "$HOME_DIR/preptm/FrontEnd/Prod/client/preptm"
mkdir -p "$HOME_DIR/preptm/Admin/Stage"
mkdir -p "$HOME_DIR/preptm/Admin/Prod"
mkdir -p "$HOME_DIR/preptm/Backend/Stage"
mkdir -p "$HOME_DIR/preptm/logs"
echo "    Done."

# ── 2. Node.js 20 (runtime only — no build tools needed) ─────────────────────
echo ""
echo "[2/7] Checking Node.js..."
if command -v node &>/dev/null && [[ "$(node -v)" == v20* ]]; then
    echo "    Node $(node -v) already installed — skipping."
else
    echo "    Installing Node.js 20..."
    curl -fsSL https://deb.nodesource.com/setup_20.x | sudo -E bash -
    sudo apt-get install -y nodejs
    echo "    Node $(node -v) installed."
fi

# ── 3. .NET 6 runtime (not SDK — CI builds, server only runs) ────────────────
echo ""
echo "[3/7] Checking .NET 6 runtime..."
if dotnet --list-runtimes 2>/dev/null | grep -q "Microsoft.NETCore.App 6"; then
    echo "    .NET 6 runtime already installed — skipping."
else
    echo "    Installing .NET 6 runtime..."
    wget -q "https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb"
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    sudo apt-get update -qq
    sudo apt-get install -y aspnetcore-runtime-6.0
    echo "    .NET 6 runtime installed."
fi

# ── 4. PM2 ───────────────────────────────────────────────────────────────────
echo ""
echo "[4/7] Checking PM2..."
if command -v pm2 &>/dev/null; then
    echo "    PM2 $(pm2 -v) already installed — skipping."
else
    echo "    Installing PM2..."
    sudo npm install -g pm2
    pm2 startup systemd -u "$DEPLOY_USER" --hp "$HOME_DIR"
    sudo systemctl enable "pm2-$DEPLOY_USER"
    echo "    PM2 installed and startup configured."
fi

# ── 5. Certbot ───────────────────────────────────────────────────────────────
echo ""
echo "[5/7] Checking Certbot..."
if command -v certbot &>/dev/null; then
    echo "    Certbot already installed — skipping."
else
    echo "    Installing Certbot..."
    sudo apt-get update -qq
    sudo apt-get install -y certbot python3-certbot-nginx
    echo "    Certbot installed."
fi

# ── 6. Nginx catch-all self-signed cert ──────────────────────────────────────
echo ""
echo "[6/7] Nginx catch-all certificate..."
if [ -f /etc/ssl/certs/nginx-catch-all.crt ]; then
    echo "    Already exists — skipping."
else
    sudo openssl req -x509 -nodes -newkey rsa:2048 -days 3650 \
        -keyout /etc/ssl/private/nginx-catch-all.key \
        -out    /etc/ssl/certs/nginx-catch-all.crt  \
        -subj   "/CN=catch-all" 2>/dev/null
    echo "    Created."
fi

# ── 7. Sudoers for service restarts ──────────────────────────────────────────
echo ""
echo "[7/7] Sudoers for deploy user..."
SUDOERS_FILE="/etc/sudoers.d/preptm-deploy"
if [ -f "$SUDOERS_FILE" ]; then
    echo "    Already configured — skipping."
else
    sudo tee "$SUDOERS_FILE" > /dev/null <<EOF
# Allow the deploy user to control Nginx and PrepTM backend services
# without a password (required for GitHub Actions CI/CD over SSH).
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl reload nginx
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl start preptm-*
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl stop preptm-*
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl restart preptm-*
EOF
    sudo chmod 0440 "$SUDOERS_FILE"
    echo "    Written to $SUDOERS_FILE."
fi

# ── Next steps ────────────────────────────────────────────────────────────────
cat <<'NEXT'

══════════════════════════════════════════════════════════════════
 MANUAL STEPS — complete these in order
══════════════════════════════════════════════════════════════════

── A. Add GitHub Actions SSH key ────────────────────────────────

  Generate a dedicated deploy key (on any machine):
    ssh-keygen -t ed25519 -C "github-actions-deploy" -f ~/.ssh/preptm_deploy

  Add the PUBLIC key to your server:
    cat ~/.ssh/preptm_deploy.pub >> ~/.ssh/authorized_keys

  Add the PRIVATE key to GitHub:
    Repo → Settings → Secrets → Actions → New repository secret
      DEPLOY_SSH_KEY  ← contents of ~/.ssh/preptm_deploy (private key)
      DEPLOY_HOST     ← your server IP
      DEPLOY_USER     ← your SSH username

── B. Install Nginx site configs ────────────────────────────────

  sudo cp deploy/nginx/00-catch-all.conf            /etc/nginx/sites-available/00-catch-all
  sudo cp deploy/nginx/stageui.preptm.com.conf      /etc/nginx/sites-available/stageui.preptm.com
  sudo cp deploy/nginx/stageadmin.preptm.com.conf   /etc/nginx/sites-available/stageadmin.preptm.com

  sudo ln -sf /etc/nginx/sites-available/00-catch-all          /etc/nginx/sites-enabled/
  sudo ln -sf /etc/nginx/sites-available/stageui.preptm.com    /etc/nginx/sites-enabled/
  sudo ln -sf /etc/nginx/sites-available/stageadmin.preptm.com /etc/nginx/sites-enabled/
  sudo rm -f /etc/nginx/sites-enabled/default

── C. Point DNS then get SSL certificates ───────────────────────

  (DNS A records must point to this server's IP before running Certbot.)

  sudo certbot --nginx -d stageui.preptm.com
  sudo certbot --nginx -d stageadmin.preptm.com
  sudo nginx -t && sudo systemctl reload nginx

── D. First frontend deployment ─────────────────────────────────

  After the first CI run deploys the frontend files, start PM2:
    cd ~
    pm2 start ecosystem.config.cjs
    pm2 save

── E. Fix backend systemd service names ─────────────────────────

  Check what's on your server:
    systemctl list-units --type=service | grep preptm

  Update the `systemd:` fields in deploy-stage-backend.yml to match.

── F. Backend appsettings.json ──────────────────────────────────

  CI/CD never overwrites appsettings*.json. Place staging credentials
  manually at:
    ~/preptm/Backend/Stage/<ServiceName>/appsettings.json

══════════════════════════════════════════════════════════════════
NEXT
