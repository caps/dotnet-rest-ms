FROM microsoft/dotnet:1.1.2-runtime

WORKDIR /app

COPY /bin/Release/netcoreapp1.1 /app
COPY appsettings.json /app/appsettings.json

EXPOSE 5000

ENTRYPOINT dotnet publish/dotnet-ms.dll
