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


<!--
Hey! Today we're exploring LLM-assisted coding.

Not here to tell you it's amazing - you're going to try it and decide for yourself.
-->

---

# Show of Hands

<v-clicks>

**Used an LLM for code?**

**Still uses it?**
</v-clicks>


<!--
Get a sense of the room.

Some will be experienced, some skeptical, some curious.

That's perfect.
-->

---

# Vibecoding

<v-clicks>

- You describe what you want
- LLM generates code
- You review
- Say it is not up to standard
- Loop
- Get frustrated

**Questions**:
- How much structure do you give the LLM?
- How much context do you give the LLM?
</v-clicks>


<!--
Not trying to convince anyone.

Just exploring: if you're going to use LLMs for code, how do you do it well?
-->

---

# How to

<v-clicks>

1. **Quick intro** - I show what I built (5 min)
2. **You navigate** - Try it yourself (~30 min)
3. **Compare** - What did we all discover? (20 min)
4. **Improve** - Better approaches (15 min)
5. **Brownfield reality** - Legacy code challenges (15 min)
6. **Reality check** - When this fails (10 min)

</v-clicks>

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

```csharp {all|1-10|12-26|all}{maxHeight: '400px'}
// All in one file (Program.cs) - 87 lines
var responses = new[] {
    "That's an interesting point! Tell me more.",
    "I understand. How does that make you feel?",
    "Fascinating! I'd love to hear your thoughts...",
    // ... 7 more responses
};

var greetings = new[] { "hello", "hi", "hey", "greetings" };
var questions = new[] { "how are you", "what's up" };

app.MapPost("/api/chat", (ChatRequest request) => {
    var userMessage = request.Message.ToLower().Trim();

    if (greetings.Any(g => userMessage.Contains(g)))
        botResponse = "Hello! How can I help you?";
    else if (questions.Any(q => userMessage.Contains(q)))
        botResponse = "I'm doing great! How about you?";
    else if (userMessage.Contains("bye"))
        botResponse = "Goodbye! Come back anytime!";
    else if (userMessage.Contains("?"))
        botResponse = "Great question! " + responses[Random.Shared.Next()];
    else
        botResponse = responses[Random.Shared.Next()];

    return new ChatResponse(botResponse);
});

record ChatRequest(string Message);
record ChatResponse(string Response);
```

<v-click>

Everything inline. If-else chains. Where would new features go?

</v-click>

<!--
Classic free-form result:
- Works
- Fast
- But... structure?

You'll see this yourself in a moment.
-->

---

# The Frontend

```vue {*}{maxHeight: '400px'}
<script setup lang="ts">
interface Message {
  id: number
  text: string
  sender: 'user' | 'bot'
  timestamp: Date
}

const messages = ref<Message[]>([...])
const inputMessage = ref('')
let messageIdCounter = 2

const sendMessage = async () => {
  // Add user message
  messages.value.push(userMessage)

  // Call backend API
  const response = await fetch('http://localhost:5000/api/chat', {
    method: 'POST',
    body: JSON.stringify({ message: userInput })
  })

  // Add bot response
  messages.value.push(botMessage)
}
</script>
```

<v-click>

All state in one component. Where would user profiles go?

</v-click>

<!--
Frontend also inline:
- All in ChatBot.vue
- No composables
- Hardcoded API URL
- Global message counter

Same pattern as backend.
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

<!--
Get 3-4 people to share.

Listen for:
- Structure problems
- Iteration count
- Confidence in the code
-->

---

# Common Patterns

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

🔄 **9 iterations** (vs 3-5 clean)

📄 **[See full transcript](https://github.com/MatthijsRademaker/cl-session-vibe-coding/blob/exercise-0-messy-legacy/transcripts/EXERCISE-0-TRANSCRIPT.md)**

❌ **LLM suggests clean patterns that don't fit**

❌ **Data structure conflicts** (ConcurrentBag limitations)

❌ **Cascading breaking changes**

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

---
layout: center
class: text-center
---

# One More Thing...

<v-click>

<div class="text-4xl mt-12 mb-8">
🤖
</div>

</v-click>

<v-click>

<div class="text-2xl font-bold">
This entire workshop was vibecoded.
</div>

</v-click>

<v-clicks>

**All 5 exercises** - Generated with Claude Code

**All documentation** - Written with LLM assistance

**This presentation** - Created using templates

**The transcripts** - Real LLM conversations

**The migration guide** - Vibecoded

</v-clicks>

<v-click>

<div class="text-sm opacity-75 mt-8">
Total time: ~4 hours. Lines of code: ~5000.
</div>

</v-click>

<!--
This is the proof.

Not theory. Practice.

The workshop you just experienced was built the way we taught you.

Questions?
-->
