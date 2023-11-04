# Planet Party Planner (PPP) Backend Solution

## Description

This C# backend solution serves as the server-side component for managing users and events. It provides various endpoints to create, update, and query users and events, as well as user-event associations. The solution also supports category management.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [API Documentation](#api-documentation)
- [Database](#database)
- [Testing](#testing)
- [Contributors](#contributors)

## Installation

1. Clone this repository:

   ```bash
   git clone git@github.com:dimitricbrown/EnvironmentalActsBE.git
   cd EnvironmentalActsBE
   
## Usage

This backend solution provides a set of RESTful API endpoints for managing users, events, categories, and user-event associations. Below are some example endpoints:

- Check if a user exists: **`GET /checkuser/{uid}`**
- Get all users: **`GET /users`**
- Get a user by ID: **`GET /users/{id}`**
- Create a new user: **`POST /users`**
- Update a user: **`PUT /users/{id}`**
- Delete a user: **`DELETE /users/{id}`**
- Get events a user has signed up for: **`GET /userEvents/{uid}`**
- Get users on an event: **`GET /eventUser/{id}`**
- Get events created by a user: **`GET /createdEventUser/{uid}`**
- Add a user to an event: **`POST /eventUser/{userId}/{eventId}`**
- Get all events: **`GET /events`**
- Get an event by ID: **`GET /events/{id}`**
- Update an event: **`PUT /events/{id}`**
- Create a new event: **`POST /events`**
- Delete an event: **`DELETE /events/{id}`**
- Delete a user from an event: **`DELETE /eventUser/{eventId}/{userId}`**
- Get all categories: **`GET /categories`**

Please provide the required data when making API requests.

## Configuration

The application supports CORS. Make sure to configure allowed origins in the **`MyAllowSpecificOrigins`** policy within the **`Program.cs`** file.

## API Documentation

For detailed API documentation, please refer to the Swagger documentation, which can be accessed when the application is running in a development environment. Visit the Swagger UI at http://localhost:5000/swagger after starting the application.

## Database

The application uses Entity Framework Core with PostgreSQL. The database configuration is defined in the **`EADbContext`** class, which includes the following entities:

- **`User`**: Represents user data.
- **`Event`**: Represents event data.
- **`Category`**: Represents categories for events.

The **`OnModelCreating`** method in the **`EADbContext`** class is used to seed initial data into the database. It populates the database with sample data for users, events, and categories. You can modify this method to include your own initial data.

The database connection string should be configured in your app settings or environment variables to point to your PostgreSQL database.

## Testing

To test the application, you can write unit tests and integration tests. Make sure to include tests for each API endpoint to ensure the correctness of your code.

## Contributors

**Github Accounts**
- [**Cole Amantea**](https://github.com/coleama) - Full-Stack
- [**Dimitric Brown**](https://github.com/dimitricbrown) - Full-Stack
- [**Austin Magnum**](https://github.com/dipolarbear25) - Frontend Code Only
- [**Cody Tucker**](https://github.com/ctucker0113) - Frontend Code Only

