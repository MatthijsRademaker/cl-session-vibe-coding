# Exercise 4: Brownfield Migration Strategy

**Branch**: `exercise-4-brownfield-migration`

## The Problem

You've experienced both sides:

**Exercise 0** (Messy Legacy):
- Adding features takes 20-30 minutes
- 8-15 iterations with LLM
- Unclear where code should go
- Breaking existing functionality

**Exercise 3** (Clean Template):
- Same features take 8-12 minutes
- 3-5 iterations with LLM
- Clear structure
- Consistent results

**The gap**: LLMs are 2-3x slower on brownfield code.

**The question**: "How do I migrate my messy codebase?"

## Migration Strategies

### ❌ Strategy 1: Full Rewrite

```
Day 1: "Let's use the template and rewrite everything!"
Day 30: Still not done, business is blocked
Result: FAIL
```

**Why it fails**:
- Business can't wait months
- Can't freeze features during rewrite
- High risk of missing edge cases
- "Big bang" deployments are dangerous

**When to use**: Never for production systems.

### ❌ Strategy 2: Keep Adding to Mess

```
Day 1: "We'll clean it up later"
Day 365: Codebase is 3x messier
Result: Technical debt spiral
```

**Why it fails**:
- "Later" never comes
- Each feature makes structure worse
- LLM becomes less effective over time
- Eventually forces Strategy 1

**When to use**: Only if code is temporary (< 6 months lifespan).

### ✅ Strategy 3: Strangler Fig Pattern

**The approach that works**:

```
┌─────────────────────────────────────────┐
│ Legacy Codebase (Program.cs)            │
│ ┌─────────────┐                         │
│ │ /api/chat   │ ← OLD (still running)   │
│ └─────────────┘                         │
│                                         │
│ New Features (Clean Architecture)       │
│ ┌─────────────────────────────────────┐ │
│ │ /api/messages/reactions  ← NEW      │ │
│ │ /api/users/profiles      ← NEW      │ │
│ └─────────────────────────────────────┘ │
│                                         │
│ Over time, migrate old endpoints...     │
└─────────────────────────────────────────┘
```

