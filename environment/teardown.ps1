
docker-compose -f full-stack.yml down
docker-compose -f selenium-grid.yml down
docker-compose -f databases.yml down
docker-compose -f network.yml down

Remove-Item -r -force full-stack
Remove-Item -r -force databases
Remove-Item -r -force downloads