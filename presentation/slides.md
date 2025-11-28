---
theme: default
highlighter: shiki
lineNumbers: true
transition: slide-left
title: Vibecoding Workshop
mdc: true
---

# Vibecoding Workshop

LLM-assisted coding: What works, what doesn't

<div class="pt-12 text-sm opacity-75">
Press Space to start →
</div>

<!--
Hey! Today we're exploring LLM-assisted coding.

Not here to tell you it's amazing - you're going to try it and decide for yourself.
-->

---

# Show of Hands

<v-clicks>

**Used ChatGPT/Claude/Copilot for code?**

**Actually shipped the result?**

**Would use it again?**

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
Today: Hands-on. You decide what works.
</div>

</v-click>

<!--
Get a sense of the room.

Some will be experienced, some skeptical, some curious.

That's perfect.
-->

---

# The Concept

<v-clicks>

**LLM-assisted coding** (vibecoding):
- You describe what you want
- LLM generates code
- You review, iterate, ship

**Questions**:
- How much structure do you give the LLM?
- How do you keep quality high?
- When does this actually help?

</v-clicks>

<v-click>

**Today**: Try it yourself, see what happens

</v-click>

<!--
Not trying to convince anyone.

Just exploring: if you're going to use LLMs for code, how do you do it well?
-->

---

# Workshop Plan

<v-clicks>

1. **Quick context** - I show what I built (5 min)
2. **You build** - Try it yourself (~30 min)
3. **Compare** - What did we all discover? (20 min)
4. **Improve** - Better approaches (15 min)
5. **Brownfield reality** - Legacy code challenges (15 min)
6. **Reality check** - When this fails (10 min)

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
~80% of the time is you coding
</div>

</v-click>

<!--
This is hands-on. Minimal talking from me.

You'll learn more from trying than from listening.
-->

---
layout: center
---

# Context: What I Built

Quick look at free-form LLM coding

---

# The Prompt

```text
Create a chatbot.
Frontend: Vue 3
Backend: .NET 8
Keep it simple.
```

<v-click>

**Result**: Working in 15 minutes ✅

</v-click>

<v-click>

**Code quality**: 🤷

</v-click>

<!--
I built this before the workshop to show you what happens with zero constraints.

Fast, but...
-->

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

Everything inline. Where would auth go? Tests?

</v-click>

<!--
Classic free-form result:
- Works
- Fast
- But... structure?

You'll see this yourself in a moment.
-->

---

# Quick Stats

| Metric | Value |
|--------|-------|
| Time | 15 min |
| Files | 2 |
| Tests | 0 |
| Iterations | 7 |

<v-click>

**Reality**: Even simple code needed iteration

</v-click>

<!--
Point: LLMs aren't one-shot magic.

You iterate, debug, refine.

Now let's see what YOU build.
-->

---
layout: center
---

# Your Turn

Build something with an LLM

---

# The Assignment

Pick **one** feature to add to the chatbot:

<v-clicks>

**Option A: Message Reactions** ⭐
Add emoji reactions (👍, ❤️, 😂) to messages

**Option B: User Profiles**
Users have names + avatar colors

**Option C: Message History**
Load last 50 messages when chat opens

</v-clicks>

<!--
Pick whichever interests you.

They're all similar difficulty.
-->

---

# How to Approach It

<v-clicks>

**Up to you!**

Some ideas:
- Free-form: "Add message reactions to this chatbot"
- More specific: "Add reactions using X pattern..."
- Very detailed: Write out full requirements

**Goal**: See what works for YOU

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
No right answer. Just try it.
</div>

</v-click>

<!--
This is the experiment.

How much guidance do you give the LLM?

Try your instinct. We'll compare notes after.
-->

---

# Setup

```bash
# Clone
git clone https://github.com/MatthijsRademaker/cl-session-vibe-coding.git
cd cl-session-vibe-coding

# Start on exercise-1 (simple chatbot)
git checkout exercise-1-freeform

# Frontend
cd frontend && npm install && npm run dev

# Backend
cd backend/Api && dotnet run

# LLM ready? (ChatGPT / Claude / Copilot)
```

<v-click>

**Ready? Build time** ⏱️ (~30 min)

</v-click>

<!--
Everyone set up?

Questions before we start?

Alright, go!
-->

---
layout: center
---

# Discussion

What did we discover?

---

# Share

<v-clicks>

**Did it work?**

**How many iterations with the LLM?**

**What was hard? What was easy?**

**Would you ship this?**

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
Honest answers - no judgment
</div>

