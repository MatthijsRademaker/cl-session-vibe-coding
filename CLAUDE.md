# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a **vibecoding workshop repository** - a hands-on educational project for experienced developers to learn about LLM-assisted coding. The repo contains multiple exercise branches demonstrating different approaches to building the same chatbot application (from free-form to DDD with proper architecture).

**Important**: This is a teaching repository. The code intentionally shows progression from poor to good practices across different branches.

## Repository Structure

### Multi-Stack Application
- **Frontend**: Vue 3 + TypeScript + Vite (in `frontend/`)
- **Backend**: .NET 8 Web API with DDD architecture (in `backend/`)
- **Presentation**: Slidev presentation (in `presentation/`)

### Branch Structure
The exercises are layered branches building on each other:
- `main` - Base layer
- `exercise-0-messy-legacy` - Intentionally messy brownfield code
- `exercise-1-freeform` - Fast prototype (no architecture)
- `exercise-2-ddd-guardrails` - Clean DDD implementation
- `exercise-3-prompt-engineering` - Template-guided implementation
- `exercise-4-brownfield-migration` - Migration strategy

## Common Development Commands

### Docker (Recommended)
```bash
docker-compose up            # Start both frontend and backend with hot reload
docker-compose up -d         # Start in detached mode
docker-compose down          # Stop all services
docker-compose logs -f       # View logs
docker-compose restart backend   # Restart specific service
```

Both services run with hot reload enabled:
- Frontend: http://localhost:5173
- Backend: http://localhost:5000
- Swagger: http://localhost:5000/swagger

### Frontend (Vue + Vite) - Local Development
```bash
cd frontend
npm install          # Install dependencies (or use bun install)
npm run dev          # Start dev server (http://localhost:5173)
npm run build        # Build for production
```

### Backend (.NET 8 API) - Local Development
```bash
cd backend/Api
dotnet restore       # Restore NuGet packages
dotnet run           # Run API (http://localhost:5000)
dotnet build         # Build solution
dotnet watch run     # Run with hot reload
```

### Testing (Backend)
```bash
cd backend/Tests
dotnet test                           # Run all tests
dotnet test --logger "console;verbosity=detailed"  # Verbose output
```

The test project uses xUnit, Reqnroll (BDD), and FluentAssertions.

### Presentation
```bash
npm run slides       # Start Slidev presentation
npm run slides:build # Build presentation
npm run slides:export # Export presentation
```

## Architecture

### Backend: Domain-Driven Design (DDD)

The .NET backend follows clean architecture with DDD patterns:

```
Api/                    - Entry point, controllers, dependency injection
├── Controllers/        - HTTP endpoints (thin controllers)
├── Program.cs          - DI container configuration

Application/           - Use cases and DTOs (orchestration layer)
├── DTOs/              - Data transfer objects
├── UseCases/          - Application logic (e.g., SendMessageUseCase)

Domain/                - Core business logic (framework-independent)
├── Entities/          - Domain models (Message, Conversation)
├── Interfaces/        - Repository contracts (IConversationRepository)
├── Services/          - Domain service contracts (IChatbotService)

Infrastructure/        - External concerns (I/O, persistence)
├── Repositories/      - Repository implementations (InMemoryConversationRepository)
├── Services/          - External service implementations (RuleBasedChatbotService)

Tests/                 - xUnit + Reqnroll tests
```

### Dependency Flow
```
Api → Application → Domain ← Infrastructure
                      ↑
                  (implements)
```

**Key Principle**: Domain layer has no dependencies. Infrastructure and Application depend on Domain interfaces.

### Frontend: Vue 3 Composition API

Simple Vue 3 setup:
- TypeScript with strict mode
- Vite for fast dev server and HMR
- Components in `src/components/`
- Entry point: `src/main.ts`

## Important Context for AI Coding

### When Working Across Branches
- Each exercise branch represents a **different quality level** of the same feature
- Exercise 0 is **intentionally bad** (teaching tool)
- Exercise 2-4 show **proper architecture**
- Don't "fix" Exercise 0/1 to look like Exercise 2 unless explicitly asked

### DDD Architecture Rules (Exercise 2+)
- **Domain entities** use private setters, factory methods, and validation
- **Use cases** coordinate between domain and infrastructure
- **Controllers** are thin - delegate to use cases
- **Never** let infrastructure concerns leak into Domain
- **Repository pattern** abstracts data access

### Testing Philosophy
- Tests use **xUnit** (not NUnit or MSTest)
- Use **FluentAssertions** for readable assertions
- **Reqnroll** for BDD-style feature tests (successor to SpecFlow)

## Workshop Context

This repository supports a 2-hour hands-on workshop. Participants:
1. See messy legacy code (Exercise 0)
2. Compare free-form vs. structured approaches
3. Build features using prompt templates
4. Experience maintenance challenges

**Key Insight**: The goal is to show trade-offs between speed and structure, not to declare one approach "best."

## What NOT to Do

- Don't add unnecessary complexity to Exercise 1 (it's meant to be simple)
- Don't remove the intentional technical debt in Exercise 0/1 (teaching examples)
- Don't change the DDD architecture in Exercise 2+ without explicit request
- Don't add authentication, database, or production features (this is a workshop demo)
- Don't suggest "improvements" that would obscure the teaching points

## CORS Configuration

The backend is configured to accept requests from:
- `http://localhost:5173` and `http://localhost:5174` (Vite default ports)
- `http://0.0.0.0:5173` and `http://0.0.0.0:5174` (Docker host binding)
- `http://127.0.0.1:5173` and `http://127.0.0.1:5174` (loopback)

This covers both local development and Docker environments. If you need additional origins, update the CORS policy in `backend/Api/Program.cs` (around line 14-30).

## Docker Setup

The project includes Docker support with hot reload for both frontend and backend:

**Frontend**: Uses Bun runtime with Vite dev server
- Volume mount for source code
- Hot reload enabled with `--host 0.0.0.0`
- Port 5173 mapped to host

**Backend**: Uses .NET SDK with `dotnet watch`
- Volume mounts exclude bin/obj folders
- Hot reload enabled
- Port 5000 mapped to host

Both containers run on a shared `chatbot-network` bridge network.
