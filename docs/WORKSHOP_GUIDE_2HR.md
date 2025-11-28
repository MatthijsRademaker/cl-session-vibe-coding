# 2-Hour Vibecoding Workshop Guide

**Target Audience**: Experienced developers skeptical about LLM-assisted coding
**Duration**: 2 hours (including talk time)
**Format**: Demo-heavy with guided discussion

---

## Workshop Flow

### 1. Opening: The Hook (15 min)

**Goal**: Show what vibecoding looks like in action

#### Setup
- Open Claude Code or similar LLM tool
- Have empty Vue + .NET projects ready (master branch)
- Share screen

#### Demo Script
```
"I'm going to build a chatbot feature. Watch the conversation."

[Type in LLM]:
"Create a chatbot component in Vue 3 with TypeScript.
Backend should be .NET minimal API. Keep it simple."

[Let it build, fix one bug in real-time]

"This took 15 minutes. Now here's the question:
Is this good code? Would you ship this?"
```

#### Transition
- "Let's examine what we just built"
- Switch to `exercise-1-freeform` branch
- "This is the result of free-form vibecoding"

**Key Message**: Fast, but is it right?

---

### 2. Exercise 1 Deep Dive (20 min)

**Goal**: Understand free-form vibecoding results

#### What to Show

1. **The Code** (5 min)
   ```bash
   # Show structure
   cat backend/Api/Program.cs  # All in one file
   cat frontend/src/components/ChatBot.vue  # 200 LOC component
   ```

   **Discussion prompts**:
   - "Where would you add authentication?"
   - "Where are the business rules?"
   - "How would you test this?"

2. **The Transcript** (10 min)
   - Open `transcripts/EXERCISE-1-TRANSCRIPT.md`
   - Walk through iterations 1-3
   - Show how bugs were found and fixed

   **Highlight**:
   - Iteration 2: Enter key bug
   - Iteration 3: Auto-scroll issue
   - Iteration 5: TypeScript types fixed

   **Key Message**: "Even 'simple' code takes iteration"

3. **The Concerns** (5 min)
   - All logic in one file (Program.cs)
   - No separation of concerns
   - No tests
   - Hard to find where to add features

   **Ask participants**:
   - "What worries you about this code?"
   - "Would you merge this PR?"

---

### 3. Exercise 3 Demo: Template-Driven (30 min)

**Goal**: Show how templates guide LLM output

#### Setup (5 min)
- Switch to `exercise-3-prompt-engineering` branch
- Open `.prompts/NEW_FEATURE_TEMPLATE.md`

**Say**: "Instead of free-form, we give the LLM a detailed template"

#### Show the Template (10 min)

**Key sections to highlight**:

1. **Architecture Guidance**
   ```markdown
   ## Backend Structure

   Domain/Features/{FeatureName}/
     ├── {Entity}.cs           # Domain model
     ├── I{Repository}.cs      # Interface
     └── {UseCase}.cs          # Business logic
   ```

2. **Quality Requirements**
   - Validation rules
   - Error handling patterns
   - Testing requirements (BDD with Gherkin)

3. **The Example**
   ```gherkin
   Feature: Chat
     Scenario: Send a message
       Given I have a conversation
       When I send message "Hello"
       Then I should receive a response
   ```

**Key Message**: "The template IS the guardrails"

#### Live Demo (15 min)

**Scenario**: "Let's add a simple feature using this template"

Pick one:
- Message timestamps
- User typing indicator
- Message character limit

```
[Type in LLM]:
"Using the NEW_FEATURE_TEMPLATE.md, add message timestamps
to the chat feature. Follow the vertical slice architecture."

[Let it generate]
[Show the results]
```

**What to show**:
- Files created follow the structure
- Tests are included
- Clean separation of concerns

**Key Message**: "Same speed, better structure"

---

### 4. Side-by-Side Comparison (25 min)

**Goal**: Evidence-based discussion of trade-offs

#### The Metrics (10 min)

Open `METRICS.md`, show table:

