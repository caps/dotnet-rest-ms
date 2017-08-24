#!/bin/bash
dotnet restore;
dotnet publish -c "Release";

docker build -t dotnet-ms .;
docker stop dotnet-ms && docker rm dotnet-ms;
docker run -it -p 5000:5000 --name dotnet-ms dotnet-ms;
