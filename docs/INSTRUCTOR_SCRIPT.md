# Instructor Script - Vibecoding Workshop (2 hours)

**Use this as your presentation notes during the workshop**

---

## Pre-Workshop Checklist

```bash
# Start apps before participants arrive
cd frontend && npm run dev  # http://localhost:5173
cd backend/Api && dotnet run  # http://localhost:5000

# Have these open in tabs:
# - LLM tool (Claude Code / ChatGPT / etc.)
# - This script
# - transcripts/EXERCISE-1-TRANSCRIPT.md
# - .prompts/NEW_FEATURE_TEMPLATE.md
# - METRICS.md
# - WHEN_NOT_TO_USE.md

# Have these branches ready:
git branch
# ✓ master
# ✓ exercise-1-freeform
# ✓ exercise-3-prompt-engineering
```

---

## Section 1: Opening Demo (15 min) ⏰ 0:00-0:15

### Opening Line

> "I'm going to show you something that will either excite you or terrify you. You'll decide which."

### The Demo (10 min)

**Goal**: Build chatbot live with free-form prompt

**What to do**:
1. Ensure you're on `master` branch (empty scaffolds)
2. Share screen with LLM tool visible
3. Type this prompt:

```
Create a chatbot feature for this Vue 3 + .NET 8 app.

Frontend:
- ChatBot.vue component with message list and input
- TypeScript types
- Modern styling

Backend:
- Minimal API endpoint /api/chat
- Rule-based responses (no external APIs)
- Return bot response based on user message

Keep it simple.
```

4. Let LLM generate (should take ~30 seconds)
5. Copy code into your project
6. Run it, show it works
7. Find one bug (refresh page, test typing)
8. Ask LLM to fix it

**What to say while it generates**:
- "Notice I didn't specify architecture"
- "I didn't mention tests"
- "Just asked for a chatbot"

### The Hook (5 min)

**After it's working, ask**:

> "This took 15 minutes. Is this good code?"

**Pause for answers**, then:

> "That's what we're here to figure out. Not 'is this amazing' or 'is this terrible' - but **when is this appropriate**?"

**Transition**:
- "Let's examine what we just built"
- Switch to `exercise-1-freeform` branch
- "This is the actual result of free-form vibecoding"

---

## Section 2: Exercise 1 Deep Dive (20 min) ⏰ 0:15-0:35

### Show the Code (5 min)

**Files to show**:

1. **backend/Api/Program.cs**
   ```bash
   wc -l backend/Api/Program.cs  # ~150 lines, all in one file
   ```

   **Say**: "All the logic is here. API, business rules, response generation - everything."

   **Point out**:
   - Lines 10-50: The endpoint definition
   - Lines 20-40: All the business logic inline
   - No separation of concerns

2. **frontend/src/components/ChatBot.vue**
   ```bash
   wc -l frontend/src/components/ChatBot.vue  # ~200 lines
   ```

   **Say**: "Frontend is actually pretty reasonable. One component, clean structure."

### Discussion Prompts (5 min)

**Ask the group**:

