# Game Library Management System

This repository contains a complete full-stack game library management system with an ASP.NET Core Web API backend and a .NET MAUI cross-platform mobile application. The system demonstrates modern development practices including database normalization, image storage, and cross-platform mobile development.

## Purpose
This project serves as a comprehensive example for learning modern .NET development patterns, including:
- RESTful API design with Entity Framework Core
- Database normalization and relationships
- Image storage and management
- Cross-platform mobile development with .NET MAUI
- Modern JSON serialization practices

## Architecture

### Backend (GameLibraryApi)
- **ASP.NET Core Web API** (.NET 9)
- **Entity Framework Core** with SQL Server LocalDB
- **Normalized Database Schema** with proper foreign key relationships
- **Image Storage** with database persistence
- **System.Text.Json** serialization with circular reference handling

### Frontend (MauiGameLibrary)
- **.NET MAUI** cross-platform application
- **MVVM Architecture** with data binding
- **Image Upload/Download** functionality
- **Cross-platform** support (Android, iOS, Windows, macOS)

## Features

### Core Game Management
- ✅ **Complete CRUD Operations** - Create, Read, Update, Delete games
- ✅ **Game Information** - Title, company, description, year published, multiplayer support
- ✅ **Normalized Database** - Separate tables for GameTypes, Genres, and AgeRestrictions
- ✅ **Data Validation** - Input validation and error handling

### Image Management
- ✅ **Database Image Storage** - Images stored as byte arrays in database
- ✅ **Image Upload/Download** - Full image lifecycle management
- ✅ **Image Validation** - File type and size validation (JPEG, PNG, GIF, BMP, 5MB limit)
- ✅ **Real Sample Data** - Seeded with actual game images

### Reference Data
- ✅ **Game Types** - Nintendo Wii, Switch, PlayStation 5, Xbox Series X, PC
- ✅ **Genres** - Adventure, Platformer, Action, RPG, Strategy, Sports, Racing, Shooter
- ✅ **Age Restrictions** - ESRB ratings (E, E10+, T, M, AO, RP)

### MAUI Mobile Application
- ✅ **Cross-Platform UI** - Native performance on multiple platforms
- ✅ **Image Selection** - Device camera/gallery integration
- ✅ **Local Image Caching** - Optimized image display
- ✅ **Responsive Design** - Adaptive layouts for different screen sizes

## Getting Started

### Prerequisites
- Visual Studio 2022 or later with .NET 9 SDK
- SQL Server LocalDB (included with Visual Studio)
- .NET MAUI workload installed

### Running the API
1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd GameLibraryRest
   ```

2. Navigate to the API project:
   ```sh
   cd GameLibraryApi
   ```

3. Restore dependencies and build:
   ```sh
   dotnet restore
   dotnet build
   ```

4. Apply database migrations:
   ```sh
   dotnet ef database update
   ```

5. Run the API:
   ```sh
   dotnet run
   ```

6. The API will be available at `https://localhost:7108` or `http://localhost:5025`

### Running the MAUI Application
1. Open the solution in Visual Studio 2022
2. Set `MauiGameLibrary` as the startup project
3. Select your target platform (Windows, Android, iOS, etc.)
4. Update the API URL in `MauiGameLibrary/Configuration/ApplicationSettings.cs` if needed
5. Build and run the application

## API Endpoints

### Game Management
- `GET /api/gamestuff` - Get all games with related data
- `GET /api/gamestuff/{id}` - Get a specific game by ID
- `POST /api/gamestuff` - Create a new game
- `PUT /api/gamestuff/{id}` - Update an existing game
- `DELETE /api/gamestuff/{id}` - Delete a game

### Reference Data
- `GET /api/gamestuff/gametypes` - Get all game types
- `GET /api/gamestuff/genres` - Get all genres  
- `GET /api/gamestuff/agerestrictions` - Get all age restrictions

### Image Management
- `GET /api/gamestuff/{id}/image` - Download game image
- `POST /api/gamestuff/{id}/image` - Upload game image (multipart/form-data)
- `DELETE /api/gamestuff/{id}/image` - Delete game image

## Database Schema

### Tables
- **GameInformations** - Main game data with foreign keys
- **GameTypes** - Console/platform types (Nintendo, PlayStation, etc.)
- **Genres** - Game genres (Adventure, Action, etc.)
- **AgeRestrictions** - ESRB age ratings (E, T, M, etc.)

### Key Features
- **Normalized Design** - Proper foreign key relationships
- **Image Storage** - Binary data with metadata (filename, content type)
- **Seeded Data** - Pre-populated with sample games and real images
- **Migration Support** - Entity Framework Code First migrations

## Technology Stack

### Backend
- **ASP.NET Core 9.0** - Web API framework
- **Entity Framework Core 9.0** - ORM and database access
- **SQL Server LocalDB** - Database engine
- **System.Text.Json** - JSON serialization
- **Swagger/OpenAPI** - API documentation

### Frontend  
- **.NET MAUI** - Cross-platform UI framework
- **MVVM Pattern** - Model-View-ViewModel architecture
- **Community Toolkit MAUI** - Additional controls and validation
- **MediaPicker** - Image selection from device
- **HttpClient** - API communication

## Sample Data

The system includes pre-seeded sample data:
- **The Legend of Zelda: Breath of the Wild** (Nintendo Wii, Adventure, E10+) - with real game image
- **Super Mario Odyssey** (Nintendo Switch, Platformer, E) - with real game image

## Development Notes

### Image Storage Implementation
- Images are stored as `byte[]` in the database `ImageData` field
- Image metadata includes filename and MIME type
- Client-side images are cached locally for performance
- Supports JPEG, PNG, GIF, and BMP formats with 5MB size limit

### JSON Serialization
- Uses `System.Text.Json` with circular reference handling
- Properly configured navigation properties for Entity Framework relationships
- Case-insensitive property matching for API compatibility

### Cross-Platform Considerations
- MAUI app targets Android, iOS, Windows, and macOS
- Images are handled appropriately for each platform
- Local file system access for image caching

## License
This project is for educational purposes and demonstration of modern .NET development practices. Feel free to use, modify, and share for learning.

## Contributing
This project demonstrates various .NET development patterns and is open for educational improvements and enhancements.
