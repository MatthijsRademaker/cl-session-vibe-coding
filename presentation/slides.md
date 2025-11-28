---
theme: default
highlighter: shiki
lineNumbers: true
transition: slide-left
title: Vibecoding Workshop
mdc: true
---

# Vibecoding Workshop

Building with LLMs: Free-form vs Template-driven

<div class="pt-12 text-sm opacity-75">
Press Space to start →
</div>

<!--
Hey! This is a hands-on session about using LLMs to build code.

Not here to convince you it's amazing - just showing you two approaches and letting you decide.
-->

---

# Quick Context

You've probably tried ChatGPT/Claude for coding

<v-clicks>

**Today**: Same task, two ways
- Free-form: "Build me a chatbot"
- Template: "Build me a chatbot following this structure..."

**Goal**: See which one you'd actually use

</v-clicks>

<!--
Show of hands - who's used an LLM to write code before?

Cool. Today we're comparing approaches, not selling you on AI.
-->

---

# Workshop Flow (2hrs)

<v-clicks>

1. **Show**: Free-form result (messy but fast)
2. **Show**: Template approach (structure)
3. **Build**: You try the template (~50 min)
4. **Compare**: Discuss what happened
5. **Reality check**: When this fails

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
Most of the time is you building, not me talking
</div>

</v-click>

<!--
This isn't a lecture. You'll spend most of the time coding.

I'll show you what happened when I built the same thing two ways, then you try it.
-->

---
layout: center
---

# Part 1: Free-Form Result

What happens with zero constraints?

---

# The Prompt

```text
Create a chatbot.
Frontend: Vue 3
Backend: .NET 8
Keep it simple.
```

<v-click>

**Result**: ✅ Works in 15 minutes

</v-click>

<v-click>

**But...**

</v-click>

---

# The Code

```csharp {all|1-5|7-12|all}
// All in one file (Program.cs)
var responses = new[] {
    "That's interesting!",
    "Tell me more..."
};

app.MapPost("/api/chat", (ChatRequest request) => {
    var message = request.Message.ToLower();

    if (message.Contains("hello"))
        return new ChatResponse("Hi there!");

    return new ChatResponse(responses[Random.Shared.Next()]);
});
```

<v-click>

**Question**: Where would you add authentication?

</v-click>

<!--
Everything's inline. Business logic mixed with HTTP handling.

It works, but... where do things go as it grows?
-->

---

# Free-Form Stats

| Metric | Value |
|--------|-------|
| Time | 15 min |
| Files | 2 |
| Tests | 0 |
| Iterations | 7 (bugs found) |

<v-click>

**Reality**: Even "simple" took iteration

</v-click>

<!--
Key point: LLMs aren't one-shot. You iterate.

But the structure? All over the place.
-->

---
layout: center
---

# Part 2: Template Approach

What if we gave the LLM structure?

---

# The Template

Instead of:
> "Create a chatbot"

We give:
> "Using template.md, create a chatbot with:
> - Vertical slice architecture
> - Validation & error handling
> - BDD tests"

<v-click>

**Template = Guardrails**

</v-click>

<!--
The template tells the LLM:
- Where files go
- What quality looks like
- What tests to write

Not hoping for good code - requiring it.
-->

---

# Template Structure

```markdown
Domain/Features/{FeatureName}/
  ├── {Entity}.cs        # Business logic
  ├── {UseCase}.cs       # Orchestration
  └── I{Repo}.cs         # Interface

Tests/Features/{FeatureName}/
  └── {Feature}.feature  # Gherkin tests
```

<v-click>

Each feature = isolated folder

</v-click>

---

# Template: Testing Required

```gherkin
Feature: Chat
  Scenario: Send message
    Given I have a conversation
    When I send "Hello"
    Then I get a response
```

<v-click>

LLM generates code **and** tests

</v-click>

---
layout: center
---

# Part 3: Your Turn

Build time 🔨

---

# The Assignment

Pick **one** feature:

<v-clicks>

**Option A: Message Reactions** ⭐
Add emoji reactions (👍, ❤️, 😂)

**Option B: User Profiles**
Names + avatar colors

**Option C: Message History**
Last 50 messages on load

</v-clicks>

---

# How to Use the Template

