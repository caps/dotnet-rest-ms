FROM microsoft/dotnet:1.1.1-runtime

WORKDIR /app

COPY /bin/Release/netcoreapp1.1 /app
COPY appsettings.Development.json /app/appsettings.json

EXPOSE 5000

ENTRYPOINT dotnet publish/dotnet-rest-ms.dll