# Exercise 2: DDD Architecture with Guardrails

## The Constraint
> "Create a chatbot using Domain-Driven Design architecture"

Same functionality as Exercise 1, but with strict architectural guardrails.

## What Changed from Exercise 1

### Backend Architecture: Complete Transformation

#### Exercise 1 (Free-form)
- Single `Program.cs` file
- Minimal API endpoints
- All logic inline
- No separation of concerns

#### Exercise 2 (DDD Guardrails)
```
backend/
├── Domain/              # Core business logic
│   ├── Entities/
│   │   ├── Conversation.cs    (Aggregate root)
│   │   └── Message.cs         (Value object)
│   ├── Interfaces/
│   │   └── IConversationRepository.cs
│   └── Services/
│       └── IChatbotService.cs (Domain service interface)
│
├── Application/         # Use cases
│   ├── DTOs/
│   │   ├── SendMessageRequest.cs
│   │   └── ChatResponse.cs
│   └── UseCases/
│       └── SendMessageUseCase.cs
│
├── Infrastructure/      # External concerns
│   ├── Repositories/
│   │   └── InMemoryConversationRepository.cs
│   └── Services/
│       └── RuleBasedChatbotService.cs
│
└── Api/                # HTTP endpoints
    ├── Controllers/
    │   └── ChatController.cs
    └── Program.cs      (DI configuration)
```

## Layer-by-Layer Breakdown

### 1. Domain Layer (Pure Business Logic)
**No dependencies on other layers**

#### `Conversation` (Aggregate Root)
- Manages collection of messages
- Encapsulates message adding logic
- Private setters for immutability
- Tracks conversation metadata (created, last message time)

```csharp
public Message AddMessage(string content, MessageSender sender)
{
    var message = new Message(content, sender);
    _messages.Add(message);
    LastMessageAt = DateTime.UtcNow;
    return message;
}
```

#### `Message` (Value Object)
- Validates content in constructor
- Immutable after creation
- Contains timestamp and sender info

#### `IConversationRepository` (Repository Interface)
- Defines contract for persistence
- Domain layer defines WHAT, not HOW

#### `IChatbotService` (Domain Service Interface)
- Abstracts response generation
- Domain concern (chatbot behavior) but implementation is infrastructure

### 2. Application Layer (Orchestration)
**Depends only on Domain**

#### `SendMessageUseCase`
- Coordinates the flow
- No business rules (those are in Domain)
- Pure orchestration:
  1. Get/create conversation
  2. Add user message
  3. Generate bot response
  4. Add bot message
  5. Persist changes
  6. Return DTO

```csharp
public async Task<ChatResponse> ExecuteAsync(SendMessageRequest request)
{
    // Orchestration only, no business logic
    var conversation = /* get or create */;
    conversation.AddMessage(request.Message, MessageSender.User);
    var botResponse = await _chatbotService.GenerateResponseAsync(request.Message);
    var botMessage = conversation.AddMessage(botResponse, MessageSender.Bot);
    await _conversationRepository.UpdateAsync(conversation);
    return new ChatResponse(...);
}
```

#### DTOs
- Data Transfer Objects for API communication
- Separate from domain entities
- Can have different shapes than domain model

### 3. Infrastructure Layer (Implementation Details)
**Depends on Domain and Application**

#### `InMemoryConversationRepository`
- Implements `IConversationRepository`
- Could be swapped for SQL, MongoDB, etc.
- Domain doesn't care about implementation

#### `RuleBasedChatbotService`
- Implements `IChatbotService`
- Same logic as Exercise 1
- Could be swapped for OpenAI, Anthropic, etc.
- Domain defines interface, Infrastructure implements it

### 4. API Layer (Entry Point)
**Depends on Application and Infrastructure**

#### `ChatController`
- Thin controller
- Delegates to use case
- Handles HTTP concerns only (status codes, error handling)

#### `Program.cs`
- Dependency injection configuration
- Wires up implementations to interfaces
- Layer dependencies enforced here

```csharp
// Infrastructure implementations
builder.Services.AddSingleton<IConversationRepository, InMemoryConversationRepository>();
builder.Services.AddScoped<IChatbotService, RuleBasedChatbotService>();

// Application use cases
builder.Services.AddScoped<SendMessageUseCase>();
```

