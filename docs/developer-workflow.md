# Horus Developer & Debugging Workflow

## 1. Local Environment Setup

### Prerequisites

- Node.js 25.x
- .NET SDK 9.0
- Docker & Docker Compose
- VSCode or JetBrains Rider (recommended)
- npm (preferred over npm for workspace performance)

### Environment Variables

- Copy `.env.example` -> `.env.local`
- Update the following values:
  - `RABBITMQ_USER`
    - This will be your user for the RabbitMQ Dashboard
  - `RABBITMQ_PASSWORD`
    - This will be your password for the RabbitMQ Dashboard
  - `POSTGRES_USER`
    - This is the Database User
  - `POSTGRES_PASSWORD`
    - This is the Database Password
  - `DATABASE_CONNECTION_STRING`
    - Host=postgres;Database={POSTGRES_DB};Username={POSTGRES_USER};Password={POSTGRES_PASSWORD}

---

## 2. Running the Application

### Backend (.NET)

```bash
cd backend
dotnet restore
dotnet build
dotnet watch run --urls http://0.0.0.0:8080 --project ./Horus.Api/Horus.Api.csproj
```

### Frontend (Next.js)

```bash
cd client
npm install # Or npm, yarn...
npm dev
```

### Access

- Frontend -> [http://localhost:3000](http://localhost:3000)
- API -> [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 3 Debugging Setup

Horus supports debugging for both **backend (.NET 9)** and **frontend (Next.js 15)**.

- Backend: Visual Studio Code (C# Dev Kit), Visual Studio, CLI, or Docker.
- Frontend: VS Code JavaScript debugger and browser DevTools.

For full setup details, see the dedicated documentation:

**[Debugging & Developer Workflow](/docs/debugging.md)**

---

### Need Help?

If you encounter issues attaching the debugger or inspecting worker queues, please contact:

**Security Contact:** contato@joao-alves.com  
**Subject:** `[Horus] Debugging Assistance`

## 4. Testing

### Backend

```bash
cd backend/tests
dotnet test --collect:"XPlat Code Coverage"
```

Results: `TestResults/coverage.cobertura.xml` (uploaded to GitHub Artifacts)

### Frontend

```bash
cd client
npm test
npm lint
```

---

## 5. Branching & Commits

Follow **Conventional Commits**:

```
feat(subscription): add webhook event handler
fix(api): handle invalid plan cancellation
```

Branch names:

```
feature/embedding-queue
fix/lint-workflow
chore/update-dotnet-sdk
```

---

## 6. Developer Tips

- **Hot Reload:** both Next.js and .NET 9.0 support hot reload, no manual restarts.
- **API Contracts:** maintain schema parity between backend `DTOs` and frontend `zod` schemas.
- **Logging:** use structured logging with correlation IDs across backend requests.
- **VSCode Debug Profiles:** provided in `.vscode/launch.json` and `.vscode/tasks.json`.
- **Testing Jobs:** run background jobs locally using:

  ```bash
  docker compose up rabbitmq
  dotnet run --project backend/src/Horus.Worker
  ```

---

## 7. Common Issues

TODO

---

## 8. CI Integration

All local commands mirror CI pipelines.
Before pushing:

```bash
npm lint && npm test && dotnet test
```

This ensures you’ll pass:

- ESLint & Prettier
- .NET format & analyzers
- EditorConfig rules
- Unit tests with coverage
