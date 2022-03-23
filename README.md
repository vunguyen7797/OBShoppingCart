
# Simple Shopping Cart Web API

This project is a demo shopping cart Web API created with ASP.NET Core 6.

## Features

- [POST] - [AllowAnonymous] Create a new order with a list of items.
- [PUT] - [Authorize]       Update an entire order (any changes, eg: shipping address, item quantities).
- [PATCH] - [Authorize]   Update order detail partially.
- [GET] - [Authorize]     Get all created orders with full details.
- [GET] - [Authorize]     Get a single brief order (without item details).
- [GET] - [Authorize]     Get a single full order (with all item details).
- [DELETE]  Delete a single created order.
- [POST]    Simple user registration.
- [POST]    Simple user login.

## Design Pattern
Simple repository-service pattern with Dependencies Injection.
The pattern in this project doesn't utilize the Unit of Work.

## Technologies, Tools and Packages:
- .NET 6 ASP.NET Core
- Entity Framework Core
- Swagger
- NLog
- AutoMapper
- JsonPatchDocument (AspNetcore.Mvc.NewtonsoftJson)
- SQL Server

## Explaination how a JWT is used to secure the endpoint
JWT works as an authentication mechanism. Thus, the JWT represents the unique identity of a user who is interacting with the server. User can be identified with JWT and this makes the data exchange process more secure without clients having to attach the private credentials on every server request.

When a user signs in and is verified, the API (Token Service in the project) will create a JWT and sign it using a secret key specified in appsettings.json file.

Once the client obtains the token, this token can be used to authenticate without private credentials needed in future requests. The client can just send subsequent requests with JWT in the header. JWT Bearer authentication will extract and validate the JWT token from Authorization request header.

Endpoints with [Authorize] attribute limits access authenticated users.
Endpoints with [AllowAnnonymous] accepts any requests without authentication.

When the app gets more complex, claims-based authorization (roles, policy) should be applied to secure endpoints with clear access scopes for different types of users.

## Installation

In main OBShoppingCart directory, create two files: **appsettings.json** and **appsettings.Development.json** with the code below:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL database connection string"
  },
  "TokenKey": "something secret"
}
```

Migrate database using Package Manager Console in Visual Studio: 
(Select **OBShoppingCart.DataAccess** on Default Project dropdown.)

```
add-migration Message
update-database
```

Application Urls

```
https://localhost:5000
http://localhost:5001
```
## NLog
Log files may be located in `OBShoppingCart/OBShoppingCart/bin/Debug/net6.0/logs`
