# MoviesAPI

MoviesAPI is a RESTful API built with ASP.NET Core targeting .NET 8. It provides endpoints for managing movies, comments, and user authentication. The API integrates with an external movie database (TMDB) to fetch movie details, genres, and other related data.

---

## Features
- Fetch now-playing, top-rated, and searched movies from TMDB.
- Manage user comments on movies.
- User authentication and registration with JWT-based authentication.
- Role-based authorization.
- Integration with TMDB API for movie data.

---

## Technologies Used
- **.NET 8**
- **ASP.NET Core**
- **Entity Framework Core** (SQL Server)
- **RestSharp** (for TMDB API integration)
- **Newtonsoft.Json** (for JSON serialization/deserialization)
- **JWT Authentication**
- **Swagger/OpenAPI** (for API documentation)

---

## Prerequisites
- .NET 8 SDK
- SQL Server
- TMDB API Key

---

## Installation

1. Clone the repository: git clone https://github.com/your-repo/MoviesAPI.git cd MoviesAPI
2. Configure the database connection string in `appsettings.json`: "ConnectionStrings": { "DefaultConnection": "Your-SQL-Server-Connection-String" }
3. Configure the TMDB API key in `appsettings.json`: "TMDB": { "BaseUrl": "https://api.themoviedb.org/3", "BearerToken": "Your-TMDB-API-Key" }
4. Apply database migrations: dotnet ef database update
5. Run the application
   