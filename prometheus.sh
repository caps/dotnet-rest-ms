#!/bin/bash

docker stop prometheus;
docker rm prometheus;

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

docker run -p 9090:9090 --link dotnet-ms:dotnet-ms -v $DIR/prometheus.yml:/etc/prometheus/prometheus.yml --name prometheus prom/prometheus

