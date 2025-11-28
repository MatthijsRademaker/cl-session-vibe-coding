---
theme: default
background: https://images.unsplash.com/photo-1451187580459-43490279c0fa?q=80&w=2072
class: text-center
highlighter: shiki
lineNumbers: true
info: |
  ## Vibecoding Workshop
  Template-driven LLM development for skeptics
drawings:
  persist: false
transition: slide-left
title: Vibecoding Workshop
mdc: true
---

# Vibecoding Workshop

Template-Driven LLM Development

<div class="pt-12">
  <span @click="$slidev.nav.next" class="px-2 py-1 rounded cursor-pointer" hover="bg-white bg-opacity-10">
    Press Space to start <carbon:arrow-right class="inline"/>
  </span>
</div>

<div class="abs-br m-6 flex gap-2">
  <a href="https://github.com/yourusername/cl-sessie" target="_blank" alt="GitHub" title="Open in GitHub"
    class="text-xl slidev-icon-btn opacity-50 !border-none !hover:text-white">
    <carbon-logo-github />
  </a>
</div>

<!--
Welcome everyone! Today we're exploring template-driven vibecoding.

This isn't a hype session - it's hands-on, evidence-based learning.

You'll build something yourself and decide if this is useful.
-->

---
layout: center
---

# Who This Is For

<v-clicks>

- 🤔 **Skeptical developers** who've heard the hype
- 🔍 **Evidence-based thinkers** who want data
- 👩‍💻 **Experienced engineers** comfortable with code
- 🎯 **Pragmatists** looking for actual productivity gains

</v-clicks>

<br>

<v-click>

### Not For:
- ❌ "AI will replace developers" believers
- ❌ People looking for magic solutions
- ❌ Those unwilling to review generated code

</v-click>

<!--
If you're skeptical - good! That's the right mindset.

We're going to show you evidence, not hype.

You'll decide based on what you build, not what I say.
-->

---
layout: two-cols
---

# Workshop Structure

<v-clicks>

## Part 1: The Problem (15 min)
Free-form vibecoding results

## Part 2: The Template (10 min)
How to guide LLMs

## Part 3: Hands-On (50 min)
🔨 **You build a feature**

</v-clicks>

::right::

<v-clicks>

## Part 4: Discussion (20 min)
Compare results & metrics

## Part 5: Honesty (15 min)
When NOT to use this

## Part 6: Wrap-Up (10 min)
Q&A and takeaways

</v-clicks>

<!--
This is a hands-on workshop.

You won't just watch - you'll build.

50 minutes of building time is the core of this session.
-->

---
layout: center
class: text-center
---

# Part 1: The Problem

What happens with free-form vibecoding?

<!--
Let's start by looking at what happens when you just ask an LLM
to "build a chatbot" with no structure or guidance.
-->

---

# Exercise 1: Free-Form Vibecoding

**The Prompt**: "Create a chatbot. Frontend: Vue 3. Backend: .NET 8. Keep it simple."

<v-clicks>

**Result**: ✅ Works in 15 minutes

**But...**

</v-clicks>

---

# Exercise 1: The Code

All business logic in **one file** (Program.cs):

```csharp {all|4-12|14-20|22-38|all}
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(/* ... */);

// Response patterns defined inline
var responses = new[] {
    "That's interesting! Tell me more.",
    "I understand. How does that make you feel?",
    // ... 10 more responses
};

var greetings = new[] { "hello", "hi", "hey" };
var questions = new[] { "how are you", "what's up" };

// Business logic mixed with HTTP handling
app.MapPost("/api/chat", (ChatRequest request) => {
    var userMessage = request.Message.ToLower().Trim();
    string botResponse;

    // All rules inline
    if (greetings.Any(g => userMessage.Contains(g))) {
        botResponse = "Hello! I'm doing great...";
    } else if (questions.Any(q => userMessage.Contains(q))) {
        botResponse = "I'm doing wonderfully!...";
    } else if (userMessage.Contains("bye")) {
        botResponse = "Goodbye!...";
    } else if (userMessage.Contains("help")) {
        botResponse = "I'm here to chat!...";
    } else if (userMessage.Contains("?")) {
        botResponse = "Great question! " + responses[Random.Shared.Next()];
    } else {
        botResponse = responses[Random.Shared.Next()];
    }

    return new ChatResponse(botResponse);
});
```

