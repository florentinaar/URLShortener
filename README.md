# URL Shortener Application 

## Overview

A simple URL Shortener application built using ASP.NET Core 8 and Entity Framework Core  
It allows users to:
- Shorten URLs, Track the number of clicks on each shortened URL, Set expiration times for shortened URLs, Delete URLs from the database, and genereate QRCode

## Prerequisites

Before running this application, ensure you have the following installed:
- .NET SDK
- SQL Server
- Entity Framework Core
- Visual Studio

## Getting Started
### 1. Clone the Repository
Clone the repository

### 2. Setup Database

Update the connection string in appsettings.json to match your SQL Server configuration:

"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=url_shortener;Trusted_Connection=True;TrustServerCertificate=True;"
}

Run database migrations:

- dotnet ef migrations add InitialCreate
- dotnet ef database update

or:
- Add-Migration "Initial"
- Update-Database

### 3. Run the Application

dotnet run

Open the browser and navigate to the URL that is displayed in the console.
