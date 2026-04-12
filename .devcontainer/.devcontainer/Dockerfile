FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0
USER root

RUN apt-get update \
    && apt-get install -y curl gpg apt-transport-https ca-certificates gnupg2 unixodbc-dev \
    && curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg \
    && install -o root -g root -m 644 microsoft.gpg /usr/share/keyrings/microsoft-prod.gpg \
    && rm microsoft.gpg \
    && echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/microsoft-prod.gpg] https://packages.microsoft.com/debian/12/prod bookworm main" > /etc/apt/sources.list.d/microsoft-prod.list \
    && apt-get update \
    && apt-get install -y azure-functions-core-tools-4 mssql-tools18 \
    && npm install -g azurite

ENV PATH="$PATH:/opt/mssql-tools18/bin"
USER vscode
