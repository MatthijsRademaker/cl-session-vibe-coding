# Exercise 3: Prompt Engineering with Templates

## The Approach
Instead of giving vague instructions, we provide the LLM with **detailed, reusable templates** that specify exactly how to implement features.

## The Template

See `.prompts/NEW_FEATURE_TEMPLATE.md` for the comprehensive template.

### What the Template Provides

1. **Architecture Overview**
   - Vertical slice structure (feature-based organization)
   - Clear folder hierarchy
   - Frontend modularity guidelines

2. **Step-by-Step Implementation Guide**
   - Domain layer (pure business logic)
   - Application layer (use cases/handlers)
   - Infrastructure layer (implementations)
   - API layer (controllers)
   - BDD tests (Reqnroll feature files)
   - Frontend modules (components, composables, API clients)

3. **Code Examples for Every Layer**
   - Complete, working code samples
   - Naming conventions
   - Best practices
   - Common patterns

4. **Testing Template**
   - Gherkin feature file structure
   - Step definition examples
   - Testing checklist

5. **Quality Standards**
   - Naming conventions
   - File organization rules
   - Code quality guidelines

## Key Difference: Vertical Slices

### Exercise 2 (Horizontal Layers)
```
Domain/
  Entities/
  Interfaces/
  Services/
Application/
  DTOs/
  UseCases/
```

### Exercise 3 (Vertical Slices)
```
Domain/
  Features/
    Notifications/
      Entities/
      Interfaces/
    Chat/
      Entities/
      Interfaces/
Application/
  Features/
    Notifications/
      Commands/
      Handlers/
```

**Why Vertical Slices?**
- ✅ Each feature is self-contained
- ✅ Easy to find all code related to a feature
- ✅ Can delete entire feature by removing folder
- ✅ Reduces merge conflicts (teams work on different features)
- ✅ Clearer boundaries for microservices

## Template Benefits

### 1. Consistency
Every feature follows the same pattern:
- Same folder structure
- Same naming conventions
- Same testing approach
- Same documentation style

### 2. Speed
Developers (and LLMs) don't need to decide:
- Where to put files
- How to name things
- What patterns to use
- How to test

### 3. Quality
Template enforces:
- Separation of concerns
- Dependency inversion
- Testability
- Best practices

### 4. Onboarding
New developers can:
- Look at template
- See working examples
- Copy and modify
- Be productive immediately

### 5. Scalability
As codebase grows:
- Features stay isolated
- No "god classes"
- Clear modularization
- Easy to refactor

## Using the Template

### Prompt Format

```
I need to implement a new feature: [FEATURE_NAME]

Requirements:
- [Requirement 1]
- [Requirement 2]
- [Requirement 3]

Please follow the template in .prompts/NEW_FEATURE_TEMPLATE.md exactly.
Implement:
1. Domain layer with entity and repository interface
2. Application layer with command/handler
3. Infrastructure layer with repository implementation
4. API controller
5. BDD feature file with scenarios
6. Frontend module (components, composable, API client)
```

### Example: Notifications Feature

**Prompt**:
```
I need to implement a notifications feature that allows the backend to send
toast notifications to the frontend.

Requirements:
- Notifications have: message, type (Info/Warning/Success/Error), timestamp
- Backend can create notifications
- Frontend can fetch unread notifications
- Notifications can be marked as read
- Real-time streaming (Server-Sent Events)

Follow .prompts/NEW_FEATURE_TEMPLATE.md exactly.
```

**LLM Output** (following template):
- ✅ `Domain/Features/Notifications/Entities/Notification.cs`
- ✅ `Domain/Features/Notifications/Interfaces/INotificationRepository.cs`
- ✅ `Application/Features/Notifications/Commands/CreateNotificationCommand.cs`
- ✅ `Application/Features/Notifications/Handlers/CreateNotificationHandler.cs`
- ✅ `Infrastructure/Features/Notifications/Repositories/InMemoryNotificationRepository.cs`
- ✅ `Api/Features/Notifications/Controllers/NotificationsController.cs`
- ✅ `Tests/Features/Notifications/Notifications.feature` (BDD)
- ✅ `frontend/src/features/notifications/` (complete module)

## Comparison Across Exercises

| Aspect | Exercise 1 | Exercise 2 | Exercise 3 |
|--------|-----------|-----------|-----------|
| **Guidance** | "Create chatbot" | "Use DDD" | Detailed template |
| **Structure** | Inline code | Horizontal layers | Vertical slices |
| **Consistency** | LLM decides | Some patterns | Enforced by template |
| **Testability** | None | Some tests | BDD + unit tests |
| **Frontend** | Monolithic | Shared components | Feature modules |
| **Scalability** | Low | Medium | High |
| **Learning Curve** | None | DDD knowledge | Template reading |
| **Speed (first feature)** | Fast | Slow | Medium |
| **Speed (subsequent features)** | Fast | Medium | Fast (copy template) |

## Benefits of Prompt Engineering

### For Solo Developers
- Consistent codebase even working alone
- Don't need to remember all patterns
- Quick reference guide
- Faster feature development

### For Teams
- Everyone follows same structure
- Less code review time
- Easier to understand others' code
- Reduced onboarding time

### For LLM Assistance
- Precise instructions
- Reproducible output
- Less back-and-forth
- Higher quality code

## Template Evolution

Templates can evolve with the project:

1. **Start simple** - Basic structure
2. **Add learnings** - Common patterns emerge
3. **Refine** - Best practices solidify
4. **Expand** - New sections as needed
5. **Prune** - Remove outdated patterns

## When to Use Templates

### Good For
- ✅ Repetitive features (CRUD, APIs)
- ✅ Team projects
- ✅ Long-term codebases
- ✅ Multiple developers
- ✅ LLM-assisted development

### Not Needed For
- ❌ One-off prototypes
- ❌ Experimental features
- ❌ Solo hobby projects
- ❌ Highly unique domains

## Conclusion

Exercise 3 demonstrates that **prompt engineering** is the key to effective LLM assistance:

1. **Exercise 1**: "Do whatever" → Fast but inconsistent
2. **Exercise 2**: "Follow DDD" → Better but requires expertise
3. **Exercise 3**: "Follow this template" → Best of both worlds

**The Template Approach**:
- Combines speed of Exercise 1
- With quality of Exercise 2
- Adds consistency and scalability
- Makes LLMs more effective
- Creates reusable knowledge

**Key Insight**: The better your prompts (templates), the better your code. Invest in prompt engineering to multiply your productivity.
