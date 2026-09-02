# Pie Shop API

A RESTful API built with C# and ASP.NET Core for managing an online pie shop

Demo: https://pie-shop-appsvc-h0dzgyhgbha9fbg6.canadacentral-01.azurewebsites.net/swagger/index.html


## Features

- Full CRUD operations with searching, filtering and pagination
- JWT-based authentication
- Role-based authorization
- Entity Framework Core data persistence
- Asynchronous database operations
- RESTful API endpoints
- Swagger documentation


## Authentication & Authorization

The API includes a mock authentication system designed to demonstrate JWT-based authentication and authorization

Users authenticate through the login endpoint by providing a username and password. The API returns a JWT that must be included in subsequent requests

Two access levels are supported:

Username/Password: admin - Grants full API access including POST PUT and DELETE requests

Any other provided credentials are limited to read-only access to GET endpoints


## Technologies
- C#
- ASP.NET Core
- Entity Framework Core
- Git
- Azure


## What I learned

This project helped me develop a deeper understanding of:
- Designing RESTful APIs
- Authentication and Authorization