| Metric | Exercise 1 | Exercise 3 |
|--------|-----------|-----------|
| Initial build | 15 min | 20 min |
| Files created | 2 | 19 |
| Test coverage | 0% | 75% |
| Add new feature | 10-15 min | 8-12 min |

**Walk through**:
1. **Speed**: "Exercise 1 is faster initially"
2. **Structure**: "Exercise 3 has more files but organized"
3. **Tests**: "Exercise 3 includes tests automatically"
4. **Long-term**: "After 12 features, Exercise 3 is faster"

**Discussion prompt**: "When would you choose each approach?"

#### Maintenance Challenge (15 min)

**Pick one challenge from MAINTENANCE_CHALLENGES.md**:

**Challenge 1: Add Message Timestamps**

```markdown
Exercise 1 Difficulty: ⭐⭐⭐ (medium-hard)
- Find where messages are created (inline in Program.cs)
- Add timestamp field
- Update frontend display
- Risk: Might break other logic in same file

Exercise 3 Difficulty: ⭐ (easy)
- Update Domain/Features/Chat/Message.cs
- Update Application/UseCases/SendMessageUseCase.cs
- Update Frontend/features/chat/ChatMessage.vue
- Tests verify nothing broke
```

**Activity**:
1. "Let's look at Exercise 1 code - where would you add this?"
2. "Now Exercise 3 - where would you add this?"
3. "Which was easier to find?"

**Key Message**: "Initial speed vs. maintenance speed"

---

### 5. Pitfalls & Assumptions (20 min)

**Goal**: Build credibility with honesty

#### Open `WHEN_NOT_TO_USE.md`

**Walk through key pitfalls**:

1. **❌ Don't Use LLMs For**:
   - Novel algorithms ("It'll give you bubble sort for everything")
   - Security-critical code ("Always review auth logic yourself")
   - Performance-critical code ("It prioritizes readability")

   **Real example**: "I asked for file upload. It used in-memory buffering. Would have crashed on large files."

2. **⚠️ Common Assumptions LLMs Make**:
   - REST over GraphQL
   - Synchronous over async
   - In-memory over distributed

   **Key Message**: "You need to know enough to catch these"

3. **🎯 Skill Atrophy Concerns** (most important):
   ```markdown
   - If you ONLY vibecode: Yes, skills fade
   - If you use it as a tool: No, you learn faster
   ```

   **Discussion prompt**: "How would you prevent skill atrophy?"

   **Suggestions**:
   - Review every line generated
   - Do code reviews as a team
   - Build some things from scratch
   - Use it for boilerplate, not learning

#### Red Flags (5 min)

**When to be extra careful**:
- Authentication/authorization
- Payment processing
- Database migrations
- Performance-critical paths
- Complex algorithms

**Key Message**: "Vibecoding is a tool, not autopilot"

---

### 6. Takeaways & Q&A (10 min)

**Goal**: Actionable next steps

#### What You Learned

1. **Free-form vibecoding**:
   - ✅ Fast prototypes
   - ⚠️ Technical debt
   - 📊 Best for: MVPs, demos, throwaway code

2. **Template-driven vibecoding**:
   - ✅ Fast AND maintainable
   - ✅ Includes tests
   - 📊 Best for: Production code, team projects

3. **The Template is Key**:
   - Create templates for YOUR patterns
   - Encode YOUR architecture decisions
   - Guide the LLM to YOUR standards

#### Next Steps

**For Skeptics**:
- ✅ Try it on a side project first
- ✅ Create one template for a pattern you use often
- ✅ Review every line the LLM generates
- ❌ Don't ship code you don't understand

**For Believers**:
- ⚠️ Read WHEN_NOT_TO_USE.md carefully
- ⚠️ Watch for the assumptions LLMs make
- ⚠️ Keep practicing fundamentals

#### Q&A (5 min)

**Common questions**:

**Q**: "Will this replace junior developers?"
**A**: "No. You need to understand code to review it. But it changes what we spend time on."

**Q**: "What about proprietary business logic?"
**A**: "LLMs don't know your domain. You still write the business rules, LLM writes the boilerplate."

