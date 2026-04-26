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
echo "[1/8] Creating directory structure..."
mkdir -p "$HOME_DIR/preptm/FrontEnd/Stage/client/preptm"
mkdir -p "$HOME_DIR/preptm/FrontEnd/Prod/client/preptm"
mkdir -p "$HOME_DIR/preptm/Admin/Stage"
mkdir -p "$HOME_DIR/preptm/Admin/Prod"
mkdir -p "$HOME_DIR/preptm/Backend/Stage"
mkdir -p "$HOME_DIR/preptm/logs"
echo "    Done."

# ── 2. Node.js 20 ────────────────────────────────────────────────────────────
echo ""
echo "[2/8] Checking Node.js..."
if command -v node &>/dev/null && [[ "$(node -v)" == v20* ]]; then
    echo "    Node $(node -v) already installed — skipping."
else
    echo "    Installing Node.js 20..."
    curl -fsSL https://deb.nodesource.com/setup_20.x | sudo -E bash -
    sudo apt-get install -y nodejs
    echo "    Node $(node -v) installed."
fi

# ── 3. .NET 6 ────────────────────────────────────────────────────────────────
echo ""
echo "[3/8] Checking .NET 6..."
if dotnet --list-runtimes 2>/dev/null | grep -q "Microsoft.NETCore.App 6"; then
    echo "    .NET 6 already installed — skipping."
else
    echo "    Installing .NET 6..."
    # Microsoft package feed for Ubuntu
    wget -q https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    sudo apt-get update -qq
    sudo apt-get install -y dotnet-sdk-6.0
    echo "    .NET $(dotnet --version) installed."
fi

# ── 4. PM2 ───────────────────────────────────────────────────────────────────
echo ""
echo "[4/8] Checking PM2..."
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
echo "[5/8] Checking Certbot..."
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
echo "[6/8] Nginx catch-all certificate..."
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
echo "[7/8] Sudoers for deploy user..."
SUDOERS_FILE="/etc/sudoers.d/preptm-deploy"
if [ -f "$SUDOERS_FILE" ]; then
    echo "    Already configured — skipping."
else
    sudo tee "$SUDOERS_FILE" > /dev/null <<EOF
# Allow the deploy user (GitHub Actions self-hosted runner) to control
# Nginx and PrepTM backend services without a password prompt.
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl reload nginx
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl start preptm-*
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl stop preptm-*
$DEPLOY_USER ALL=(ALL) NOPASSWD: /bin/systemctl restart preptm-*
EOF
    sudo chmod 0440 "$SUDOERS_FILE"
    echo "    Written to $SUDOERS_FILE."
fi

# ── 8. GitHub Actions self-hosted runner ─────────────────────────────────────
echo ""
echo "[8/8] GitHub Actions runner..."
RUNNER_DIR="$HOME_DIR/actions-runner"
if [ -d "$RUNNER_DIR" ]; then
    echo "    Runner directory already exists — skipping download."
else
    echo "    Downloading latest runner..."
    mkdir -p "$RUNNER_DIR"
    cd "$RUNNER_DIR"

    # Detect architecture
    ARCH=$(uname -m)
    case $ARCH in
        x86_64)  RUNNER_ARCH="x64"   ;;
        aarch64) RUNNER_ARCH="arm64" ;;
        *)       echo "    Unsupported arch: $ARCH"; exit 1 ;;
    esac

    RUNNER_VERSION=$(curl -s https://api.github.com/repos/actions/runner/releases/latest \
        | grep '"tag_name"' | sed 's/.*"v\([^"]*\)".*/\1/')
    RUNNER_FILE="actions-runner-linux-${RUNNER_ARCH}-${RUNNER_VERSION}.tar.gz"

    curl -fsSL "https://github.com/actions/runner/releases/download/v${RUNNER_VERSION}/${RUNNER_FILE}" \
        -o "$RUNNER_FILE"
    tar xzf "$RUNNER_FILE"
    rm "$RUNNER_FILE"
    echo "    Runner v${RUNNER_VERSION} downloaded to $RUNNER_DIR"
fi

# ── Final instructions ────────────────────────────────────────────────────────
cat <<'NEXT'

══════════════════════════════════════════════════════════════════
 MANUAL STEPS — complete these in order
══════════════════════════════════════════════════════════════════

── A. Register the GitHub Actions runner ─────────────────────────

  1. Open: https://github.com/<your-org>/PreptmDev/settings/actions/runners/new
     (Settings → Actions → Runners → New self-hosted runner → Linux)

  2. Copy the token shown on that page, then run:
       cd ~/actions-runner
       ./config.sh --url https://github.com/<your-org>/PreptmDev --token <TOKEN>
       # Accept all defaults when prompted; set a descriptive name like "preptm-stage"

  3. Install and start as a systemd service (stays running after reboot):
       sudo ./svc.sh install
       sudo ./svc.sh start

  4. Verify it shows as "Idle" in the GitHub UI.

── B. Install Nginx site configs ────────────────────────────────

  sudo cp deploy/nginx/00-catch-all.conf /etc/nginx/sites-available/00-catch-all
  sudo cp deploy/nginx/stageui.preptm.com.conf    /etc/nginx/sites-available/stageui.preptm.com
  sudo cp deploy/nginx/stageadmin.preptm.com.conf /etc/nginx/sites-available/stageadmin.preptm.com

  sudo ln -sf /etc/nginx/sites-available/00-catch-all            /etc/nginx/sites-enabled/
  sudo ln -sf /etc/nginx/sites-available/stageui.preptm.com      /etc/nginx/sites-enabled/
  sudo ln -sf /etc/nginx/sites-available/stageadmin.preptm.com   /etc/nginx/sites-enabled/
  sudo rm -f /etc/nginx/sites-enabled/default

── C. Point DNS then get SSL certificates ───────────────────────

  (DNS A records for stageui.preptm.com and stageadmin.preptm.com
   must point to this server's IP before running Certbot.)

  sudo certbot --nginx -d stageui.preptm.com
  sudo certbot --nginx -d stageadmin.preptm.com
  sudo nginx -t && sudo systemctl reload nginx

── D. First frontend deployment ─────────────────────────────────

  After the first CI run deploys the frontend files, start PM2:
    cd ~
    pm2 start ecosystem.config.cjs
    pm2 save

── E. Backend systemd service names ─────────────────────────────

  The workflow uses names like preptm-gateway, preptm-front-api, etc.
  Check what's actually on your server:
    systemctl list-units --type=service | grep preptm

  Update the `systemd:` fields in deploy-stage-backend.yml to match.

── F. Backend appsettings.json ──────────────────────────────────

  CI/CD never overwrites appsettings*.json (intentional — credentials
  stay on the server). Place staging config manually at:
    ~/preptm/Backend/Stage/<ServiceName>/appsettings.json

══════════════════════════════════════════════════════════════════
NEXT
