# Debugging & Developer Workflow

> This document explains how to debug both **Horus Backend (.NET 9)** and **Horus Frontend (Next.js 15)** using industry-standard tools.  
> It applies to local development, Docker environments, and background services.

---

## Table of Contents

1. [Backend Debugging](#-backend-debugging)
   - [Visual Studio Code (C# Dev Kit)](#visual-studio-code-c-dev-kit)
   - [Visual Studio IDE](#visual-studio-ide)
   - [Manual Debugging (CLI)](#manual-debugging-cli)
   - [Docker Remote Debugging](#docker-remote-debugging)
   - [Logging & Tracing](#logging--tracing)
2. [Frontend Debugging (Next.js)](#-frontend-debugging-nextjs)
   - [VS Code Integrated Debugger](#vs-code-integrated-debugger)
   - [Browser DevTools](#browser-devtools)
   - [Network & API Inspection](#network--api-inspection)
3. [Additional Tips](#-additional-tips)

---

## Backend Debugging

The backend of Horus is built on **.NET 9**, using **CQRS + Mediator**, **SignalR**, and **RabbitMQ workers**.  
You can debug it using multiple approaches depending on your environment and IDE.

---

### **Visual Studio Code (C# Dev Kit)**

**Extension:**  
[**C# Dev Kit (ms-dotnettools.csdevkit)**](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

**Official Debugging Guide:**  
[Debugging C# in VS Code](https://code.visualstudio.com/docs/csharp/debugging)

#### **Setup**

1. Open the backend folder:

```bash
cd backend
code .
```

2. Run the command:

```
.NET: Generate Assets for Build and Debug
```

This creates a `.vscode/` folder with `launch.json` and `tasks.json`.

3. Start debugging (`F5`) or use **Run → Start Debugging**.

#### **Custom Configuration Example**

```json
// .vscode/launch.json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Launch Horus.API",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/Horus.Api/bin/Debug/net9.0/Horus.Api.dll",
      "cwd": "${workspaceFolder}/Horus.Api",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    {
      "name": ".NET Attach Horus.Worker",
      "type": "coreclr",
      "request": "attach"
    }
  ]
}
```

This allows launching the API or attaching to background workers for RabbitMQ jobs.

[↑ Back to top](#-debugging--developer-workflow)

---

### **Visual Studio IDE**
 **Docs:** [Debug .NET applications in Visual Studio](https://learn.microsoft.com/en-us/visualstudio/debugger/debugger-feature-tour)

#### **Steps**

1. Open the solution:

   ```bash
   Horus.sln
   ```

2. Set the startup project to `Horus.Api`.
3. Press **F5** to start debugging with full IntelliTrace support.
4. To debug background services:

   - Run `Horus.Worker`
   - Use **Debug → Attach to Process...**

Visual Studio automatically builds, launches, and attaches to the running API.

[↑ Back to top](#-debugging--developer-workflow)

---

### **Manual Debugging (CLI)**

For command-line workflows or remote containers:

```bash
dotnet build
dotnet run --launch-profile "Horus.API"
```

Attach your debugger manually:

- **VS Code** → Run and Debug → `.NET Attach Horus.API`
- **Visual Studio** → Debug → Attach to Process…

To debug workers:

```bash
dotnet run --project Horus.Worker
```

Then attach to the running process and inspect RabbitMQ message flow.

[↑ Back to top](#-debugging--developer-workflow)

---

### **Docker Remote Debugging**

If the backend runs via Docker:

Add this configuration to `.vscode/launch.json`:

```json
{
  "name": ".NET Attach Horus.API (Docker)",
  "type": "coreclr",
  "request": "attach",
  "processName": "dotnet",
  "pipeTransport": {
    "pipeProgram": "docker",
    "pipeArgs": ["exec", "-i", "horus_api"],
    "debuggerPath": "/root/.dotnet/dotnet"
  }
}
```

Now you can attach to the API process inside the container for live debugging.

[↑ Back to top](#-debugging--developer-workflow)

---

### **Logging & Tracing**

For distributed flows (API + Workers + SignalR):

```csharp
_logger.LogInformation("Processing job {JobId} for target {Target}", job.Id, job.Target);
```

View logs directly:

```bash
docker logs horus_api -f
docker logs horus_worker -f
```

[↑ Back to top](#-debugging--developer-workflow)

---

## Frontend Debugging (Next.js)

The frontend is built with **Next.js 15** (React + TypeScript).
You can debug it either in VS Code or directly in your browser.

---

### **VS Code Integrated Debugger**

1. Install the **JavaScript Debugger** (built-in for VS Code).
2. Add a `.vscode/launch.json` in `/client`:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "type": "node",
      "request": "launch",
      "name": "Next.js Debug",
      "runtimeExecutable": "npm",
      "runtimeArgs": ["run", "dev"],
      "skipFiles": ["<node_internals>/**"],
      "port": 9229,
      "console": "integratedTerminal",
      "env": {
        "NODE_ENV": "development"
      }
    }
  ]
}
```

3. Start the debugger with **F5**.
4. Set breakpoints in any `.tsx` or `.ts` file — the debugger will pause when those files are hit.

> The Node debugger attaches to the Next.js dev server (`npm run dev`) automatically.

[↑ Back to top](#-debugging--developer-workflow)

---

### **Browser DevTools**

1. Start your frontend:

   ```bash
   npm run dev
   ```

2. Open the browser at [http://localhost:80](http://localhost:80)
3. Use:

   - **Elements tab** → inspect and modify DOM & CSS.
   - **Sources tab** → set JS/TS breakpoints.
   - **Network tab** → trace API calls to `http://localhost:3333`.

You can combine DevTools with VS Code debugger — both remain active simultaneously.

[↑ Back to top](#-debugging--developer-workflow)

---

### **Network & API Inspection**

For API-level debugging between frontend and backend:

- Install **REST Client** or **Thunder Client** VS Code extensions.
- Use **Network tab** in DevTools to verify response times and payloads.
- Or capture live traffic:

  ```bash
  docker logs horus_api -f
  ```

This helps validate integration between React hooks and backend CQRS endpoints.

[↑ Back to top](#-debugging--developer-workflow)

---

## Additional Tips

- Always ensure you’re running with `ASPNETCORE_ENVIRONMENT=Development`.
- Use structured logs (`ILogger<T>`) for backend and `console.group()` for frontend logging.
- For unit testing during debug:

  ```bash
  dotnet test --filter "FullyQualifiedName~TargetTests"
  npm run test
  ```

- To debug queue-related flow, attach to the worker and monitor RabbitMQ Dashboard (`http://localhost:15672`).

---

### References

- [VS Code — Debugging C#](https://code.visualstudio.com/docs/csharp/debugging)
- [C# Dev Kit Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
- [Visual Studio Debugger](https://learn.microsoft.com/en-us/visualstudio/debugger/debugger-feature-tour)
- [Next.js Debugging Guide](https://nextjs.org/docs/app/building-your-application/configuring/debugging)
- [Node.js Debugging in VS Code](https://code.visualstudio.com/docs/nodejs/nodejs-debugging)

---

> **Tip:** All contributors should familiarize themselves with this document before submitting PRs.
> Consistent debugging setups ensure reproducible results across environments.