**Q**: "Which LLM should I use?"
**A**: "Claude, GPT-4, GitHub Copilot - try a few. Each has strengths."

**Q**: "How do I convince my team?"
**A**: "Show them this workshop. Let them try it. Make your own decision."

---

## Quick Reference for Instructors

### Pre-Workshop Setup
```bash
# Have these branches ready
git checkout master                     # Empty scaffolds
git checkout exercise-1-freeform       # Free-form result
git checkout exercise-3-prompt-engineering  # Template result

# Test the apps work
cd frontend && npm run dev              # http://localhost:5173
cd backend/Api && dotnet run            # http://localhost:5000
```

### Key Files to Have Open
1. `transcripts/EXERCISE-1-TRANSCRIPT.md` - Show the iterations
2. `.prompts/NEW_FEATURE_TEMPLATE.md` - The template
3. `METRICS.md` - The data table
4. `WHEN_NOT_TO_USE.md` - The honest discussion
5. `MAINTENANCE_CHALLENGES.md` - Pick one challenge

### Timing Checkpoints
- ⏰ 15 min: Done with opening demo
- ⏰ 35 min: Done with Exercise 1 review
- ⏰ 65 min: Done with Exercise 3 demo
- ⏰ 90 min: Done with comparison
- ⏰ 110 min: Done with pitfalls
- ⏰ 120 min: Workshop complete

### If You're Running Over Time

**Cut**:
- Exercise 2 mention (just say "there's a middle ground")
- Live demo in Exercise 3 (just show the result)
- Multiple maintenance challenges (pick one)

**Keep**:
- Opening demo (the hook)
- Exercise 1 transcript (shows iteration)
- Template walkthrough (the key insight)
- Metrics comparison (the evidence)
- Pitfalls discussion (builds credibility)

---

## Success Criteria

**Participants should leave with**:

1. ✅ **Understanding**: "I see when vibecoding is useful and when it's not"
2. ✅ **Evidence**: "I saw the data comparing approaches"
3. ✅ **Tool**: "I have a template I can use"
4. ✅ **Skepticism**: "I know what to watch out for"

**NOT**:
- ❌ "AI will solve everything" (naive)
- ❌ "I'll never write code again" (dangerous)
- ❌ "This is just hype" (hasn't seen evidence)

---

## Follow-Up Materials

**Share with participants**:
- Link to this repo (MIT license, free to use)
- `NEW_FEATURE_TEMPLATE.md` (they can copy/modify)
- `WHEN_NOT_TO_USE.md` (share with skeptical colleagues)
- `METRICS.md` (show management)

**Encourage them to**:
- Try it on a side project this week
- Create a template for one pattern they use often
- Share their experience (what worked, what didn't)

---

## Customization Tips

### For Different Tech Stacks
- Replace Vue/C# with your stack
- Keep the same architectural patterns
- Update the template for your conventions

### For Different Experience Levels

**Senior Engineers**:
- Focus on architecture comparisons
- Deep dive into trade-offs
- Discuss team adoption strategies

**Tech Leads**:
- Emphasize long-term costs
- Show 1-year projections
- Discuss template governance

**Skeptics**:
- Lead with pitfalls first
- Show honest limitations upfront
- Focus on when NOT to use

---

## Common Pitfalls (For Instructors)

### Don't
- ❌ Oversell the benefits ("AI will replace X")
- ❌ Ignore the downsides
- ❌ Skip the maintenance challenge (it's the proof)
- ❌ Rush through the template (it's the key insight)

### Do
- ✅ Be honest about limitations
- ✅ Show real code, not slides
- ✅ Let participants voice concerns
- ✅ Acknowledge valid criticisms

---

## Summary

**This 2-hour workshop**:
- ✅ Shows, doesn't just tell
- ✅ Uses evidence, not hype
- ✅ Honest about limitations
- ✅ Gives actionable takeaways

**The key insight**: "Templates turn vibecoding from fast-but-messy into fast-AND-maintainable."

**The honest caveat**: "But it's not magic. You still need to understand the code."

---

*For questions or customization help, see README.md or open an issue.*
