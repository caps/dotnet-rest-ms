#!/bin/bash
dotnet restore;
#dotnet build -c "Release";
dotnet publish -c "Release";

docker build -t dotnet-rest-ms .;
docker run -it -p 5000:5000 dotnet-rest-ms;