**Key principles**:
1. **New features → Clean architecture** (use template)
2. **Old features → Leave as-is** (don't touch what works)
3. **Gradually migrate** (when you need to modify old code)
4. **Shared models** (bridge old and new)

Named after strangler fig trees that gradually replace their host.

## Step-by-Step Brownfield Migration

### Phase 1: Prepare the Ground (1-2 hours)

**Goal**: Set up clean architecture without touching legacy code.

```bash
# 1. Create clean project structure
backend/
  ├── Api/
  │   ├── Program.cs            # ← Legacy code stays
  │   └── Controllers/          # ← New controllers go here
  ├── Application/              # ← New use cases
  ├── Domain/                   # ← New entities
  └── Infrastructure/           # ← New repositories
```

**LLM Prompt**:
```
Using .prompts/NEW_FEATURE_TEMPLATE.md, create the project structure.

DON'T modify existing Program.cs
DON'T move existing endpoints
DO create empty folders for future features
DO set up dependency injection for new services
```

**Result**: Clean architecture **alongside** legacy, not replacing it.

---

### Phase 2: Add First New Feature (2-4 hours)

**Goal**: Prove the pattern works with a real feature.

**Choose**: A feature that DOESN'T require modifying legacy code.

**Example**: Message Reactions (new endpoint, doesn't change chat logic)

**LLM Prompt**:
```
Using .prompts/NEW_FEATURE_TEMPLATE.md, add message reactions.

Requirements:
- New endpoint: POST /api/messages/{id}/reactions
- Store reactions separately from legacy messages
- DON'T modify existing /api/chat endpoint
- Create bridge to connect with legacy message IDs

Follow clean architecture in Domain/Application/Infrastructure.
```

**Key technique**: **Bridging**

```csharp
// Legacy message ID from Program.cs
var legacyMessageId = 42;

// New domain entity references it
public class MessageReaction
{
    public int LegacyMessageId { get; set; }  // ← Bridge
    public string Emoji { get; set; }
}
```

**Result**: New feature with clean code, zero impact on legacy.

---

### Phase 3: Migrate When You Touch (Ongoing)

**Rule**: When you MUST modify old code, migrate that piece to new architecture.

**Example**: Bug in chat endpoint

```csharp
// BEFORE: Bug in Program.cs
app.MapPost("/api/chat", (ChatRequest request) =>
{
    // 50 lines of inline logic
    // BUG: Doesn't handle empty usernames
});

// AFTER: Migrate to controller
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly SendMessageUseCase _useCase;

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
    {
        // Clean architecture
        var result = await _useCase.Execute(request);
        return Ok(result);
    }
}

// Delete old endpoint from Program.cs
```

**LLM Prompt**:
```
Migrate the /api/chat endpoint from Program.cs to a ChatController.

Requirements:
- Follow .prompts/NEW_FEATURE_TEMPLATE.md structure
- Create SendMessageUseCase in Application/
- Move business logic to Domain/
- Write BDD tests
- Delete old endpoint after migration

Maintain exact same API contract (no breaking changes).
```

**Result**: Gradual migration, one endpoint at a time.

---

### Phase 4: Establish New Normal (Ongoing)

**Rules for the team**:

1. **All new features → Clean architecture**
   - Use the template
   - No exceptions

2. **Bug fixes in old code → Migrate that piece**
   - Fix bug in clean code
   - Delete legacy version

3. **Don't modify legacy unless necessary**
   - If it works, leave it
   - Focus new effort on new architecture

**Result**: Legacy shrinks naturally over time.

---

## Timeline Example

**Month 1**: Setup + First New Feature
- Week 1: Create project structure
- Week 2-3: Add message reactions (new, clean)
- Week 4: Add user profiles (new, clean)

**Month 2-3**: New Features + Opportunistic Migration
- New features use clean architecture
- Migrate 1-2 old endpoints when bugs found
- Legacy shrinks from 90% → 70%

**Month 4-6**: Accelerating Migration
- Most new work in clean architecture
- Team comfortable with pattern
- Legacy shrinks from 70% → 40%

**Month 12**: Mostly Clean
- 80% of code in clean architecture
- Remaining 20% is stable legacy (rarely touched)
- New features fast (8-12 min with LLM)

## Measuring Success

Track these metrics:

**Code distribution**:
```
Week 0:  [████████████████████] 100% Legacy
Week 12: [████████████░░░░░░░░]  60% Legacy, 40% Clean
Week 24: [████░░░░░░░░░░░░░░░░]  20% Legacy, 80% Clean
```

**Feature velocity** (time to add new feature):
```
Month 1: 25 min (mostly legacy)
Month 3: 18 min (half clean)
Month 6: 12 min (mostly clean)
```

**LLM iteration count** (rounds to get code working):
```
Month 1: 10 iterations (legacy code)
Month 6: 4 iterations (clean code)
```

## Common Challenges

### Challenge 1: "Should we migrate this endpoint?"

**Decision tree**:
```
Do you NEED to modify it?
├─ No  → Leave it (don't touch what works)
└─ Yes → Migrate it during the modification
```

**Example**:
- ✅ Migrate: Bug in /api/chat (need to modify anyway)
- ❌ Don't migrate: /api/health works fine (leave it)

---

### Challenge 2: "Shared data models"

**Problem**: Legacy and new code need same data.

**Solution**: Create adapter/bridge layer.

```csharp
// Legacy model (in Program.cs)
public class ChatMsg
{
    public int Id { get; set; }
    public string Content { get; set; }
}

// New domain entity
public class Message
{
    public MessageId Id { get; set; }
    public string Content { get; set; }
}

// Bridge/Adapter
public static class MessageAdapter
{
    public static Message FromLegacy(ChatMsg legacy)
    {
        return new Message(
            MessageId.From(legacy.Id),
            legacy.Content
        );
    }
}
```

**LLM Prompt**:
```
Create an adapter to convert between legacy ChatMsg and new Message entity.

Requirements:
- Static methods for two-way conversion
- Handle null cases
- Don't modify legacy model
```

---

### Challenge 3: "Team resists change"

**Problem**: "Why add complexity? Program.cs works fine!"

**Solution**: Prove value with metrics.

**Week 1**: Track time to add feature to legacy code (25 min, 12 iterations)
**Week 4**: Track time to add feature to clean code (10 min, 4 iterations)

**Show**: 2.5x speedup with clean architecture.

**Then**: Team sees benefit firsthand.

---

### Challenge 4: "LLM suggests breaking changes"

**Problem**: Migrated endpoint has different API contract.

**Solution**: Use LLM with strict constraints.

**Prompt**:
```
Migrate /api/chat endpoint to clean architecture.

CONSTRAINTS:
- API contract must stay EXACTLY the same
- Input: POST /api/chat with { message, userName }
- Output: { message, messageId }
- Status codes: 200 OK, 400 Bad Request

Test with existing frontend - no changes allowed.
```

**Verify**: Run old integration tests against new endpoint.

---

## Workshop Exercise

**Task**: Migrate one feature from Exercise 0 to Exercise 4.

### Setup (15 min)

1. Checkout exercise-0-messy-legacy
2. Create clean project structure (Application/, Domain/, Infrastructure/)
3. Set up dependency injection in Program.cs (don't delete old code yet)

### Migration (30 min)

4. Choose: Message history OR Health check endpoint
5. Use LLM with template to create clean version
6. Test both old and new endpoints work
7. Delete old endpoint
8. Verify frontend still works

### Reflection (15 min)

**Questions**:
- How long did migration take vs building from scratch?
- Did LLM help or hinder the migration?
- What would you do differently on your real codebase?
- Is strangler fig realistic for your team?

## Key Takeaways

**1. Don't rewrite, strangle**
- New features in clean architecture
- Migrate old code when you touch it
- Legacy shrinks gradually

**2. LLMs work better on clean code**
- Brownfield: 20-30 min, 10+ iterations
- Greenfield: 8-12 min, 3-5 iterations
- Migration cost is worth it for velocity

**3. Bridge don't break**
- Legacy and new code coexist
- Adapters handle shared data
- No big bang migration

**4. Prove value with metrics**
- Track feature velocity
- Show iteration count
- Demonstrate speedup to team

**5. Template enables migration**
- Without structure, LLM suggestions are inconsistent
- With template, LLM creates clean, predictable code
- Structure is investment in future speed

---

## Next Steps

**For this workshop**:
- ✅ Experienced brownfield pain (Exercise 0)
- ✅ Saw greenfield speed (Exercise 3)
- ✅ Learned migration strategy (Exercise 4)

**For your codebase**:
1. Pick ONE legacy file/module
2. Set up clean architecture alongside it
3. Add next feature using template
4. Measure time vs adding to legacy
5. Share results with team
6. Repeat

**Remember**: This is a marathon, not a sprint. Strangler fig takes time, but each week you get faster.

---

**Bottom line**: You can't instantly fix legacy code, but you can stop making it worse. Clean architecture + templates create a path forward where LLMs actually help instead of adding to the mess.
