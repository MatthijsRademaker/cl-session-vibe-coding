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

1. **The Problem** - Free-form prompting pain 
2. **The Solution** - Good architecture + templates
3. **The Template** - Your reusable prompt guide 
4. **Hands-on** - Build a feature using templates
5. **Brownfield reality** - Legacy code challenges 
6. **Reality check** - When this fails 

</v-clicks>

<!--
This is hands-on, but structured.

You'll learn a repeatable approach, not just experimentation.
-->

---
layout: center
---

# Part 1: The Problem

Why free-form prompting creates technical debt

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

```csharp {all|1-10|12-27|29-30}{maxHeight: '400px'}
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

```vue {all|2-7|13-25|11|all}{maxHeight: '400px'}
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

# Part 2: The Solution

Structured architecture + Prompt templates

---

# What Good Architecture Looks Like

Instead of everything in one file, organize by **features**:

```
Domain/Features/Reactions/
  ├── Reaction.cs         # Business logic
  ├── ReactionUseCase.cs  # Orchestration
  └── IReactionRepo.cs    # Interface

Infrastructure/Features/Reactions/
  └── InMemoryReactionRepo.cs

Tests/Features/Reactions/
  └── Reactions.feature   # BDD tests
```

<v-click>

**Key**: Each feature is self-contained and testable

</v-click>

<!--
This is what Exercise 2/3 looks like.

Clean separation of concerns.
Same code, whether free-form or template-driven.
-->

---

# The Challenge

How do you **consistently** get the LLM to produce this structure?

<v-clicks>

**❌ Free-form**: "Add reactions"
- LLM decides where files go
- Inconsistent patterns
- May skip tests

**✅ Template-driven**: "Using .prompts/NEW_FEATURE_TEMPLATE.md, add reactions"
- You decide the structure
- Consistent patterns
- Tests included

</v-clicks>

<!--
This is the insight.

Templates encode YOUR standards.
-->

---

# Part 3: The Template

Your reusable prompt guide

---
layout: center
---

# The Feature Template

`.prompts/NEW_FEATURE_TEMPLATE.md`

---

# What's in the Template?

A step-by-step guide for the LLM:

<v-clicks>

**1. Structure**: Where files go (Domain → Application → Infrastructure → API → Tests → Frontend)

**2. Patterns**: DDD entities, value objects, repositories, handlers

**3. Examples**: Code snippets showing exactly what you want

**4. Standards**: Naming conventions, testing requirements, validation rules

**5. Checklist**: Ensure nothing is forgotten

</v-clicks>

<!--
This is the money shot.

A reusable, shareable guide for consistent code.
-->

---

# Template Structure

```markdown
## Step-by-Step Feature Implementation

### 1. Domain Layer (Business Logic)
**Location**: `Domain/Features/{FeatureName}/`

**Rules**:
- ✅ No dependencies on other layers
- ✅ Pure business logic only
- ✅ Validation in constructors
- ❌ No infrastructure concerns

**Example**:
[Code example showing domain entity with validation]

### 2. Application Layer (Use Cases)
[Similar detailed guidance...]

### 3-6. [Infrastructure, API, Tests, Frontend]
```

<!--
Each step has:
- Clear location
- Explicit rules
- Working example

Copy-paste ready.
-->

---

# Using the Template

Your prompt to the LLM:

```text
Using .prompts/NEW_FEATURE_TEMPLATE.md, add message reactions.

Requirements:
- Emoji reactions (👍, ❤️, 😂)
- Store per message
- Show counts in UI

Follow the template structure exactly.
```

<v-click>

**Result**: Structured code + tests, consistent architecture

</v-click>

<!--
Same feature. Different approach.

Template guides the LLM step-by-step.
-->

---

# Why This Works

<v-clicks>

**Consistency**: Every feature follows same structure

**Onboarding**: New devs (and LLMs) know where things go

**Review**: Easy to spot deviations from standard

**Scaling**: Share template across team

**Evolution**: Update template = update all future code

</v-clicks>

<!--
This is about team scalability.

Not just you and an LLM. Your whole team.
-->

---
layout: center
---

# Part 4: Hands-On

Build a feature using the template

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

# Setup

```bash
# Clone
git clone https://github.com/MatthijsRademaker/cl-session-vibe-coding.git
cd cl-session-vibe-coding

# Start with clean architecture base (exercise-2/3)
git checkout exercise-2-ddd-guardrails

docker compose up -d
# LLM ready? (ChatGPT / Claude / Copilot)
```

<v-click>

**Your prompt**: "Using .prompts/NEW_FEATURE_TEMPLATE.md, add [your chosen feature]"

</v-click>

<!--
Start with clean architecture.

Use the template from the start.
-->

---

# Ready? Build Time ⏱️

**~40 minutes**

<v-clicks>

**Tips**:
- Read the template first
- Include it in your prompt
- Review each step the LLM produces
- Iterate if needed

**Goal**: Experience template-driven development

</v-clicks>

<!--
This is the hands-on portion.

Try it yourself. See how it feels.
-->

---
layout: center
---

# Discussion

What did we discover?

---

# Share

<v-clicks>

**Did the template help guide the LLM?**

**How many iterations did you need?**

**Code quality - would you ship this?**

**What would you change in the template?**

</v-clicks>

<!--
Get 3-4 people to share.

Listen for:
- Template clarity
- LLM adherence to structure
- Areas for improvement
-->

---

# Template-Driven Results

<v-clicks>

✅ **Consistent structure** across features

✅ **Fewer iterations** (3-5 vs 10+)

✅ **Tests included** by default

✅ **Clear where things go**

⚠️ **Takes time to build good template** initially

⚠️ **Template maintenance** as standards evolve

</v-clicks>

<!--
Template approach trade-offs:
- Upfront investment in template creation
- But ongoing consistency and speed

Most teams find it worth it.
-->

---

# Key Insight

<v-clicks>

**Free-form prompting**: Fast at first, slows down over time

**Template-driven**: Slower initially, speeds up over time

**The crossover**: Around feature 3-5

After that, templates pay for themselves

</v-clicks>

<!--
This is the reality.

Templates have upfront cost but long-term benefit.
-->

---
layout: center
---

# Part 5: Brownfield Reality

Templates meet legacy code

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

# Part 6: Reality Check

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

<v-click>

<div class="border-l-4 border-blue-500 bg-blue-50 dark:bg-blue-900/20 p-4 my-6 rounded-r">
  <p class="text-sm font-semibold text-blue-800 dark:text-blue-200 mb-1">💡 Exception</p>
  <p class="text-sm text-blue-700 dark:text-blue-300">LLMs <em>can</em> handle business logic with well-thought-out spec files (e.g., BDD scenarios)</p>
</div>

</v-click>

<!--
Be honest about limits.

LLMs are great for patterns, bad for novelty.
-->


---

# Takeaways

<v-clicks>

**Free-form prompting**: Fast initially, creates technical debt

**Templates**: Consistency and structure at scale

**Brownfield**: Strangler fig strategy (new clean, migrate gradually)

**Reality**: LLMs are tools, not magic - review always needed

</v-clicks>

<!--
No silver bullet.

Templates help, but you still need to think.
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