```text
Using .prompts/NEW_FEATURE_TEMPLATE.md,
create Message Reactions.

Requirements:
- Emoji reactions (👍, ❤️, 😂)
- Store per message
- Show counts

Follow template structure exactly.
```

<v-click>

Then: iterate, debug, get it working

</v-click>

---

# Setup Check

```bash
# Branch
git checkout exercise-2-ddd-guardrails

# Frontend
cd frontend && npm run dev

# Backend
cd backend/Api && dotnet run

# LLM ready?
```

<v-click>

**Ready? Let's build** ⏱️

</v-click>

---
layout: center
---

# ⏰ Checkpoint: 10 min

Quick check - anyone stuck?

---
layout: center
---

# ⏰ Checkpoint: 25 min

Halfway. How's it going?

---
layout: center
---

# ⏰ Checkpoint: 40 min

Wrap it up. We'll discuss in 5.

---
layout: center
---

# Part 4: Discussion

What just happened?

---

# Share

<v-clicks>

**Did it work?** (Bugs are fine)

**How many iterations?**

**Did you understand the code?**

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
Honest answers - this isn't graded
</div>

</v-click>

---

# Common Pattern

<v-clicks>

✅ **Most finish in 30-40 min**

✅ **Code is organized** (vs free-form mess)

✅ **Some have tests**

⚠️ **Still needed iteration** (normal!)

</v-clicks>

<v-click>

**Templates guide, not magic**

</v-click>

---

# The Numbers

| Metric | Free-form | Template |
|--------|-----------|----------|
| Initial | 15 min | 20 min |
| Files | 2 | ~15 |
| Tests | 0 | Included |
| Maintainability | 😬 | ✅ |

<v-click>

**Trade-off**: 5 min slower, way more organized

</v-click>

---

# Code Organization

<div grid="~ cols-2 gap-4">

<div>

## Free-form
```
backend/
  Program.cs  (everything)

frontend/
  ChatBot.vue
```

Add feature? 🤷 Where does it go?

</div>

<div>

## Template
```
backend/
  Features/
    Chat/
    Auth/  ← Add here

frontend/
  features/
    chat/
    auth/  ← Add here
```

Add feature? ✅ Obvious

</div>

</div>

---
layout: center
---

# Part 5: Reality Check

When this doesn't work

---

# ❌ Don't Use For

<v-clicks>

**Novel algorithms**
LLMs give you common solutions, not innovative ones

**Security stuff**
Always review auth, crypto, SQL queries yourself

**Performance-critical**
Optimizes for readability, not speed

**Your specific domain**
LLM doesn't know your business rules

</v-clicks>

---

# Common Assumptions

LLMs guess:

<v-clicks>

- REST (not GraphQL)
- Sync (not async)
- In-memory (not Redis/queues)
- Happy path (not error cases)

</v-clicks>

<v-click>

**You need to catch these**

</v-click>

---

# "Will I Forget How to Code?"

<v-clicks>

**If** you copy blindly: Yes ❌

**If** you review everything: No ✅

</v-clicks>

<v-click>

Like Stack Overflow - tool, not crutch

</v-click>

---

# Safeguards

<v-clicks>

1. Review every line
2. Ask LLM to explain
3. Code review with team
4. Build some things from scratch
5. Use for boilerplate, not learning

</v-clicks>

---
layout: center
---

# Wrap-Up

---

# What We Saw

<v-clicks>

**Free-form**: Fast (15 min), messy

**Template**: Slightly slower (20 min), organized

**Key**: Templates encode your standards

**Reality**: Still needs review, iteration

</v-clicks>

---

# Try It

<v-clicks>

**Skeptics**: Side project, one template

**Believers**: Read WHEN_NOT_TO_USE.md

**Everyone**: Measure, don't guess

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
Repo: github.com/MatthijsRademaker/cl-session-vibe-coding
</div>

</v-click>

---

# Q&A

<v-clicks>

**"Which LLM?"**
Claude, GPT-4, Copilot - try a few

**"Replace juniors?"**
No. You need experience to review

**"How customize template?"**
It's markdown. Change it.

</v-clicks>

---
layout: center
---

# Thanks

Try it. Decide for yourself.

<div class="text-sm opacity-75 mt-8">
github.com/MatthijsRademaker/cl-session-vibe-coding
</div>

<!--
Slides are in the repo if you want to review.

Good luck!
-->