<!--
Notice what's happening here:

Lines 4-12: Data mixed with code
Lines 14-20: HTTP logic mixed with business logic
Lines 22-38: All business rules inline - no separation

Where would you add authentication? Where are tests?
-->

---

# Exercise 1: Stats

<v-clicks>

| Metric | Value |
|--------|-------|
| **Time to build** | 15 minutes |
| **Files created** | 2 (Program.cs, ChatBot.vue) |
| **Lines of code** | ~350 |
| **Tests** | 0 |
| **Iterations with LLM** | 7 (bugs, refinements) |

</v-clicks>

<v-click>

### The Reality

Even "simple" code took **7 iterations**:
- Iteration 2: Enter key bug
- Iteration 3: Auto-scroll issue
- Iteration 5: TypeScript types
- Iteration 7: Final polish

</v-click>

<!--
Key point: Even free-form isn't instant magic.

You still iterate, find bugs, refine.

But look at the structure - everything in one place.
-->

---
layout: center
---

# Discussion

<v-clicks>

**Q: Where would you add user authentication?**

🤔 Not clear - it's all mixed together

**Q: Where are the business rules?**

🤷 Inline with HTTP handling

**Q: How would you test this?**

😬 Need full HTTP setup, can't isolate logic

</v-clicks>

<v-click>

<div class="text-center mt-8 text-2xl text-red-400">
Fast to write, but maintainability questions arise immediately.
</div>

</v-click>

<!--
Pause here - let participants think through these questions.

The point isn't that this is "bad code" - it's that it's hard to maintain.

Ask the room: "Would you merge this PR?"
-->

---
layout: center
class: text-center
---

# Part 2: The Template

How do we guide LLMs to produce better structure?

<!--
The problem: Free-form gives fast but messy results.

The solution: A template that encodes our standards.
-->

---

# The Template Approach

`.prompts/NEW_FEATURE_TEMPLATE.md`

<v-clicks>

Instead of:
> "Create a chatbot"

We provide:
> "Using NEW_FEATURE_TEMPLATE.md, create a chatbot following vertical slice architecture, with validation, error handling, and BDD tests"

</v-clicks>

<v-click>

### The Template IS the Guardrails

</v-click>

<!--
The template tells the LLM:
- Where files go
- What quality standards to meet
- What tests to write
- How to structure code

It's not magic - it's guided generation.
-->

---

# Template Structure

```markdown
## Backend Structure

Domain/Features/{FeatureName}/
  ├── {Entity}.cs           # Domain model with validation
  ├── I{Repository}.cs      # Interface (dependency inversion)
  └── {UseCase}.cs          # Business logic

Application/Features/{FeatureName}/
  └── {UseCase}.cs          # Application orchestration

Infrastructure/Features/{FeatureName}/
  └── {Implementation}.cs   # Concrete implementations

Api/Features/{FeatureName}/
  └── {Feature}Controller.cs # HTTP endpoints
```

<v-click>

**Vertical Slice Architecture**: Each feature is isolated

</v-click>

<!--
Show this section of the template.

Point out:
- Clear separation of concerns
- Each feature has its own folder
- Easy to find where things go
-->

---

# Template: Quality Requirements

The template specifies:

<v-clicks>

```markdown
## Validation Rules
- All inputs must be validated
- Domain entities validate themselves
- Use guard clauses for null checks

## Error Handling
- Never swallow exceptions
- Return meaningful error messages
- Use Result<T> pattern for operations that can fail

## Null Safety
- Use nullable reference types
- Check all inputs for null
- Provide default values where appropriate
```

