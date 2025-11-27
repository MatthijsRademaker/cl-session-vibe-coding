# Vibecoding Workshop

A hands-on workshop for experienced developers to explore different approaches to LLM-assisted coding.

## Workshop Goal

Show skeptical developers the differences between:
1. **Unconstrained LLM** - Let it make all decisions
2. **Guided LLM** - Provide architectural guardrails
3. **Prompted LLM** - Use structured, reusable prompts

## The Exercise: Building a Chatbot

Each exercise implements the same chatbot feature with different levels of LLM guidance.

### Structure

```
cl-sessie/
├── frontend/          # Vue 3 + TypeScript + Vite
├── backend/Api/       # .NET 8 Web API
└── CLAUDE.md          # Instructions for Claude Code
```

## Getting Started

### Prerequisites
- Node.js 18+
- .NET 8 SDK

### Base Layer (Current State)

This is the starting point - minimal scaffolding for both frontend and backend.

**Frontend:**
```bash
cd frontend
npm install
npm run dev
```

**Backend:**
```bash
cd backend/Api
dotnet run
```

Both are vanilla scaffolds with no custom code.

## Exercises

Each exercise will be on a separate branch:
- `base` - Minimal scaffolding (current)
- `exercise-1` - Free-form LLM implementation
- `exercise-2` - DDD-guided implementation
- `exercise-3` - Prompt-engineered implementation

## Workshop Flow

1. Start with base layer
2. Switch to exercise branch
3. Review what the LLM produced
4. Compare approaches
5. Discuss trade-offs

See `CLAUDE.md` for detailed architecture and guidelines.