1. "Where would you add user authentication?"
   - (Let them search... it's hard to find where it should go)
   - **Point**: No clear architecture

2. "Where are the business rules?"
   - (They're inline with HTTP handling)
   - **Point**: Mixing concerns

3. "How would you test this?"
   - (You can't easily - needs full HTTP setup)
   - **Point**: Not testable in isolation

**Key Message**:
> "Fast to write, but maintainability questions arise immediately."

### The Transcript (10 min)

Open `transcripts/EXERCISE-1-TRANSCRIPT.md`

**Say**: "Let's see HOW this was built. This is the actual conversation."

**Walk through**:

**Iteration 1** (lines 1-50):
- Initial prompt
- LLM generates code
- "Looks good, right?"

**Iteration 2** (lines 51-100):
- Bug found: Enter key doesn't work
- Conversation: "The Enter key doesn't work to send messages"
- LLM fixes it
- **Say**: "Notice how quickly bugs are fixed"

**Iteration 3** (lines 101-150):
- New issue: Messages don't auto-scroll
- More refinement
- **Say**: "Even 'simple' features take iteration"

**Key Message**:
> "The transcript shows something important: vibecoding is iterative, not magic. You still find bugs, you still refine."

---

## Section 3: Exercise 3 Demo (30 min) ⏰ 0:35-1:05

### The Problem Statement (2 min)

> "Exercise 1 was fast but concerning. What if we could guide the LLM to produce better structure?"

**Transition**: Switch to `exercise-3-prompt-engineering` branch

### Show the Template (12 min)

Open `.prompts/NEW_FEATURE_TEMPLATE.md`

**Say**: "This is the game-changer. Not a free-form prompt, but a detailed template."

**Sections to highlight**:

#### 1. Architecture Guidance (3 min)
Scroll to backend structure section:

```markdown
Domain/Features/{FeatureName}/
  ├── {Entity}.cs           # Domain model
  ├── I{Repository}.cs      # Interface
  └── {UseCase}.cs          # Business logic
```

**Say**:
- "We tell the LLM exactly where files go"
- "No guessing, no inline logic"
- "Vertical slice architecture - each feature is isolated"

#### 2. Quality Requirements (4 min)
Scroll to quality section:

**Point out**:
- Validation rules required
- Error handling patterns specified
- Null checks enforced
- **Say**: "We encode our standards into the template"

#### 3. Testing Requirements (5 min)
Scroll to BDD section:

```gherkin
Feature: Chat
  Scenario: Send a message
    Given I have a conversation
    When I send message "Hello"
    Then I should receive a response
```

**Say**:
- "Template requires Gherkin scenarios"
- "Tests aren't optional - they're part of the template"
- "LLM generates both code AND tests"

**Key Message**:
> "The template IS the guardrails. We're not hoping for good structure - we're requiring it."

### Live Demo (Optional, 12 min)

**If time permits**, build a small feature:

**Pick one**:
- Message timestamps
- Character limit (200 chars)
- Typing indicator

**Prompt**:
```
Using .prompts/NEW_FEATURE_TEMPLATE.md, add message timestamps
to the chat feature. Follow the vertical slice architecture
and include BDD tests.
```

**Let it generate**, then show:
- Files created follow structure
- Tests are included
- Clean separation

**If short on time**: Skip live demo, just show existing Exercise 3 code structure

### Show Exercise 3 Code (6 min)

```bash
# Show the structure
tree backend/Domain/Features/Chat
tree backend/Application/Features/Chat
```

**Point out**:
- Each feature is isolated
- Clear where things go
- Tests included

**Say**: "Same chatbot, same functionality, but structured for growth."

---

## Section 4: Comparison (25 min) ⏰ 1:05-1:30

### The Metrics (10 min)

Open `METRICS.md`

#### Speed Comparison (3 min)

Show table (lines 9-16):

| Metric | Exercise 1 | Exercise 3 |
|--------|-----------|-----------|
| Initial build | 15 min | 20 min |
| Add new feature | 10-15 min | 8-12 min |

**Say**:
- "Exercise 1 is faster initially - 5 minutes saved"
- "But Exercise 3 is faster for new features"
- "Break-even point: after ~12 features"

**Key Message**: "Initial speed vs. sustained speed"

#### Code Volume (3 min)

Show table (lines 25-37):

| Total Project LOC | Exercise 1: 350 | Exercise 3: 1430 |

**Pause for reaction**, then:

**Say**:
- "Yes, Exercise 3 is 4x more code"
- "But that includes tests (300 LOC), documentation (200 LOC)"
- "Actual business logic is similar"

**Ask**: "Is more code always worse?"

#### Long-Term Costs (4 min)

Scroll to 1-year projection (lines 166-176):

| Total Time | Exercise 1: ~16 weeks | Exercise 3: ~11 weeks |

**Say**:
- "Over one year, Exercise 3 saves 5 weeks"
- "That's more than a month of developer time"
- "Initial 5 minutes vs. 5 weeks saved"

**Key Message**: "Think beyond the first feature"

### Maintenance Challenge (15 min)

**Say**: "Let's make this concrete. I want you to think through a real scenario."

**Challenge**: Add message timestamps

#### Exercise 1 (7 min)

**On screen**: Open `backend/Api/Program.cs` from exercise-1 branch

**Ask the group**:
1. "Where would you add the timestamp to messages?"
   - (Let them look... it's in the inline logic around line 30)

2. "What could break?"
   - Answer: Other logic in same file
   - No tests to catch it

3. "How long would this take?"
   - Answer: ~15-20 minutes (find code, change it, manual test)

**Show**: All logic is intertwined in one endpoint

#### Exercise 3 (8 min)

**Switch to**: `exercise-3-prompt-engineering` branch

**Ask the group**:
1. "Where would you add the timestamp?"
   - Answer: `Domain/Features/Chat/Message.cs` (obvious)

2. "What else needs to change?"
   - Answer: UseCase, Frontend component
   - Tests verify integration

3. "How long would this take?"
   - Answer: ~8-10 minutes
   - Tests catch any mistakes

**Show**: Structure makes changes obvious

**Key Message**:
> "Architecture isn't about being 'proper' - it's about **making changes easy**."

---

## Section 5: Pitfalls & Assumptions (20 min) ⏰ 1:30-1:50

### Opening (1 min)

**Say**:
> "Time for honesty. LLMs aren't magic. Let's talk about when this goes wrong."

Open `WHEN_NOT_TO_USE.md`

### When NOT to Use (8 min)

**Walk through the top items** (lines 10-40):

#### 1. Novel Algorithms
**Say**: "LLMs give you common solutions, not innovative ones."

**Example**:
- "I asked for a sorting algorithm"
- "It gave me bubble sort"
- "For 1M items - terrible choice"

**Key Point**: "You need to know enough to catch this"

#### 2. Security-Critical Code
**Say**: "Always review authentication, authorization, crypto."

**Example**:
- "Generated JWT code that stored secrets in plain text"
- "Generated SQL queries vulnerable to injection"

**Key Point**: "Don't trust it with security"

#### 3. Performance-Critical Code
**Say**: "LLMs optimize for readability, not performance."

**Example**:
- "Generated file upload that loads entire file in memory"
- "Would crash on 1GB file"

**Key Point**: "Review hot paths carefully"

#### 4. Company-Specific Logic
**Say**: "LLMs don't know your domain."

**Example**:
- "Asked for invoice calculation"
- "It used simple arithmetic"
- "Our invoices have complex tax rules across 50 states"

**Key Point**: "You still own the business logic"

### Common Assumptions (6 min)

Scroll to assumptions section (lines 42-80):

**Say**: "LLMs make reasonable guesses, but they ARE guesses."

**Top assumptions**:
1. **REST over GraphQL** - "It defaults to REST endpoints"
2. **Synchronous over Async** - "Might generate blocking calls"
3. **In-memory over Distributed** - "Won't consider Redis, message queues"
4. **Happy path over Error handling** - "Often forgets edge cases"

**Key Message**:
> "These aren't bugs - they're defaults. You need to specify what you want."

### Skill Atrophy (5 min)

**Say**: "The biggest concern I hear: 'Will I forget how to code?'"

**Honest answer**:
- "If you ONLY vibecode and never think: **Yes**"
- "If you review every line and understand it: **No**"

**Analogy**:
> "Using a calculator doesn't make you bad at math - unless you stop understanding what the calculator is doing."

**Safeguards**:
1. Review every line
2. Do code reviews as a team
3. Build some things from scratch still
4. Use it for boilerplate, not learning

**Key Message**:
> "Vibecoding is a tool. Tools can make you faster or make you lazy - your choice."

---

## Section 6: Takeaways & Q&A (10 min) ⏰ 1:50-2:00

### What You Learned (3 min)

**Recap**:

**Exercise 1: Free-form**
- ✅ Fast (15 min)
- ⚠️ Technical debt
- 📊 Use for: Prototypes, throwaway code, MVPs

**Exercise 3: Template-driven**
- ✅ Fast AND maintainable (20 min initially, faster long-term)
- ✅ Includes tests
- 📊 Use for: Production code, team projects

**The Key Insight**:
> "Templates turn vibecoding from 'fast but messy' into 'fast AND sustainable'."

**The Honest Caveat**:
> "But you still need to understand the code. This isn't autopilot."

### Next Steps (2 min)

**For Skeptics**:
- ✅ Try it on a side project (low stakes)
- ✅ Create ONE template for a pattern you use
- ✅ Review every line it generates
- ❌ Don't ship code you don't understand

**For Believers**:
- ⚠️ Read WHEN_NOT_TO_USE.md carefully
- ⚠️ Watch for assumptions
- ⚠️ Keep practicing fundamentals

**Take Away**:
- This repo (MIT license)
- The template (customize it)
- The metrics (show your team)

### Q&A (5 min)

**Common questions**:

**Q: "Which LLM should I use?"**
A: "Claude, GPT-4, Copilot - try a few. Each has strengths. I prefer Claude for architecture."

**Q: "Will this replace junior developers?"**
A: "No. You need experience to review code. But it changes what we spend time on - less boilerplate, more domain problems."

**Q: "What about proprietary code?"**
A: "Most LLMs don't train on your inputs (check their policy). But you still own what you generate."

**Q: "How do I convince my team?"**
A: "Show them this workshop. Let them try it. Use metrics, not hype."

**Q: "What if my manager thinks this is cheating?"**
A: "Do you think using Stack Overflow is cheating? This is a tool. The question is: does it make you more effective?"

**Q: "What about code ownership? Who's responsible if there's a bug?"**
A: "You are. Always. You reviewed it, you shipped it, you own it. The LLM is a tool, not a team member."

### Closing (1 min)

**Final thought**:

> "You came in skeptical - that's good. Don't lose that skepticism. But add evidence to it. Try it, measure it, decide for yourself.
>
> Vibecoding isn't the future - it's a tool. Use it where it makes sense. Skip it where it doesn't.
>
> The only wrong choice is to ignore it without trying."

**Thank you for coming. Stay in touch. Let me know what you build.**

---

## Emergency Backup Plans

### If Demo Fails (Section 1)
- "That's actually a great example - LLMs fail sometimes"
- Switch to `exercise-1-freeform` branch
- "Here's one I prepared earlier"
- Continue with review

### If Running Behind
**Cut** (in priority order):
1. Live demo in Section 3 (just show existing code)
2. Detailed transcript walkthrough (just mention it exists)
3. Multiple maintenance challenges (just discuss one)

**Keep**:
- Opening demo (the hook)
- Template walkthrough (the insight)
- Metrics (the evidence)
- Pitfalls (the credibility)

### If Running Ahead
**Add**:
- Second maintenance challenge
- Deeper dive into Exercise 2 (DDD benefits)
- Live template customization

---

## Body Language & Tone Tips

**Do**:
- ✅ Be honest about failures
- ✅ Encourage skepticism
- ✅ Pause after questions (let them think)
- ✅ Admit when you don't know

**Don't**:
- ❌ Oversell ("This will change everything!")
- ❌ Dismiss concerns ("That's not really a problem")
- ❌ Rush through limitations
- ❌ Pretend you never make mistakes

**Remember**: This audience is skeptical. Your credibility comes from honesty, not enthusiasm.

---

## Post-Workshop

**Share**:
- Link to repo
- Your contact info
- Encourage them to share their experiences

**Follow up** (1 week later):
- "Did anyone try vibecoding this week?"
- "What worked? What didn't?"
- Iterate on the workshop based on feedback

---

*Good luck! Remember: show, don't tell. Evidence, not hype.*