</v-clicks>

<v-click>

**Your standards, encoded**

</v-click>

<!--
This is how you teach the LLM your team's conventions.

It's not hoping for good code - it's requiring it.
-->

---

# Template: Testing Requirements

```gherkin
Feature: {Feature Name}
  As a {user type}
  I want to {goal}
  So that {benefit}

Scenario: {Scenario name}
  Given {precondition}
  When {action}
  Then {expected result}
```

<v-click>

**Example**:

```gherkin
Feature: Chat
  As a user
  I want to send messages
  So that I can interact with the chatbot

Scenario: Send a message
  Given I have a conversation
  When I send message "Hello"
  Then I should receive a response
```

</v-click>

<!--
The template requires BDD tests in Gherkin format.

Tests aren't optional - they're part of the template.

LLM generates both code AND tests.
-->

---
layout: center
class: text-center
---

# Part 3: Hands-On

**Time to build!** 🔨

<!--
Now it's your turn. You'll experience template-driven vibecoding.

Not a demo - you're doing this yourself.
-->

---

# Your Assignment

Choose **ONE** feature to build:

<v-clicks>

## Option A: Message Reactions ⭐ (Recommended)
- Users can react with emoji (👍, ❤️, 😂)
- Backend stores reactions per message
- Frontend displays reaction counts

## Option B: User Profiles
- Users have names and avatar colors
- Backend stores user info
- Frontend shows user identity in chat

## Option C: Message History
- Persist messages in-memory
- Retrieve last 50 messages on load
- Backend provides history endpoint

</v-clicks>

<!--
Pick the feature that interests you.

Recommended: Start with Message Reactions (simplest).

Work independently or in pairs - your choice.
-->

---

# How to Use the Template

**Step 1**: Read `.prompts/NEW_FEATURE_TEMPLATE.md`

**Step 2**: Craft your prompt

```text
Using the template in .prompts/NEW_FEATURE_TEMPLATE.md,
create a Message Reactions feature.

Requirements:
- Users can react with emoji (👍, ❤️, 😂)
- Backend stores reactions per message
- Frontend displays reaction counts

Follow the template exactly:
1. Vertical slice architecture (Domain/Features/Reactions/)
2. Include validation and error handling
3. Write BDD tests with Gherkin scenarios
```

**Step 3**: Generate code with your LLM

**Step 4**: Test, iterate, debug

<!--
See PARTICIPANT_GUIDE.md for full instructions.

Key: Be explicit that you want the template followed.

Iteration is expected - don't panic when errors occur.
-->

---

# Setup Check

Before you start:

```bash
# Ensure you're on the right branch
git checkout exercise-2-ddd-guardrails

# Frontend running?
cd frontend && npm run dev
# → http://localhost:5173

# Backend running?
cd backend/Api && dotnet run
# → http://localhost:5000

# LLM access ready?
# Claude Code / ChatGPT / Copilot
```

<v-click>

**🚨 Raise your hand if you need help with setup**

</v-click>

<!--
Pause here - make sure everyone is ready.

Don't start the timer until everyone can proceed.
-->

---
layout: center
class: text-center
---

# Build Time: 50 Minutes

**Start now!** ⏱️

<div class="text-sm opacity-75 mt-8">
Checkpoints: 10 min, 25 min, 35 min, 40 min
</div>

<!--
Walk around, help with issues.

Don't solve problems - let the LLM do it.

If stuck: "Try asking the LLM that question"

Set a visible timer if possible.
-->

---
layout: center
class: text-center
---

# ⏰ Checkpoint: 10 Minutes

**Quick check-in:**

How many have code generated?

Any blockers?

<!--
Pause the room briefly.

Ask who has generated code already.

Help anyone completely stuck.
-->

---
layout: center
class: text-center
---

# ⏰ Checkpoint: 25 Minutes

