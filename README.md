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
The API will be available at https://localhost:5162

## Endpoints
### Account
* __GET /api/Account__ - Update transaction history with a new domestic transfer.

### JuridicalPerson
* __GET /api/JuridicalPerson__ - Retrieve data of all juridical persons.
* __POST /api/JuridicalPerson__ - Create an account as a juridical person.
* __GET /api/JuridicalPerson/{id}__ - Retrieve data of the juridical person with specified ID.
* __GET /api/JuridicalPerson/customerNumber/{customerNumber}__ - Retrieve data of the juridical person with specified customer number.

### NaturalPerson
* __GET /api/NaturalPerson__ - Retrieve data of all natural persons.
* __POST /api/NaturalPerson__ - Create an account as a natural person.
* __PATCH /api/NaturalPerson__ - Update an account with new password, NIP and REGON.
* __GET /api/NaturalPerson/{id}__ - Retrieve data of the natural person with specified ID.
* __GET /api/NaturalPerson/customerNumber/{customerNumber}__ - Retrieve data of the natural person with specified customer number.
