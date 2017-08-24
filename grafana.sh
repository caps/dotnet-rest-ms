#!/bin/bash

docker stop grafana;
docker rm grafana;

docker run -p 3000:3000 --link prometheus:prometheus --name grafana grafana/grafana
