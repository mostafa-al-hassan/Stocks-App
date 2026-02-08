# Stocks Trading App

A stock trading simulation platform built with ASP.NET MVC 9 that integrates real-time market data from Finnhub.

## Quick Start

### 1. Clone & Configure
```bash
git clone https://github.com/mostafa-al-hassan/Stocks-App.git
cd Stocks-App
```

### 2. Setup Database
Update `appsettings.json` with your own database connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Your_Connection_String"
}
```

Run migrations:
```bash
dotnet ef database update
```

### 3. Add Finnhub Token
Get free token from [finnhub.io](https://finnhub.io/) and add to `appsettings.json`:
```json
"Finnhub": { "Token": "your-token-here" }
```

### 4. Run Application
```bash
dotnet run
```
Visit: `https://localhost:5001/Trade`

## Architecture
```
StocksApp (MVC UI)
    ↓
Services (Business Logic)
    ↓
Repositories (EF Core)
    ↓
SQL Server Database
    ↓
Finnhub API ← Real-time Data
```

## Features
- Buy/Sell stock orders with real-time prices
- Order history dashboard
- PDF report generation
- N-tier architecture with Repository pattern
- Unit & integration tests

## Testing
```bash
# Run all tests
dotnet test

# Run specific tests
dotnet test Tests/ServiceTests
```

## Tech Stack
- **Backend**: ASP.NET MVC 9, Entity Framework Core
- **Database**: SQL Server
- **External API**: Finnhub (real-time stock data)
- **PDF**: Rotativa
- **Testing**: xUnit

## Screenshots
<img width="975" height="467" alt="image" src="https://github.com/user-attachments/assets/7d2885a6-3eb0-4c26-a109-330d55daaba7" />
<img width="975" height="456" alt="image" src="https://github.com/user-attachments/assets/56d52da1-dc4d-48f2-8c57-f203aefbb6d7" />
<img width="975" height="451" alt="image" src="https://github.com/user-attachments/assets/34874492-2329-448f-8824-d434d7eac6a8" />
<img width="975" height="493" alt="image" src="https://github.com/user-attachments/assets/64d26202-8d92-4648-b83a-52f2ed94a93a" />
