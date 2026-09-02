# Pie Shop API

A RESTful ASP.NET Core API for managing a fictional online pie shop, featuring JWT authentication, role-based authorization, CRUD operations, and data persistence with Entity Framework Core.

Demo: https://pie-shop-appsvc-h0dzgyhgbha9fbg6.canadacentral-01.azurewebsites.net/swagger/index.html

## Overview

Pie Shop API is a RESTful backend application built with C# and ASP.NET Core for managing an online pie shop. The API provides full CRUD functionality for pie data, along with searching, filtering, and pagination.

The project also implements a mock authentication and authorization system using JSON Web Tokens (JWT). Users authenticate through a dedicated login endpoint and receive a JWT that is required when accessing protected resources. The API supports different levels of access, with administrators receiving full CRUD access while standard users are restricted to read-only operations.

The project was built to develop practical experience with backend development concepts including RESTful API design, data persistence with Entity Framework Core, asynchronous programming, JWT authentication and authorization, and separation of application responsibilities.

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
- Swagger
- Git
- Azure


## What I learned

This project helped me develop a deeper understanding of:
- Designing RESTful APIs
- Authentication and Authorization
- Entity Framework Core
- Repository pattern
- Asynchronous programming
- Dependency Injection
- Separation of Concerns
