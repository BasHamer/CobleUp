Push-Location ..\src
dotnet restore
dotnet build
Pop-Location

Push-Location ..\src\CobleUp.BlazorApp
docker build .
Pop-Location

Push-Location ..\src\CobleUp.Worker
docker build .
Pop-Location

#docker run -itd --network=coble-up-net -p 8008:80 cobleup.blazorapp
#docker run -itd --network=coble-up-net cobleup.worker
