# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a **vibecoding workshop** designed for experienced developers who are skeptical about LLM-assisted coding. The project demonstrates progressive levels of LLM integration through practical exercises using a **chatbot** as the example application.

### Workshop Structure

Each exercise follows this pattern:
1. **Base layer** - Minimal scaffolding (Vue + .NET) → commit
2. **Exercise solution** - Implementation with different levels of LLM guidance → commit

### Exercise Levels

1. **Exercise 1 - Free-form vibecoding**: Let the LLM work autonomously with minimal constraints (e.g., "create a chatbot")
   - LLM makes ALL decisions about structure, patterns, features
   - Shows what you get with zero guardrails

2. **Exercise 2 - Structured guardrails**: Same task but with architectural constraints
   - DDD for backend, specific Vue patterns for frontend
   - Demonstrates controlled LLM behavior within established patterns

3. **Exercise 3 - Prompt engineering**: Same task with pre-made, reusable prompts
   - Template-based approach for consistency
   - Shows how to standardize LLM output

The workshop compares these approaches to help developers understand when and how to effectively use LLM assistance.

## Architecture

### Frontend
- **Location**: `frontend/`
- **Stack**: Vue 3 + TypeScript + Vite
- **Build tool**: Rolldown-Vite (experimental Vite replacement)
- **Component style**: Single File Components (SFC) with `<script setup>` syntax

### Backend
- **Location**: `backend/Api/`
- **Base**: Scaffolded .NET Web API
- **For Exercise 2+**: Domain-Driven Design (DDD) structure
  - **Domain**: Core business logic and entities
  - **Application**: Use cases and application services
  - **Infrastructure**: Data access, external services
  - **API**: HTTP endpoints and controllers
- **Testing**: BDD approach for verifying functionality (Exercise 2+)

## Common Commands

### Frontend (Vue/Vite)

```bash
# Navigate to frontend
cd frontend

# Install dependencies
npm install

# Run development server
npm run dev

# Type-check and build for production
npm run build

# Preview production build
npm run preview
```

### Backend (.NET)

```bash
# Navigate to backend
cd backend/Api

# Restore dependencies
dotnet restore

# Run the API
dotnet run

# Run tests (when added)
dotnet test
```

## Workshop Exercise Guidelines

When implementing features for workshop exercises:

### Exercise 1 - Free-form Mode
- Minimal architectural constraints
- Let the LLM make decisions about structure, naming, patterns
- Focus on getting working code quickly
- Document what the LLM chose to do and why

### Exercise 2 - Guardrails Mode
- Follow strict architectural patterns (DDD for backend, Vue composition API patterns for frontend)
- Enforce naming conventions and folder structure
- Require specific patterns for data flow, validation, error handling
- LLM should ask for clarification when patterns are ambiguous

### Exercise 3 - Prompt Engineering Mode
- Use predefined prompts stored in the project (location TBD)
- Prompts should be reusable templates with parameter substitution
- Focus on consistency and repeatability across similar tasks
- Examples: "create CRUD endpoint", "add form validation", "implement BDD test scenario"

## Key Architectural Decisions

### Frontend
- TypeScript strict mode for type safety
- Composition API with `<script setup>` (not Options API)
- Keep components focused and single-purpose

### Backend (planned)
- Domain layer should have no external dependencies
- Application layer orchestrates use cases
- Infrastructure implements interfaces defined in Application/Domain
- BDD tests should be written in Gherkin format and focus on business scenarios, not implementation details

## Notes for LLM Instances

- This is a **teaching project** - prioritize clarity and demonstrable patterns over production-grade optimization
- Each exercise level should produce working code that can be compared side-by-side
- When implementing DDD, be explicit about which layer each class belongs to
- BDD tests should read like executable specifications that non-technical stakeholders could understand
