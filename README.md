# Rest API Backend

Rest API Project build using .net Core (8.0) with EF 8

# Installation

For initial run make sure you have installed .net runtime.

For Mac, Arm64: https://learn.microsoft.com/en-us/dotnet/core/install/macos

## To run a local environment

### Create a local DB with docker

```bash
docker run --name test-db -e POSTGRES_DB=test-db -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres
```

### To create a migration for the database:

NOTE: If this is the first time you run the API, just sync the DB

Create a new migration with changes:

> NOTE: Make sure you have the EF tools installed
> dotnet tool install --global dotnet-ef
> more info: https://learn.microsoft.com/en-us/ef/core/cli/dotnet

```bash
dotnet ef migrations add migration-name
```

Then sync the changes

```bash
dotnet ef database update
```

### Create local certs for Dev

This will create a local cert for development

```bash
dotnet dev-certs https --trust
```

### Run locally on https://localhost:4001/graphql/

```bash
dotnet run
```

# To create a migration for the database:

Create a new migration with changes:

```bash
dotnet ef migrations add migration-name
```

Then sync the changes

```bash
dotnet ef database update
```

# To create a build run from inside the project folder:

Make sure you run this command from inside the project folder, and that the folder "linux64_musl" is in the same folder as the docker file, otherwise it will fail

```bash
dotnet publish -r linux-musl-x64 --output ".\linux64_musl" --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true
```

# To deploy

```bash
TODO: add deployment steps
```

# Misc

## Install EF tools

```bash
dotnet tool install --global dotnet-ef
```

Then add it to your env Path

```
export PATH="$PATH:/Users/{user-name-path}/.dotnet/tools"
```

