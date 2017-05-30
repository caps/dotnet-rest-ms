FROM microsoft/dotnet:1.1.1-sdk

WORKDIR /build

COPY ./src/DotnetMicroservice .

RUN dotnet restore
RUN dotnet publish -c Release

RUN mkdir /app
RUN cp -r bin/Release/netcoreapp1.1/* /app/
RUN cp appsettings.json /app/

EXPOSE 5000

WORKDIR /app

ENTRYPOINT dotnet dotnet-ms.dll
