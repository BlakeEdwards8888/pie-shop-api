# Pie Shop API

A RESTful API built with C# and ASP.NET Core for managing an online pie shop

Demo: https://pie-shop-appsvc-h0dzgyhgbha9fbg6.canadacentral-01.azurewebsites.net/swagger/index.html

## Overview

Pie Shop API is a RESTful backend application for managing products in a fictional online pie shop. The project was built to strengthen my understanding of backend development with ASP.NET Core and to practice designing and implementing a production-style REST API.

The API supports creating, retrieving, updating, and deleting pie data, along with searching and retrieving individual resources.

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
- Entity Framework Core
- Repository pattern
- Asynchronous programming
- Dependency Injection
- Separation of Concerns
