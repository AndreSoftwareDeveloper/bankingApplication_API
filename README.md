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
* GET /api/Account - Update transaction history with a new domestic transfer.

### JuridicalPerson
* GET /api/JuridicalPerson - Retrieve data of all juridical persons.
* POST /api/JuridicalPerson - Create an account as a juridical person.
* GET /api/JuridicalPerson/{id} - Retrieve data of the juridical person with specified ID.
* GET /api/JuridicalPerson/customerNumber/{customerNumber} - Retrieve data of the juridical person with specified customer number.

### NaturalPerson
* GET /api/NaturalPerson - Retrieve data of all natural persons.
* POST /api/NaturalPerson - Create an account as a natural person.
* PATCH /api/NaturalPerson - Update an account with new password, NIP and REGON.
* GET /api/NaturalPerson/{id} - Retrieve data of the natural person with specified ID.
* GET /api/NaturalPerson/customerNumber/{customerNumber} - Retrieve data of the natural person with specified customer number.
