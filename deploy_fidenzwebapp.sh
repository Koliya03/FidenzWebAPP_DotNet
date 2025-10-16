#!/bin/bash
set -e

APP_NAME="fidenzwebapp"
APP_USER="webuser"
APP_GROUP="webgroup"
APP_DIR="/var/www/$APP_NAME"
PUBLISH_SRC="$HOME/fidenzapp-publish"
SERVICE_FILE="/etc/systemd/system/$APP_NAME.service"
DOMAIN="fdzwebapp.bounceme.net"
PORT=5000

sudo apt update -y
sudo apt install -y nginx dotnet-sdk-8.0 aspnetcore-runtime-8.0 certbot python3-certbot-nginx

if ! id "$APP_USER" &>/dev/null; then
    sudo groupadd -f $APP_GROUP
    sudo useradd -r -g $APP_GROUP $APP_USER
fi

mkdir -p $PUBLISH_SRC
cd ..
cd FidenzWebApp
dotnet restore
dotnet build --configuration Release
dotnet publish -c Release -o $PUBLISH_SRC

sudo mkdir -p $APP_DIR
sudo cp -r $PUBLISH_SRC/* $APP_DIR/
sudo chown -R $APP_USER:$APP_GROUP $APP_DIR
sudo chmod -R 755 $APP_DIR

sudo bash -c "cat > $SERVICE_FILE" <<EOF
[Unit]
Description=Fidenz Web App (.NET 8)
After=network.target

[Service]
WorkingDirectory=$APP_DIR
ExecStart=/usr/bin/dotnet $APP_DIR/FidenzWebApp.dll --urls "http://localhost:$PORT"
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=$APP_NAME
User=$APP_USER
Group=$APP_GROUP
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
EOF

sudo systemctl daemon-reload
sudo systemctl enable $APP_NAME
sudo systemctl restart $APP_NAME

NGINX_CONF="/etc/nginx/sites-available/$APP_NAME.conf"
sudo bash -c "cat > $NGINX_CONF" <<EOF
server {
    listen 80;
    server_name $DOMAIN;

    location / {
        proxy_pass http://localhost:$PORT;
        proxy_http_version 1.1;
        proxy_set_header Upgrade \$http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host \$host;
        proxy_cache_bypass \$http_upgrade;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
    }
}
EOF

sudo ln -sf $NGINX_CONF /etc/nginx/sites-enabled/
sudo nginx -t && sudo systemctl restart nginx
sudo certbot --nginx -d $DOMAIN --non-interactive --agree-tos -m admin@$DOMAIN || true

#LOG_DIR="/var/log/$APP_NAME"
#ARCHIVE_DIR="/var/log/$APP_NAME/archive"

#sudo mkdir -p $LOG_DIR
#sudo mkdir -p $ARCHIVE_DIR
#sudo chown -R $APP_USER:$APP_GROUP $LOG_DIR
#sudo chmod -R 750 $LOG_DIR

#sudo bash -c "cat > /etc/cron.daily/${APP_NAME}_log_archive" <<EOF
#!/bin/bash
#find $LOG_DIR -type f -name "*.log" -exec cp {} $ARCHIVE_DIR/ \;
#find $ARCHIVE_DIR -type f -mtime +7 -delete
#EOF

#sudo chmod +x /etc/cron.daily/${APP_NAME}_log_archive