**Halfway point:**

How many have it running?

Still debugging?

<!--
Most should have generated code by now.

Some will be debugging compilation errors.

That's normal - iteration is part of the process.
-->

---
layout: center
class: text-center
---

# ⏰ Checkpoint: 35 Minutes

**Wrapping up:**

Get it to a working state.

Finish current iteration.

<!--
Give them a heads-up that time is running out.

They don't need perfection - just working code.
-->

---
layout: center
class: text-center
---

# ⏰ Time's Up: 40 Minutes

**Finish your current step**

We'll discuss in 5 minutes.

<!--
Some will have fully working code.

Some will have bugs.

Both are fine - the point is the experience.
-->

---
layout: center
class: text-center
---

# Part 4: Discussion

What just happened?

<!--
Time to reflect on the experience.

This is where the learning happens.
-->

---

# Share Your Experience

<v-clicks>

**Q: Did it work? What broke?**

🎤 (Call on 3-4 participants)

**Q: How long did it take?**

🎤 (Ask around the room)

**Q: How many iterations with the LLM?**

🎤 (Show of hands: 1-2? 3-5? 5+?)

**Q: Did you understand the code it generated?**

🎤 (Honest answers)

</v-clicks>

<!--
This is crucial - let participants share real experiences.

Don't skip this even if running over time.

Honest feedback (bugs, struggles) is valuable.
-->

---

# Common Observations

<v-clicks>

