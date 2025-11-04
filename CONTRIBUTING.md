# Contributing to Horus

Thank you for your interest in contributing to **Horus**!  
This document describes the expected contribution process, code standards, CI checks, and review workflow. Please read it carefully before opening a pull request.

---

## Overview

Horus is a full-stack application composed of:

- **Frontend:** Next.js 15 (`/client`)
- **Backend:** ASP.NET 9 (`/backend`)

Our CI/CD pipelines enforce high code quality through **linting, testing, and formatting** for both environments.  
Every contribution must pass all automated checks before being merged into the main branches.

---

## Repository Structure

```

/client/      → Next.js frontend (Node 25)
/backend/     → .NET 9 backend
/.github/     → CI workflows

```

---

## Development Setup

### Frontend

```bash
cd client
npm install
npm run dev
```

### Backend

```bash
cd backend
dotnet restore
dotnet build
dotnet run
```

Make sure you have **.NET SDK 9.0+** and **Node 25+** installed locally.

---

## Code Style & Linting

### Frontend

We follow the official **Next.js ESLint** rules, extended with Prettier for formatting.

Run lint locally:

```bash
npm run lint
```

### Backend

We use `dotnet format` and the default **Microsoft Code Analysis** rules.
These enforce consistent spacing, async naming conventions, nullable reference usage, and analyzer checks.

Run lint locally:

```bash
dotnet format --verify-no-changes
```

> Any formatting or analyzer violations must be fixed before opening a pull request.

---

## Testing

### Frontend

Tests are written using **Jest** and run automatically in CI.

```bash
npm test
```

### Backend

Unit tests use **xUnit** and must produce a `.trx` report.

```bash
dotnet test --logger "trx;LogFileName=test_results.trx" --collect:"XPlat Code Coverage"
```

All test artifacts are uploaded in CI for review.

---

## Continuous Integration

All contributions are validated through **GitHub Actions** workflows:

| Workflow        | Description                                          |
| --------------- | ---------------------------------------------------- |
| `ci-node.yml`   | Builds and tests the Next.js frontend                |
| `ci-dotnet.yml` | Builds, formats, and tests the .NET backend          |
| `lint.yml`      | Runs ESLint, .NET analyzers, and EditorConfig checks |

### Triggers

Workflows execute automatically on:

- `push` events targeting `main` and `develop`
- `pull_request` events into `main` or `develop`

### GitHub Checks

All lint, build, and test results are reported back to the Pull Request through **GitHub Checks** annotations.

---

## Commit Conventions

We follow the **Conventional Commits** standard:

| Type        | Description                               |
| ----------- | ----------------------------------------- |
| `feat:`     | New feature                               |
| `fix:`      | Bug fix                                   |
| `docs:`     | Documentation changes                     |
| `style:`    | Code style / formatting                   |
| `refactor:` | Refactoring without new features or fixes |
| `test:`     | Adding or fixing tests                    |
| `chore:`    | Build process or tooling changes          |

Example:

```
feat(subscription): add cancellation event and domain handler
```

---

## Pull Request Process

1. Create a new branch from `develop`:

   ```
   git checkout -b feat/your-feature-name
   ```

2. Ensure all tests and linters pass locally.
3. Push your branch and open a Pull Request.
4. A maintainer will review your code before merging.
5. CI must succeed for the PR to be merged.

> PRs without successful CI runs or unresolved review comments will not be merged.

---

## Local Validation

You can run all checks locally before committing:

```bash
# Frontend
npm run lint && npm test

# Backend
dotnet format && dotnet test
```

---

## Review Standards

Each PR must:

- Contain descriptive commit messages.
- Include tests for new logic when applicable.
- Avoid unrelated file changes.
- Pass all workflows and code reviews.

Maintainers may request updates or additional tests before merging.

---

## License

By contributing to this repository, you agree that your contributions will be licensed under the same license as the project.

---

## Security

If you discover a security vulnerability, **do not open a public issue**.
Please contact us directly:

**Email:** [contato@joao-alves.com](mailto:contato@joao-alves.com)

We take security reports seriously and will respond as soon as possible.

---

## Community

You can discuss ideas, improvements, or issues using **GitHub Discussions**.
For technical or feature proposals, please create a new discussion thread before submitting a PR.

---

**Thank you for contributing to Horus!**
