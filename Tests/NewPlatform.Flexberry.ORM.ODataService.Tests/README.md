# Integration Tests

## Tests run Postgres

Before start test run please fill connection string `ConnectionStringPostgres` in `App.config` like this:

```xml
<add name="ConnectionStringPostgres" connectionString="SERVER=localhost;User ID=postgres;Password=p@ssw0rd;Port=5432;" />
```
Install [Docker](https://docker.com) if it is not installed yet and start Docker.

Then start Docker container with PostgreSQL by command from root folder this repository:

```sh
docker-compose up -d
```

Tests ready to run. Do it now.

When the database is no longer needed for tests, run the command:

```sh
docker-compose down
```

Also perform undo in App.config before commit changes.

## Test run MSSQL

Before start test run please fill connection string `ConnectionStringMssql` in `App.config` like this:

```xml
<add name="ConnectionStringMssql" connectionString="SERVER=localhost;User ID=sa;Password=p@ssw0rd;" />
```

Install [Docker](https://docker.com) if it is not installed yet and start Docker.

Then start Docker container with Microsoft SQL Server by command from root folder this repository:

```sh
docker-compose -f docker-compose-mssql.yml up -d
```

Tests ready to run. Do it now.

When the database is no longer needed for tests, run the command:

```sh
docker-compose down
```

Also perform undo in App.config before commit changes.