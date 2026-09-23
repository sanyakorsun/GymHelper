# GymHelper

GymHelper is a web application for tracking gym workouts.

## Features

- Create a user profile
- Create workouts
- Add exercises to workouts
- Add sets with weight and repetitions
- View workout history
- Delete workouts
- Create and delete exercises
- Store workout data in SQLite

## Technologies

- C#
- .NET 10
- ASP.NET Core
- Entity Framework Core
- SQLite
- HTML
- CSS
- JavaScript
- Swagger

## Project Structure

- `GymHelper.API` — API controllers and web frontend
- `GymHelper.Application` — services, DTOs and interfaces
- `GymHelper.Domain` — application entities
- `GymHelper.Infrastructure` — database context and repositories

## How to Run

1. Open `GymHelper.sln` in Visual Studio.
2. Set `GymHelper.API` as the startup project.
3. Run the project.
4. Open the application in your browser.

## Database

The application uses SQLite.

The database file is:

`gymhelper.db`

## Author

GymHelper project created as a student project.