#!/usr/bin/env bash
# One-time server setup for PrepTM staging environment.
# Run once on the Ubuntu server as your deploy user (not root).
#   bash deploy/scripts/server-setup.sh
set -euo pipefail

DEPLOY_USER=$(whoami)
HOME_DIR=$HOME

echo "──────────────────────────────────────────────"
echo " PrepTM — Server Setup"
echo " User : $DEPLOY_USER"
echo " Home : $HOME_DIR"
echo "──────────────────────────────────────────────"

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

# ── 2. Node.js 20 ────────────────────────────────────────────────────────────
echo ""
echo "[2/7] Checking Node.js..."
if command -v node &>/dev/null; then
    echo "    Node $(node -v) already installed — skipping."
else
    echo "    Installing Node.js 20..."
    curl -fsSL https://deb.nodesource.com/setup_20.x | sudo -E bash -
    sudo apt-get install -y nodejs
    echo "    Node $(node -v) installed."
fi

# ── 3. PM2 ───────────────────────────────────────────────────────────────────
echo ""
echo "[3/7] Checking PM2..."
if command -v pm2 &>/dev/null; then
    echo "    PM2 $(pm2 -v) already installed — skipping."
else
    echo "    Installing PM2..."
    sudo npm install -g pm2
    pm2 startup systemd -u "$DEPLOY_USER" --hp "$HOME_DIR"
    sudo systemctl enable "pm2-$DEPLOY_USER"
    echo "    PM2 installed and startup configured."
fi

# ── 4. Certbot ───────────────────────────────────────────────────────────────
echo ""
echo "[4/7] Checking Certbot..."
if command -v certbot &>/dev/null; then
    echo "    Certbot already installed — skipping."
else
    echo "    Installing Certbot..."
    sudo apt-get update -qq
    sudo apt-get install -y certbot python3-certbot-nginx
    echo "    Certbot installed."
fi

# ── 5. Nginx catch-all self-signed cert ──────────────────────────────────────
echo ""
echo "[5/7] Nginx catch-all certificate..."
if [ -f /etc/ssl/certs/nginx-catch-all.crt ]; then
    echo "    Already exists — skipping."
else
    sudo openssl req -x509 -nodes -newkey rsa:2048 -days 3650 \
        -keyout /etc/ssl/private/nginx-catch-all.key \
        -out    /etc/ssl/certs/nginx-catch-all.crt  \
        -subj   "/CN=catch-all" 2>/dev/null
    echo "    Created."
fi

# ── 6. Sudoers for Nginx reload and service restarts ─────────────────────────
echo ""
echo "[6/7] Sudoers for deploy user..."
SUDOERS_FILE="/etc/sudoers.d/preptm-deploy"
if [ -f "$SUDOERS_FILE" ]; then
    echo "    Already configured — skipping."
else
    sudo tee "$SUDOERS_FILE" > /dev/null <<EOF
# Allow the deploy user to control Nginx and PrepTM systemd services
# without a password (required for GitHub Actions CI/CD).
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl reload nginx
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl start preptm-*
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl stop preptm-*
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl restart preptm-*
EOF
    sudo chmod 0440 "$SUDOERS_FILE"
    echo "    Sudoers entry written to $SUDOERS_FILE."
fi

# ── 7. SSH authorized key check ──────────────────────────────────────────────
echo ""
echo "[7/7] SSH authorized_keys..."
if [ ! -f "$HOME_DIR/.ssh/authorized_keys" ]; then
    mkdir -p "$HOME_DIR/.ssh"
    chmod 700 "$HOME_DIR/.ssh"
    touch "$HOME_DIR/.ssh/authorized_keys"
    chmod 600 "$HOME_DIR/.ssh/authorized_keys"
    echo "    Created empty authorized_keys — add your deploy public key to it."
else
    KEYS=$(wc -l < "$HOME_DIR/.ssh/authorized_keys")
    echo "    $KEYS key(s) already present."
fi

# ── Next steps ───────────────────────────────────────────────────────────────
cat <<'NEXT'

══════════════════════════════════════════════════════════════════
 MANUAL STEPS — complete these in order
══════════════════════════════════════════════════════════════════

1. Add your deploy SSH public key to ~/.ssh/authorized_keys

2. Install Nginx site configs:
     sudo cp deploy/nginx/00-catch-all.conf /etc/nginx/sites-available/00-catch-all
     sudo cp deploy/nginx/stageui.preptm.com.conf /etc/nginx/sites-available/stageui.preptm.com
     sudo cp deploy/nginx/stageadmin.preptm.com.conf /etc/nginx/sites-available/stageadmin.preptm.com
     sudo ln -sf /etc/nginx/sites-available/00-catch-all     /etc/nginx/sites-enabled/
     sudo ln -sf /etc/nginx/sites-available/stageui.preptm.com   /etc/nginx/sites-enabled/
     sudo ln -sf /etc/nginx/sites-available/stageadmin.preptm.com /etc/nginx/sites-enabled/
     sudo rm -f /etc/nginx/sites-enabled/default

3. Point DNS for stageui.preptm.com and stageadmin.preptm.com to this server IP,
   then get SSL certificates:
     sudo certbot --nginx -d stageui.preptm.com
     sudo certbot --nginx -d stageadmin.preptm.com

4. Test and reload Nginx:
     sudo nginx -t && sudo systemctl reload nginx

5. Add these secrets to your GitHub repo
   (Settings → Secrets → Actions → New repository secret):
     DEPLOY_SSH_KEY  — private key matching the public key in authorized_keys
     DEPLOY_HOST     — server IP or hostname
     DEPLOY_USER     — SSH username (ubuntu / your username)

6. After the first CI/CD run deploys the frontend, start PM2:
     cd ~
     pm2 start ecosystem.config.cjs
     pm2 save

7. Verify the backend systemd service names match the workflow matrix.
   Check existing services:
     systemctl list-units --type=service | grep preptm
   Update the `systemd:` fields in deploy-stage-backend.yml to match.

8. For each backend service, the appsettings.json on the server is NOT
   overwritten by CI/CD (intentional). Place the staging appsettings.json
   manually at:
     ~/preptm/Backend/Stage/<ServiceName>/appsettings.json

══════════════════════════════════════════════════════════════════
NEXT
