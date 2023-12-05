### Kafka with dotnetcore

#### kafka setup
```
# goto Weather.Kafka.Common/Config/  & run following commands
docker-compose up -d
docker-compose down

# if required to stop & remove all containers
docker stop $(docker ps -q)
docker rm $(docker ps -a -q)

# ref
curl --silent --output docker-compose.yml https://raw.githubusercontent.com/confluentinc/cp-all-in-one/6.1.1-post/cp-all-in-one/docker-compose.yml

# run control center of kafka
    - open http://localhost:9021/
```
Project Structure
```
# configs/models/kafka docker file
    - Weather.Kafka.Common

# producer
    - Weather.Kafka.Producer --> API
    - Weather.Kafka.Common --> common configs, models
    
# consumer
    - Weather.Kafka.Consumer --> worker service
    - Weather.Kafka.Common  --> common configs, models
```