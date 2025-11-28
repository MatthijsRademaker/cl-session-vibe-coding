# Exercise 1: Free-form Vibecoding

## The Prompt
> "Create a chatbot"

That's it. No architectural guidance, no constraints, no specifications. Just build a chatbot.

## What the LLM Built

### Frontend (Vue 3 + TypeScript)
**File**: `frontend/src/components/ChatBot.vue`

The LLM autonomously decided to create:

#### UI/UX Decisions
- Modern chat interface with gradient header
- Message bubbles (user on right, bot on left)
- Timestamps on each message
- Typing indicator with animated dots
- Smooth slide-in animations for messages
- Auto-scroll to bottom on new messages
- Enter to send, Shift+Enter for new line
- Disabled state during message sending

#### Technical Decisions
- Composition API with `<script setup>`
- TypeScript interfaces for type safety (`Message` interface)
- Reactive state management with `ref()`
- Auto-incrementing message IDs
- Async/await for API calls
- Error handling with user-friendly messages
- `nextTick()` for DOM updates before scrolling

#### Styling Choices
- Purple gradient theme (#667eea to #764ba2)
- White message bubbles for bot, colored for user
- Glassmorphism effect on header (backdrop-filter)
- Responsive max-width container
- Box shadows for depth
- Smooth transitions and hover states

### Backend (.NET 8 Minimal API)
**File**: `backend/Api/Program.cs`

The LLM chose to implement:

#### Architecture Decisions
- Minimal API style (not controllers)
- Single POST endpoint: `/api/chat`
- Request/Response records for type safety
- CORS enabled for local development

#### Chatbot Logic
- Simple rule-based responses (no AI/ML)
- Pattern matching for common inputs:
  - Greetings: "hello", "hi", "hey"
  - Questions: "how are you", "what's up"
  - Help requests
  - Goodbyes
  - Questions (containing "?")
- Fallback to random generic responses
- Case-insensitive matching with `.ToLower()`

#### Response Strategy
- 10 pre-defined generic responses
- Context-aware responses for specific patterns
- Randomization for variety
- Friendly, conversational tone

## Key Observations

### What the LLM Did Well

1. **Complete working solution** - Both frontend and backend integrate seamlessly
2. **User experience** - Smooth animations, loading states, error handling
3. **Modern practices** - TypeScript, async/await, reactive programming
4. **Attention to detail** - Timestamps, typing indicators, auto-scroll
5. **Reasonable scope** - Simple but functional, not over-engineered

### What the LLM Chose (Without Being Asked)

- Chat UI instead of command-line interface
- Rule-based bot instead of API integration
- Specific color scheme and styling
- Message persistence in memory (no database)
- Time formatting
- Animation timing and easing
- Error messages
- CORS configuration
- Port numbers (5000, 5173)

### Potential Issues

- No message history persistence (lost on refresh)
- No user identification/sessions
- Very basic bot logic (not a real AI)
- No input validation/sanitization
- No rate limiting
- CORS wide open for development
- No conversation context/memory

### What's Missing

- User authentication
- Conversation history storage
- Real AI/LLM integration
- Multi-user support
- Message editing/deletion
- File attachments
- Rich text/markdown support

## Running the Application

### Backend
```bash
cd backend/Api
dotnet run
# API at http://localhost:5000
# Swagger UI at http://localhost:5000/swagger
```

### Frontend
```bash
cd frontend
npm run dev
# App at http://localhost:5173
```

## Testing

1. Open http://localhost:5173
2. Type "hello" - should get greeting response
3. Type "how are you?" - should get status response
4. Type "what do you think?" - should get random response
5. Watch for typing indicator
6. Observe message animations

## Conclusion

With zero architectural guidance, the LLM produced:

✅ **Good**:
- Working, polished UI
- Functional chatbot experience
- Modern development practices
- Good UX patterns

⚠️ **Trade-offs**:
- Made ALL decisions independently
- May not match your architecture
- Limited to LLM's "best guess" approach
- No consistency with existing patterns

**For Exercise 2**, we'll build the same chatbot but with DDD constraints to see how guardrails change the outcome.
