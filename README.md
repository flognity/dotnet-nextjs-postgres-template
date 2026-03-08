# Template for a Web-App with ASP.NET Core + Next.js + PostgreSQL

Starter template for a web application with an ASP.NET Core backend, a Next.js frontend, and a PostgreSQL database. The template is set up for a single-server deployment model where ASP.NET serves the API and the built frontend from one container.

## Stack

- Backend/runtime: ASP.NET Core Minimal API (`server/`) on .NET 10
- Frontend source: Next.js 16 + TypeScript + Tailwind (`frontend/`)
- Deployment model: single server container (ASP.NET serves API + static frontend)
- Database: PostgreSQL 16
- Migrations: Flyway SQL scripts in `db/migrations`
- Local orchestration: Docker Compose

## Prerequisites

- Docker Desktop and Docker Compose for the recommended local setup
- .NET 10 SDK for local backend development without Docker
- Node.js 20 and npm for local frontend development without Docker
- PostgreSQL 16 if you want to run the full stack locally without Docker

## Use This Template

1. Click `Use this template` on GitHub to create a new repository from this template.
2. Clone your new repository locally.
3. Search and replace all instances of `template-webapp` with your desired project name.
4. Rename `template-webapp.slnx` to `<your-project-name>.slnx`.
5. Review the license for your project and replace [LICENSE](./LICENSE) if you do not want to keep MIT.
6. Update the README with your project-specific details.
7. Update all comments at the start of each applicable file with your project-specific details, including license headers if you changed the license.
8. Open the solution in Visual Studio and set the Server as your startup project if you want to use the IDE workflow.

## Repository Structure

```text
.
|-- server/                 # ASP.NET Core backend project, Dockerfile, and runtime config
|-- frontend/               # Next.js app source
|-- db/
|   `-- migrations/         # Flyway-compatible SQL migrations
|-- .env.example            # Example local environment variables
|-- docker-compose.yml      # postgres + flyway + single server service
|-- LICENSE
|-- README.md
`-- template-webapp.slnx
```

## Quick Start (Docker, recommended)

```bash
docker compose up --build
```

Services started:

- `postgres` on `localhost:5432`
- `flyway` migration runner
- `server` on `http://localhost:5000` (API + UI)

Health check:

```bash
curl http://localhost:5000/health
```

Stop the stack:

```bash
docker compose down
```

## Continuous Integration

GitHub Actions runs CI for pull requests and for pushes to `main` using [`.github/workflows/ci.yml`](./.github/workflows/ci.yml).

The workflow currently validates:

- backend restore and build with .NET
- frontend install with `npm ci`
- frontend production build with `npm run build`

If you change backend build behavior, Node.js requirements, or frontend build commands, update the workflow to match.

## How the Single-Server Setup Works

The frontend is still authored in `frontend/`, but it is exported as static files and bundled into the ASP.NET image:

1. [server/Dockerfile](./server/Dockerfile) builds `frontend/` with Node using `npm ci` and `npm run build`.
2. The exported frontend files are copied into the server's static web root during the container build.
3. ASP.NET serves the built frontend and API from the same container and port.

Result: one container, one port, one host.

## Local Development Without Docker

You can develop without Docker, but you must run PostgreSQL yourself and apply the SQL migrations in `db/migrations`.

### Database

1. Start PostgreSQL 16 locally.
2. Create a database named `template-webapp`.
3. Create a user such as `app` with access to that database.
4. Apply the SQL scripts in `db/migrations` in version order, or run Flyway yourself against the local database.
5. Set `ConnectionStrings__Default` to match your local database connection string.

The default connection string in [server/appsettings.json](./server/appsettings.json) expects:

```text
Host=localhost;Port=5432;Database=template-webapp;Username=app;Password=app
```

### Backend

```bash
cd server
dotnet restore
dotnet run
```

By default, the launch profile exposes the backend at `http://localhost:63101` and `https://localhost:63100`.

### Frontend

```bash
cd frontend
npm install
npm run dev
```

If your frontend needs to call the backend directly during local development, set `NEXT_PUBLIC_API_BASE_URL` to the backend URL you are actually using. Copy [`.env.example`](./.env.example) to your own local env file and adjust the values for your setup.

> Note: `npm run dev` starts a separate Next.js development server for UI iteration only. Production deployment uses the static frontend export bundled into the ASP.NET server.

## License

This repository is licensed under the [MIT License](./LICENSE). MIT allows broad reuse, modification, redistribution, and commercial use with minimal obligations.

If you use this repository as a template for your own project, review whether MIT is still the right license for your fork. If you choose a different license, replace [LICENSE](./LICENSE) and update any related file headers or README references accordingly.
