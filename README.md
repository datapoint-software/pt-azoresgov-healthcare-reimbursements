# azoresgov-healthcare-reimbursements

## Summary

A Reimbursement Management System for the Healthcare Department of the Government of Azores.

## Getting started

### System requirements

#### Docker Desktop

Download from → https://www.docker.com/products/docker-desktop/.

#### Microsoft Visual Studio 2022

Download from → https://visualstudio.microsoft.com/vs/.

Ensure you include the ASP.NET Core components in your installation.

Ensure .NET/8 and C#/12 is supported in your development environment.

#### Microsoft Visual Studio Code

Download from → https://code.visualstudio.com/

#### NodeJS

Download from → https://nodejs.org/en/download/

#### MySQL Innovation

Run it as a Docker Container →

```
docker run --name mysql-server-innovation -p 3306:3306 -e MYSQL_ROOT_PASSWORD=321cd92b-d888-4e2e-96f9-73cedbca038d -d mysql:innovation
```

Create the application database →

```
CREATE DATABASE `Reimbursements` 
    DEFAULT ENCRYPTION 'N'
	DEFAULT CHARACTER SET `utf8mb4`
    COLLATE `utf8mb4_0900_ai_ci`;
```

Create the application user →

```
CREATE USER 'reimbursements-app'@'%'
	IDENTIFIED BY 'c9e93853-8225-4ff7-b5e6-77fe222edd18';
    
GRANT DELETE, INSERT, UPDATE, SELECT
	ON `Reimbursements`.* 
	TO 'reimbursements-app'@'%';

FLUSH PRIVILEGES;
```

Create the migrator user →

```
CREATE USER 'reimbursements-migrator-app'@'%'
	IDENTIFIED BY '667e9bd5-ffc1-4232-85ae-d085061668b4';
    
GRANT ALL PRIVILEGES
	ON `Reimbursements`.* 
	TO 'reimbursements-migrator-app'@'%';
    
FLUSH PRIVILEGES;
```