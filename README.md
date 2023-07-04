# azoresgov-healthcare-reimbursements

## Summary

A reimbursement management system for the Regional Directorate of Health of the Government of the Azores.

## Getting started

### System requirements

#### Docker Desktop

Download from → https://www.docker.com/products/docker-desktop/.

#### Microsoft Visual Studio 2022

Download from → https://visualstudio.microsoft.com/vs/.

Ensure you include the ASP.NET Core components in your installation.

Ensure .NET/7 and C#/11 is supported in your development environment.

#### Microsoft Visual Studio Code

Download from → https://code.visualstudio.com/

#### Microsoft SQL Server Management Studio

Download from → https://aka.ms/ssmsfullsetup.

#### NodeJS

Download from → https://nodejs.org/en/download/

#### Microsoft SQL Server 2022

Run it as a Docker Container →

```
docker run --name mssql-server-2022 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=662f08e2-efb9-452d-82a8-fcdb5c715d5a" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

Create the application database →

```
CREATE DATABASE [HealthcareReimbursements]
 COLLATE Latin1_General_100_CI_AI;

GO

ALTER DATABASE [HealthcareReimbursements] 
    SET COMPATIBILITY_LEVEL = 130;

GO
```

Create the application user →

```
CREATE LOGIN [azoresgov-healthcare-reimbursements-app] 
  WITH PASSWORD=N'8cd4a9c3-a6a6-4e6b-abd1-d38063cb7be4', 
  DEFAULT_DATABASE=[HealthcareReimbursements], 
  CHECK_EXPIRATION=OFF, 
  CHECK_POLICY=OFF;

GO

USE [HealthcareReimbursements];

GO

CREATE USER [azoresgov-healthcare-reimbursements-app] 
  FOR LOGIN [azoresgov-healthcare-reimbursements-app];

GO

ALTER ROLE [db_datareader] 
  ADD MEMBER [azoresgov-healthcare-reimbursements-app];

GO

ALTER ROLE [db_datawriter] 
  ADD MEMBER [azoresgov-healthcare-reimbursements-app];

GO
```

Create the migrator user →

```
CREATE LOGIN [azoresgov-healthcare-reimbursements-migrator-app] 
  WITH PASSWORD=N'3e0f2c93-0669-4b4b-9c91-4e652ebcd083', 
  DEFAULT_DATABASE=[HealthcareReimbursements], 
  CHECK_EXPIRATION=OFF, 
  CHECK_POLICY=OFF;

GO

USE [HealthcareReimbursements];

GO

CREATE USER [azoresgov-healthcare-reimbursements-migrator-app] 
  FOR LOGIN [azoresgov-healthcare-reimbursements-migrator-app];

GO

ALTER ROLE [db_owner] 
  ADD MEMBER [azoresgov-healthcare-reimbursements-migrator-app];

GO
```
