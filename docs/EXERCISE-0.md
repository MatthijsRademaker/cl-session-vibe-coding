# Exercise 0: The Messy Reality

**Branch**: `exercise-0-messy-legacy`

## Overview

This is the codebase most developers actually encounter: **legacy code with no structure**.

Unlike the clean greenfield examples in Exercise 1-3, this represents the brownfield reality:
- All backend code in one file (`Program.cs`)
- Mixed patterns and inconsistent naming
- TODOs and FIXMEs everywhere
- Commented-out code
- No tests
- Hardcoded values
- Unclear where to add new features

## What's Here

### Backend (`backend/Api/Program.cs`)
A working chatbot API with these "legacy code smells":

```csharp
// ❌ Everything in one file (138 lines)
// ❌ Global state (ConcurrentBag, static counter)
// ❌ Inline endpoint logic (no separation of concerns)
// ❌ Models mixed with application code
// ❌ TODOs that never get done
// ❌ Commented-out code
// ❌ Inconsistent error handling
// ❌ CORS copied from StackOverflow
// ❌ Hardcoded API URL
// ❌ No logging
// ❌ No tests
```

**Features**:
- POST `/api/chat` - Send message, get bot response
- GET `/api/messages` - Get message history
- GET `/api/health` - Health check

### Frontend (`frontend/src/App.vue`)
A single-file Vue component with similar issues:

```vue
// ❌ All logic in one component (230 lines)
// ❌ No composables or shared logic
// ❌ Hardcoded API URL
// ❌ setTimeout for scroll (race condition)
// ❌ Inconsistent error handling
// ❌ Copy-pasted formatTime function
// ❌ FIXME comments
// ❌ Old commented styles "for backwards compatibility"
```

## The Challenge

This is what you'll face in the **brownfield workshop section**.

### Task: Add Message Reactions
Try using an LLM to add emoji reactions (👍, ❤️, 😂) to messages.

**What you'll discover**:
1. **LLM doesn't know where to put code**
   - Should reactions go in `ChatMsg` model?
   - In the endpoint handler?
   - As a new endpoint?

2. **LLM suggests clean patterns that don't fit**
   - "Create a `ReactionService`" (but there are no services)
   - "Add to the repository layer" (but there's no repository)
   - "Write unit tests" (but there's no test infrastructure)

3. **LLM creates inconsistencies**
   - Uses `snake_case` when codebase uses `camelCase`
   - Suggests async when existing code is sync
   - Adds proper validation when existing code has none

4. **Context is too large**
   - All code in one file means LLM sees everything
   - Hard to focus on specific area
   - Easy to accidentally modify unrelated code

## Why This Matters

**Current workshop reality**:
- Exercise 1-3: "Vibecoding is fast!"
- Participants try at work: "Why is this so slow and error-prone?"

**Root cause**: Greenfield vs brownfield performance gap.

LLMs work great on:
- ✅ Clean structure
- ✅ Consistent patterns
- ✅ Small, focused files
- ✅ Clear separation of concerns

LLMs struggle with:
- ❌ Everything in one file
- ❌ Mixed patterns
- ❌ Unclear architecture
- ❌ Legacy code smells

## Expected Outcome

When participants try adding reactions to Exercise 0, they should experience:

**Time**: 20-30 minutes (vs 8-12 for Exercise 3)
**Iterations**: 8-15 rounds with LLM (vs 3-5 for Exercise 3)
**Friction**:
- LLM suggests creating new files (but should it?)
- Unclear where reactions belong
- Breaking existing functionality
- Inconsistent with existing style

**Key insight**: "Oh, that's why the template matters - it prevents this mess!"

## Real-World Brownfield

This exercise is deliberately messy but **realistic**:

Common legacy patterns:
- ✅ God file/class with all logic
- ✅ Global state
- ✅ Mixed sync/async
- ✅ No tests
- ✅ Copy-pasted code
- ✅ Commented-out experiments
- ✅ TODOs from 2 years ago
- ✅ Hardcoded URLs and magic strings

This is what participants will face on **real projects**.

## Workshop Flow

### 1. Opening (5 min)
Show Exercise 0 code:
- "This is what most codebases look like"
- Point out the mess
- "Let's try adding a feature with an LLM"

### 2. Hands-On Struggle (15 min)
Participants try adding message reactions:
- Experience the friction firsthand
- See LLM suggestions that don't fit
- Multiple iterations to get it working

### 3. Discussion (10 min)
**Prompts**:
- "How many iterations did it take?"
- "Did the LLM suggest patterns that didn't fit?"
- "Would you ship this code?"
- "How is this different from what you expected?"

### 4. The Contrast
**Then** show Exercise 3 (template-driven greenfield):
- Same feature, clean codebase
- Much faster, fewer iterations
- Clear where everything goes

**Key takeaway**: "Structure enables speed. Templates create structure."

## What Participants Learn

1. **LLMs amplify architecture**
   - Good structure → fast, consistent results
   - Bad structure → slow, inconsistent results

2. **Brownfield requires different strategy**
   - Can't just "add features" to legacy code
   - Need to refactor first OR accept slower pace
   - Templates help, but can't fix fundamental mess

3. **Template value becomes obvious**
   - Not about being pedantic
   - It's about **making LLMs effective**
   - Structure is scaffolding for AI assistance

4. **Realistic expectations**
   - Vibecoding isn't magic on messy codebases
   - You need either: clean code OR more time
   - The 3x speedup depends on architecture quality

## Instructor Notes

### Common Questions

**"Should we refactor this first?"**
→ Great question! That's Exercise 4.

**"Why not just create new clean files?"**
→ Because legacy integration is hard. You still need to connect to this mess.

**"Is this exaggerated?"**
→ No. Many production codebases are worse.

**"How do you prevent this?"**
→ That's what the template in Exercise 3 helps with.

### Troubleshooting

**If participants finish too quickly**:
- Ask them to add a second feature (user profiles)
- See how each feature makes the mess worse

**If participants get stuck**:
- That's the point! Let them struggle a bit
- This builds empathy for the brownfield challenge

**If participants say "I'd refactor first"**:
- Perfect! That leads to Exercise 4 discussion

## Next Steps

After Exercise 0, the workshop flow is:

1. ✅ **Exercise 0**: Experience brownfield pain
2. → **Exercise 3**: See greenfield speed (for contrast)
3. → **Exercise 4**: Learn brownfield migration strategy

This creates the narrative:
- "I tried on messy code → it was slow"
- "I tried on clean code → it was fast"
- "How do I handle my messy codebase?" → Exercise 4

---

**Bottom line**: Exercise 0 makes participants go "Oh, THAT'S why structure matters for LLMs!"

It transforms templates from "nice to have" to "essential for speed."