</v-click>

<!--
Get 3-4 people to share.

Listen for:
- Structure problems
- Iteration count
- Confidence in the code
-->

---

# Common Patterns

What I usually hear:

<v-clicks>

✅ **Fast initial generation**

⚠️ **Lots of iteration** (5-10+ rounds)

⚠️ **Structure all over the place**

⚠️ **Not sure where to add next feature**

❓ **Would I ship this?** (mixed feelings)

</v-clicks>

<!--
This is the problem free-form hits:
- Speed is great
- But structure? Consistency? Tests?

Sound familiar?
-->

---

# The Challenge

<v-clicks>

**LLM makes decisions**:
- Where files go
- How to structure code
- What patterns to use
- Whether to write tests

**Problem**: Those decisions are inconsistent

**Question**: How do we guide it better?

</v-clicks>

<!--
This is the insight.

You can use LLMs fast, but you need some way to guide them.

So... how?
-->

---
layout: center
---

# One Approach: Templates

Encoding your standards

---

# The Idea

Instead of:
> "Add message reactions"

You say:
> "Using this template, add message reactions"

<v-click>

**Template** = Your structure, patterns, standards

</v-click>

<!--
Let me show you what I mean.

This is ONE approach - not the only one.
-->

---

# Example Template

```markdown
## Structure

Domain/Features/{FeatureName}/
  ├── {Entity}.cs        # Business logic
  ├── {UseCase}.cs       # Orchestration
  └── I{Repo}.cs         # Interface

Tests/Features/{FeatureName}/
  └── {Feature}.feature  # Gherkin tests

## Requirements
- Validation on all inputs
- Error handling with Result<T>
- BDD tests for happy + error paths
```

<!--
This tells the LLM:
- Where files go
- What patterns to use
- What quality means

It's YOUR conventions, codified.
-->

---

# Template in Action

```text
Using .prompts/NEW_FEATURE_TEMPLATE.md,
add message reactions.

Requirements:
- Emoji reactions (👍, ❤️, 😂)
- Store per message
- Show counts in UI

Follow the template structure.
```

<v-click>

LLM generates structured code + tests

</v-click>

<!--
Same feature you just built.

But now the LLM follows YOUR structure.
-->

---

# Compare

<div grid="~ cols-2 gap-4">

<div>

## Free-form
```
backend/
  Program.cs (everything)

frontend/
  ChatBot.vue
```

- Fast
- Unstructured
- Hard to extend

</div>

<div>

## Template-driven
```
backend/Features/
  Reactions/
    Reaction.cs
    UseCase.cs
    IRepo.cs
  Tests/
    Reactions.feature
```

- Bit slower
- Consistent
- Clear where things go

</div>

</div>

<!--
Trade-off:
- Lose 5-10 min upfront
- Gain structure and consistency

Worth it? Depends on your project.
-->

---

# Discussion

<v-clicks>

**Would a template have helped you?**

**What would YOUR template include?**

**When would you skip the template?**

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
Template is in the repo: .prompts/NEW_FEATURE_TEMPLATE.md
</div>

</v-click>

<!--
This is the key question.

There's no one-size-fits-all.

What makes sense for YOUR codebase?
-->

---
layout: center
---

# But Wait...

There's a problem

---

# The Missing Reality

Everything so far assumed **greenfield** code:
- Clean structure
- Consistent patterns
- Small files
- Clear organization

**Real world**: You're dropped into **legacy** code.

<!--
This is the gap.

All examples were greenfield. Most work is brownfield.
-->

---

# Exercise 0: Legacy Code

```csharp
// Program.cs - All 138 lines in one file
var messages = new ConcurrentBag<ChatMsg>();

app.MapPost("/api/chat", (ChatRequest request) => {
    // 50 lines of inline logic
    // TODO: move to service?
    // FIXME: handle edge cases
    // Commented-out experiments
    // Inconsistent patterns
});

// Models at bottom
public class ChatMsg { ... }
```

**Try adding reactions to THIS**

<!--
This is what most codebases look like.

Not the clean examples we showed.
-->

---

# What Happens

Try using LLM on messy codebase:

<v-clicks>

⏱️ **20-30 minutes** (vs 8-12 clean)

🔄 **10-15 iterations** (vs 3-5 clean)

❌ **LLM suggests clean patterns that don't fit**
"Create a ReactionService" (but there are no services!)

❌ **Unclear where code should go**
In Program.cs? New file? Which one?

❌ **Each addition makes it messier**

