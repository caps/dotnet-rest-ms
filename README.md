# dotnet-ms
A .NET Core RESTful microservice running on Docker

## Dependencies ##
- dotnet cli - was built with 1.0.3
- Docker daemon running - was tested with Docker version 17.03.1-ce, build c6d412e

## To run on Docker ##
`./build.sh`

## Run w/ Prometheus and Grafana ##
docker run -p 9090:9090 -v /prometheus.yml:/etc/prometheus/prometheus.yml --name prometheus prom/prometheus
docker run -d -p 3000:3000 --link prometheus:prometheus --name grafana grafana/grafana 

Login to Grafana using admin:admin, add Prometheus datasource with <ip_address>:9090, import Grafana dashboard with id 2204 (https://grafana.com/dashboards/2204) 
