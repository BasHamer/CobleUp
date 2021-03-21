
docker-compose -f full-stack.yml down
docker-compose -f full-stack.yml down
docker-compose -f selenium-grid.yml down
docker-compose -f databases.yml down
docker-compose -f network.yml down

rm -r -f full-stack
rm -r -f databases
rm -r -f downloads