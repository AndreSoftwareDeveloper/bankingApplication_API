# Banking Application API

REST API created in ASP.NET Core. Provides ability to perform CRUD operations on customer's data, divided into natural and juridical persons.

## Requirements

* Visual Studio 2022
* .NET SDK 8.0

## Installation
`git clone https://github.com/AndreSoftwareDeveloper/bankingApplication_API`
`cd bankingApplication_API`

## Configuration
Run migrations to create new database schema:
`dotnet ef migrations add InitialCreate`
`dotnet ef database update`

To build the API run the following command:
`dotnet run`

## Endpoints
### Account
* GET /api/Account

### JuridicalPerson
* GET /api/JuridicalPerson
* POST /api/JuridicalPerson
* GET /api/JuridicalPerson/{id}
* GET /api/JuridicalPerson/customerNumber/{customerNumber}

### NaturalPerson
* GET /api/NaturalPerson
* POST /api/NaturalPerson
* PATCH /api/NaturalPerson
* GET /api/NaturalPerson/{id}
* GET /api/NaturalPerson/customerNumber/{customerNumber}