## Key DDD Principles Applied

### 1. **Dependency Inversion**
- Domain defines interfaces
- Infrastructure implements them
- High-level modules don't depend on low-level modules

### 2. **Aggregate Root**
- `Conversation` controls all message access
- Messages can't exist without a conversation
- Encapsulation of business rules

### 3. **Value Objects**
- `Message` is immutable
- Validated in constructor
- No identity, just values

### 4. **Repository Pattern**
- Abstracts data access
- Domain doesn't know about databases
- Easy to swap implementations

### 5. **Use Cases**
- Single Responsibility
- Clear entry points
- Orchestration, not business logic

### 6. **Separation of Concerns**
- Each layer has ONE job
- Clear boundaries
- Testable in isolation

## Frontend Changes

Minimal - just updated API contract:

```typescript
// Old (Exercise 1)
body: JSON.stringify({ message: userInput })

// New (Exercise 2)
body: JSON.stringify({
  conversationId: null,
  message: userInput
})
```

Frontend still doesn't know about DDD - that's backend architecture.

## Trade-offs

### Pros of DDD Approach
✅ **Testability**: Each layer can be tested independently
✅ **Maintainability**: Clear where code belongs
✅ **Flexibility**: Easy to swap implementations (change DB, change chatbot logic)
✅ **Scalability**: Can split into microservices along layer boundaries
✅ **Domain Focus**: Business logic is isolated and clear
✅ **Team Collaboration**: Different devs can work on different layers

### Cons of DDD Approach
❌ **Complexity**: More files, more indirection
❌ **Overhead**: Overkill for simple CRUD
❌ **Learning Curve**: Team needs to understand DDD
❌ **Development Speed**: Slower to build initially
❌ **Boilerplate**: More interfaces, DTOs, mapping

## Comparison: Exercise 1 vs Exercise 2

| Aspect | Exercise 1 | Exercise 2 |
|--------|-----------|-----------|
| **Files** | 2 (Program.cs, ChatBot.vue) | 12+ files across 4 layers |
| **Lines of Code** | ~150 backend | ~400+ backend |
| **Setup Time** | Minutes | Hours (first time) |
| **Testability** | Hard (everything coupled) | Easy (layers isolated) |
| **Change DB** | Rewrite Program.cs | Swap repository implementation |
| **Change Bot Logic** | Modify inline code | Swap service implementation |
| **Add Feature** | Find where to put it (unclear) | Clear layer placement |
| **Onboarding** | Quick to understand | Requires DDD knowledge |

## When to Use Each Approach

### Use Exercise 1 Style When:
- Prototyping/MVP
- Team is small (1-3 devs)
- Requirements are unclear
- Project lifetime < 6 months
- Simple CRUD operations

### Use Exercise 2 Style When:
- Long-term production application
- Multiple teams/developers
- Complex business logic
- Need to change implementations frequently
- Testability is critical
- Domain experts involved in development

## Running the Application

### Backend
```bash
cd backend
dotnet run --project Api
# API at http://localhost:5000
# Swagger UI at http://localhost:5000/swagger
```

### Frontend
```bash
cd frontend
npm run dev
# App at http://localhost:5173
```

## Testing DDD Layers

The beauty of DDD is isolated testing:

```csharp
// Test domain logic (no dependencies)
var conversation = new Conversation();
conversation.AddMessage("Hello", MessageSender.User);
Assert.Equal(1, conversation.Messages.Count);

// Test use case with mocks
var mockRepo = new Mock<IConversationRepository>();
var mockChatbot = new Mock<IChatbotService>();
var useCase = new SendMessageUseCase(mockRepo.Object, mockChatbot.Object);
```

## Conclusion

Exercise 2 demonstrates how **architectural guardrails** change the LLM's output:

- Same functionality as Exercise 1
- Completely different structure
- More code, more complexity
- But also: more maintainable, more testable, more scalable

The guardrails forced the LLM to:
- Think about layers and dependencies
- Separate concerns properly
- Follow established patterns
- Create testable, modular code

**Next**: Exercise 3 will use prompt engineering templates to make this even more consistent and reusable.
