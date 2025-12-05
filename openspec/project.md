# Project Context

## Purpose
This is an **evidence-based workshop** for teaching experienced developers about LLM-assisted coding (vibecoding). The goal is not to evangelize, but to let developers **experience the differences themselves** through hands-on exercises.

### Core Goals
- Demonstrate the trade-offs between speed and structure in LLM-assisted development
- Show how templates and architecture can guide LLMs to produce maintainable code
- Provide honest comparisons between different approaches (free-form, DDD, template-driven)
- Address real-world concerns: brownfield migration, maintenance, and limitations

### Workshop Structure
The workshop uses a **chatbot application** as the example project, built incrementally through 5 exercises:
- **Exercise 0**: Messy legacy code (brownfield reality)
- **Exercise 1**: Free-form vibecoding (fast but technical debt)
- **Exercise 2**: DDD with guardrails (clean architecture, slower initial build)
- **Exercise 3**: Template-driven (best of both worlds)
- **Exercise 4**: Brownfield migration strategies

## Tech Stack

### Frontend
- **Vue 3** - Progressive JavaScript framework
- **TypeScript** - Type-safe JavaScript
- **Vite** - Fast build tool and dev server
- **Rolldown** - Rust-based bundler (vite override)

### Backend
- **.NET 9.0** - Modern .NET framework
- **ASP.NET Core** - Web API framework
- **C#** - Primary backend language
- **Clean Architecture** - Layered architecture pattern
  - Api (Controllers, DTOs)
  - Application (Use Cases, Interfaces)
  - Domain (Entities, Business Logic)
  - Infrastructure (Data Access, External Services)
  - Tests (Unit and Integration tests)

### Development Tools
- **Docker** - Containerization (docker-compose for full stack)
- **Slidev** - Markdown-based presentation slides
- **Git** - Version control with branch-based exercises
- **OpenSpec** - Change proposal and specification system

### Package Managers
- **npm** - Frontend dependencies
- **NuGet** - Backend dependencies

## Project Conventions

### Code Style
- **TypeScript**: Strict mode enabled, prefer type inference where clear
- **C#**: Nullable reference types enabled, implicit usings enabled
- **Naming**:
  - C#: PascalCase for classes/methods, camelCase for parameters
  - TypeScript: camelCase for variables/functions, PascalCase for components
- **Line Length**: Keep reasonable (avoid excessive nesting)
- **Comments**: Only where logic isn't self-evident; prefer self-documenting code

### Architecture Patterns
- **Clean Architecture / DDD** (Domain-Driven Design)
  - Domain layer contains business logic (entities, value objects)
  - Application layer orchestrates use cases
  - Infrastructure handles external concerns (DB, APIs)
  - API layer handles HTTP concerns only
- **Dependency Inversion**: Dependencies flow inward (toward Domain)
- **Separation of Concerns**: Each layer has a single responsibility
- **CQRS-lite**: Separate read and write operations where beneficial

### Testing Strategy
- **Test Coverage Goal**: 75%+ for template-driven approach
- **Testing Pyramid**: More unit tests, fewer integration tests
- **Test Naming**: Descriptive names that explain the scenario
- **Test Organization**: Tests folder mirrors the project structure
- **Focus**: Business logic in Domain/Application layers should be well-tested

### Git Workflow
- **Branching Strategy**: Branch-based exercise structure
  - `main` - Base layer
  - `exercise-0-messy-legacy` - Brownfield starting point
  - `exercise-1-freeform` - Free-form approach
  - `exercise-2-ddd-guardrails` - DDD approach (current base for exercises 3+)
  - `exercise-3-prompt-engineering` - Template approach
  - `exercise-4-brownfield-migration` - Migration strategy
- **Commit Messages**:
  - Format: `<type>: <description>`
  - Types: feat, fix, chore, docs, refactor
  - Keep concise, focus on "why" not "what"
- **Merging**: Exercise branches build on each other incrementally

## Domain Context

### Educational Workshop Context
This is a **teaching tool**, not a production application. The chatbot is intentionally simple to focus on development process, not complex features.

### Vibecoding Philosophy
- **Vibecoding** = LLM-assisted coding with emphasis on conversational, iterative development
- Not about "AI replacing developers" - about developers using AI as a tool
- Focus on **replicable patterns** and **honest trade-offs**
- Templates guide LLMs to match your team's standards

### Key Concepts
- **Free-form vibecoding**: Fast but creates technical debt
- **Structured vibecoding**: Slower initial build, consistent maintenance
- **Template-driven**: Achieves both speed AND maintainability
- **Brownfield migration**: Gradual improvement using Strangler Fig pattern

### Success Metrics
Workshop succeeds when participants can:
1. Articulate when to use vibecoding (and when not to)
2. Demonstrate measurable speedup in appropriate scenarios
3. Create their own templates for common patterns
4. Evaluate trade-offs honestly

## Important Constraints

### Educational Requirements
- **Simplicity over completeness**: Features kept minimal to focus on process
- **Multiple implementations**: Same chatbot built 3+ ways for comparison
- **Reproducibility**: Workshop participants should get similar results
- **Honesty**: Must address limitations and failures, not just successes

### Technical Constraints
- **No authentication**: Kept simple for workshop context
- **In-memory storage**: No real database to reduce setup complexity
- **Local-first**: Should work without internet (after initial setup)
- **Cross-platform**: Must work on Windows, Mac, Linux

### Time Constraints
- **2-hour workshop**: Timing is critical
- **15-20 min hands-on builds**: Features must be achievable in this window
- **Quick setup**: Minimal dependencies, fast installation

## External Dependencies

### Required for Participants
- **Node.js 18+** - JavaScript runtime
- **.NET 9 SDK** - Backend runtime and build tools
- **Git** - Version control
- **LLM Access** - Claude, ChatGPT, or similar (participants bring their own)

### Optional
- **Docker & Docker Compose** - For containerized deployment
- **VS Code** - Recommended editor

### No External Services
- No databases (in-memory only)
- No cloud services
- No API keys required (except participant's LLM access)
- Self-contained workshop environment
