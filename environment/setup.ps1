# docker-compose scale chrome=5 
docker-compose -f network.yml up -d
docker-compose -f full-stack.yml up -d
docker-compose -f selenium-grid.yml up -d
docker-compose -f databases.yml up -d
docker-compose -f topics.yml up 