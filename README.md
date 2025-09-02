# GameLibraryApi

This repository contains an example ASP.NET Core Web API project for managing a game library. It is intended for educational purposes and demonstrates basic CRUD operations using C# and .NET 9.

## Purpose
This project is provided as an example for learning and educational use. It is not intended for production use.

## Features
- Retrieve all games
- Get details for a specific game
- Add new games
- Edit existing games
- Delete games

## Getting Started
1. Clone the repository:
   ```sh
   git clone <repository-url>
   ```
2. Open the solution in Visual Studio 2022 or later.
3. Build and run the project.
4. The API will be available at `https://localhost:<port>/api/GameStuff`.

## API Endpoints
- `GET /api/GameStuff` - Get all games
- `GET /api/GameStuff/{id}` - Get a game by ID
- `POST /api/GameStuff` - Add a new game
- `PUT /api/GameStuff/{id}` - Edit a game
- `DELETE /api/GameStuff/{id}` - Delete a game

## License
This project is for educational purposes only. Feel free to use, modify, and share for learning.