</v-clicks>

<!--
This is the reality most participants will face.

LLMs are 2-3x slower on brownfield.
-->

---

# The Gap

<div grid="~ cols-2 gap-8">

<div>

## Greenfield
✅ 8-12 min to add feature
✅ 3-5 iterations
✅ Clear structure
✅ LLM thrives

**Workshop so far**

</div>

<div>

## Brownfield
❌ 20-30 min to add feature
❌ 10-15 iterations
❌ Messy structure
❌ LLM struggles

**Your day job**

</div>

</div>

**Question**: How do you bridge this gap?

<!--
This is the real challenge.

Templates help, but what about existing mess?
-->

---

# Strangler Fig Strategy

**Don't rewrite. Gradually replace.**

```
┌──────────────────────────────────────┐
│ Legacy Code (Program.cs)             │
│ /api/chat ← OLD (leave it)           │
│                                      │
│ New Features (Clean)                 │
│ /api/messages/reactions ← NEW        │
│ /api/users/profiles ← NEW            │
│                                      │
│ Over time, migrate when you touch... │
└──────────────────────────────────────┘
```

<v-clicks>

1. **New features → Clean architecture** (use template)
2. **Old features → Leave as-is** (if they work)
3. **Bug fixes → Migrate that piece** (opportunistic)

</v-clicks>

<!--
Named after strangler fig trees.

They gradually replace their host tree.

Same strategy for code.
-->

---

# Timeline

**Month 1**: All new features in clean architecture
- Legacy: 90%, Clean: 10%
- Feature speed: 18 min average

**Month 3**: Migrate a few old pieces when fixing bugs
- Legacy: 60%, Clean: 40%
- Feature speed: 14 min average

**Month 6**: Most code is clean
- Legacy: 30%, Clean: 70%
- Feature speed: 10 min average

**Key**: Legacy shrinks naturally over time

<!--
This is realistic migration.

Not a 3-month rewrite project.

Gradual, sustainable improvement.
-->

---

# Brownfield Takeaways

<v-clicks>

**Reality**: Most code is messy, not clean

**LLMs struggle** with unstructured code (2-3x slower)

**Templates help** but can't fix fundamental mess

**Strategy**: Strangler fig (new clean, migrate gradually)

**Timeline**: Months, not weeks

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
See Exercise 0 and Exercise 4 in the repo
</div>

</v-click>

<!--
This is the honest reality check.

Vibecoding isn't magic on legacy code.

But there IS a path forward.
-->

---
layout: center
---

# Reality Check

When LLMs fail

---

# Don't Use For

<v-clicks>

**Novel algorithms**
LLMs give common solutions, not innovative ones

**Security-critical**
Always review auth, crypto, SQL yourself

**Performance-critical**
Optimizes for readability, not speed

**Your domain logic**
LLM doesn't know your business

</v-clicks>

<!--
Be honest about limits.

LLMs are great for patterns, bad for novelty.
-->

---

# Common Traps

<v-clicks>

**Assumptions**:
- REST (not GraphQL)
- Sync (not async)
- In-memory (not Redis)
- Happy path (no error handling)

**You need to catch these**

</v-clicks>

<!--
LLMs make reasonable guesses.

But they're still guesses.

Review everything.
-->

---

# "Will I Forget How to Code?"

<v-clicks>

**If** you copy blindly: Yes ❌

**If** you review and understand: No ✅

</v-clicks>

<v-click>

Like Stack Overflow: tool, not replacement

</v-click>

<!--
Real concern.

The safeguard: always review, always understand.

If you don't understand it, don't ship it.
-->

---

# Takeaways

<v-clicks>

**LLMs**: Fast, but need guidance

**Templates**: One way to add structure

**Your approach**: Whatever works for you

**Reality**: Still needs review, iteration

</v-clicks>

<!--
No silver bullet.

Just tools. Use them thoughtfully.
-->

---

# Try It

<v-clicks>

**Skeptics**: Try on a side project

**Interested**: Create a template for YOUR patterns

**Everyone**: Measure results, don't guess

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
Try a few. Claude, GPT-4, Copilot.

**"Replace developers?"**
No. Review takes experience.

**"How to customize template?"**
It's markdown. Fork it, change it.

</v-clicks>

---
layout: center
---

# Thanks

Experiment. Decide for yourself.

<div class="text-sm opacity-75 mt-8">
github.com/MatthijsRademaker/cl-session-vibe-coding
</div>

<!--
Good luck!

Reach out if you have questions.
-->
