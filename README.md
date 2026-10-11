# OmniPOS — Backend API

Cloud API for the OmniPOS platform. **ASP.NET Core 8** Web API with PostgreSQL 16 and Redis 7, built for high-concurrency billing and multi-tenant stores.

## Structure

```
backend-api/
├── PosSystem.Api/
│   ├── Controllers/             # API controllers (orders, etc.)
│   ├── Program.cs               # App bootstrap, CORS, Swagger, EF Core, Redis
│   ├── appsettings.json         # Connection strings & JWT settings
│   └── PosSystem.Api.csproj
└── docker-compose.yml           # PostgreSQL 16, Redis 7 & Adminer
```

## Getting Started

### 1. Start infrastructure (PostgreSQL & Redis)

Ensure Docker Desktop is running, then:

```bash
docker compose up -d
```

- PostgreSQL: `localhost:5432`
- Redis: `localhost:6379`
- Adminer GUI: `http://localhost:8080`

### 2. Run the API

```bash
dotnet run --project PosSystem.Api
```

- API: `http://localhost:5000`
- Swagger: `http://localhost:5000/swagger`

## Configuration

Connection strings and JWT settings live in `PosSystem.Api/appsettings.json`. Override them per environment with `appsettings.Development.json` or environment variables (do not commit secrets).

## Requirements

- .NET 8 SDK
- Docker Desktop (for PostgreSQL & Redis)
