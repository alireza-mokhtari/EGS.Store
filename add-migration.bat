@echo off
set migrationName=%1
dotnet ef migrations add %migrationName% --project EGS.Infrastructure --startup-project EGS.Api --output-dir Persistence\Migrations 