## ✅ What Usually Works
- Most finish in 30-40 minutes
- Files are organized (vs Exercise 1's single file)
- Some have tests (vs Exercise 1's zero)
- LLM understood the structure

## ⚠️ What's Still Hard
- Still needed multiple iterations (that's normal!)
- Some tests don't compile/pass
- Some bugs in the logic
- Had to review and understand code

</v-clicks>

<v-click>

### Key Insight
**Templates guide iteration, they don't eliminate it**

</v-click>

<!--
Normalize the struggles.

If people had bugs - that's expected.

If it took multiple iterations - that's vibecoding.

The point: structured code in ~40 minutes.
-->

---

# The Metrics: Exercise 1 vs Exercise 3

| Metric | Exercise 1 (Free-form) | Exercise 3 (Template) |
|--------|-----------|-----------|
| **Initial build** | 15 min | 20 min |
| **Files created** | 2 | 19 |
| **Lines of code** | 350 | 1430 |
| **Test coverage** | 0% | 75% |
| **Time to add feature** | 10-15 min | 8-12 min |
| **1-year cost (3 devs, 50 features)** | ~16 weeks | ~11 weeks |

<v-click>

**Break-even point**: After ~12 features

</v-click>

<!--
Exercise 1 is faster initially (5 min saved).

But you just built structured code in ~40 min.

With tests included.

After 12 features, template approach is faster overall.
-->

---

# Code Volume: A Closer Look

**Exercise 3: 1430 LOC** 😱

<v-click>

But wait...

</v-click>

<v-clicks>

- **Tests**: 300 LOC
- **Documentation**: 200 LOC
- **Actual business logic**: ~600 LOC (similar to Exercise 1)
- **Infrastructure**: 330 LOC (reusable)

</v-clicks>

<v-click>

**Question**: Is more code always worse?

</v-click>

<v-click>

**Answer**: Not if it's organized, tested, and maintainable.

</v-click>

<!--
Address the LOC concern directly.

More code isn't bad if it's structured.

Would you rather maintain 350 LOC in one file or 1430 split across features?
-->

---

# Structure Comparison

<div class="grid grid-cols-2 gap-4">

<div>

## Exercise 1
```
backend/
  Api/
    Program.cs    (all logic here)

frontend/
  components/
    ChatBot.vue
```

**To add authentication:**
- 🤷 Not clear where it goes
- 😬 Might break existing logic

</div>

<div>

## Exercise 3
```
backend/
  Domain/Features/
    Chat/
    Auth/         (add here)
  Application/Features/
  Infrastructure/Features/
  Api/Features/

frontend/
  features/
    chat/
    auth/         (add here)
```

**To add authentication:**
- ✅ Clear: Features/Auth/
- ✅ Isolated from Chat

</div>

</div>

<!--
This is the key difference.

Exercise 1: Everything in one place
Exercise 3: Features isolated

Which is easier to maintain?
-->

---
layout: center
class: text-center
---

# Part 5: Honesty Time

When NOT to use vibecoding

<!--
Time to build credibility with honesty.

LLMs aren't magic - let's talk about limitations.
-->

---

# ❌ Don't Use LLMs For

<v-clicks>

## 1. Novel Algorithms
LLMs suggest **common solutions**, not innovative ones

**Example**: Asked for sorting → got bubble sort → terrible for 1M items

## 2. Security-Critical Code
Always review authentication, authorization, crypto

**Example**: Generated JWT with plain text secrets, SQL injection vulnerabilities

## 3. Performance-Critical Code
LLMs optimize for **readability**, not performance

**Example**: File upload that loads entire file in memory → crashes on large files

</v-clicks>

<!--
Be brutally honest here.

Share real examples of LLM failures.

This builds trust more than any success story.
-->

---

# ⚠️ Common LLM Assumptions

<v-clicks>

LLMs make **reasonable guesses**, but they ARE guesses:

## Defaults:
- ✅ REST over GraphQL
- ✅ Synchronous over Async
- ✅ In-memory over Distributed (Redis, queues)
- ✅ Happy path over Error handling

## What This Means:
You need to **specify** what you want, or **catch** what it assumes.

</v-clicks>

<v-click>

**Key Point**: You need to know enough to catch these issues.

</v-click>

<!--
These aren't bugs - they're defaults.

If you want GraphQL, you must say so.

If you need async, you must specify.

This is why you can't vibecode blindly.
-->

---

# The Skill Atrophy Question

> "Will I forget how to code?" 🤔

<v-clicks>

**Honest answer**:

**IF** you only vibecode and never think: **Yes** ❌

**IF** you review every line and understand it: **No** ✅

</v-clicks>

<v-click>

### Analogy

Using a calculator doesn't make you bad at math—

**unless you stop understanding what the calculator is doing.**

</v-click>

<!--
This is the biggest concern people have.

Be honest: yes, it's a risk if you're lazy.

But no, it's not a risk if you stay engaged.

Like any tool - use it thoughtfully.
-->

---

# Safeguards Against Skill Atrophy

<v-clicks>

## 1. Review Every Line
Don't copy code blindly

## 2. Ask the LLM to Explain
"Explain this code to me line by line"

## 3. Do Code Reviews as a Team
Multiple eyes catch more issues

## 4. Build Some Things From Scratch
Don't vibecode 100% of the time

## 5. Use for Boilerplate, Not Learning
Learn fundamentals first, then use LLM for repetitive work

</v-clicks>

<!--
Give practical advice.

These are things people can actually do.

Emphasize: vibecoding is a tool, not a replacement for thinking.
-->

---

# Red Flags: When to Be Extra Careful

<v-clicks>

⚠️ **Authentication / Authorization**

⚠️ **Payment Processing**

⚠️ **Database Migrations**

⚠️ **Performance-Critical Paths**

⚠️ **Complex Algorithms**

</v-clicks>

<v-click>

**For these**: Always review carefully, add extra tests, consider manual implementation.

</v-click>

<!--
Don't skip LLMs entirely for these.

But be EXTRA careful.

Review multiple times, add tests, maybe pair program.
-->

---
layout: center
class: text-center
---

# Part 6: Wrap-Up

Key takeaways and next steps

<!--
Final section - bring it all together.
-->

---

# What You Learned Today

<v-clicks>

## 1. Free-Form Vibecoding
✅ Fast (15 min)
⚠️ Technical debt
📊 Use for: Prototypes, throwaway code

## 2. Template-Driven Vibecoding
✅ Fast AND maintainable (20 min initially, faster long-term)
✅ Includes tests automatically
📊 Use for: Production code, team projects

## 3. The Key Insight
**Templates turn vibecoding from "fast but messy" into "fast AND sustainable"**

## 4. The Honest Caveat
**You still need to understand the code. This isn't autopilot.**

</v-clicks>

<!--
Summarize the main points.

Don't oversell - be balanced.

The template is the key insight.
-->

---

# Next Steps

<div class="grid grid-cols-2 gap-8">

<div>

## For Skeptics 🤔

<v-clicks>

- ✅ Try it on a side project (low stakes)
- ✅ Create ONE template for a pattern you use often
- ✅ Review every line it generates
- ❌ Don't ship code you don't understand

</v-clicks>

</div>

<div>

## For Believers 🚀

<v-clicks>

- ⚠️ Read WHEN_NOT_TO_USE.md carefully
- ⚠️ Watch for the assumptions LLMs make
- ⚠️ Keep practicing fundamentals
- ⚠️ Share experiences with your team

</v-clicks>

</div>

</div>

<v-click>

<div class="mt-8 text-center text-xl">
Both: **Measure results, don't rely on hype**
</div>

</v-click>

<!--
Actionable next steps.

For skeptics: Start small, stay cautious.

For believers: Stay grounded, watch for pitfalls.

Everyone: Evidence over hype.
-->

---

# Resources to Take Away

<v-clicks>

📋 **NEW_FEATURE_TEMPLATE.md**
Customize for your stack and conventions

📊 **METRICS.md**
Quantifiable comparisons (show your team/manager)

⚠️ **WHEN_NOT_TO_USE.md**
Share with skeptical colleagues

💬 **transcripts/**
See real LLM conversations with iterations

🔧 **MAINTENANCE_CHALLENGES.md**
Try more hands-on scenarios

</v-clicks>

<v-click>

**GitHub Repo**: MIT License (free to use, customize, share)

</v-click>

<!--
These are yours to take and use.

Customize the template for your team.

Share the metrics with management.

It's all open source.
-->

---

# Q&A

<v-clicks>

**Q: Which LLM should I use?**
A: Claude, GPT-4, Copilot - try a few. I prefer Claude for architecture.

**Q: Will this replace junior developers?**
A: No. You need experience to review. But it changes what we focus on.

**Q: What about proprietary code?**
A: Most LLMs don't train on your inputs (check their policy). You own what you generate.

**Q: How do I customize the template?**
A: It's just markdown. Replace the architecture with yours, add your conventions.

**Q: What if my manager thinks this is cheating?**
A: Do you think using Stack Overflow is cheating? This is a tool.

</v-clicks>

<!--
Answer common questions.

Keep answers short and practical.

Add your own based on your experience.
-->

---
layout: center
class: text-center
---

# Final Thought

<v-click>

**Vibecoding is a tool, not magic.**

</v-click>

<v-clicks>

Like any tool:
- ✅ Use it for the right job
- ✅ Learn its limitations
- ✅ Get better with practice
- ❌ Don't use it blindly

</v-clicks>

<v-click>

<div class="mt-12 text-2xl">
You came in skeptical.

Stay skeptical.

But add **evidence** to your skepticism.
</div>

</v-click>

<!--
Leave them with a balanced view.

Skepticism is healthy - keep it.

But make decisions based on experience, not assumptions.
-->

---
layout: center
class: text-center
---

# Thank You!

<div class="mt-8">

Try it. Measure it. Decide for yourself.

</div>

<div class="mt-12 opacity-75">

**Questions?** Open an issue on GitHub

**Feedback?** Let us know what worked (and what didn't)

</div>

<!--
End on a collaborative note.

You want feedback to improve the workshop.

Thanks for coming!
-->
