

- Configure user secrets in Host and in Functional tests projects to set your development connection string

- use dotnet restore to setup tools used by the project



Migrations

 dotnet ef migrations add migrationName --project src/SuperErp.Api/SuperErp.Api.csproj  --startup-project src/SuperErp.Host/SuperErp.Host.csproj -o Data/Migrations -v