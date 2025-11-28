# Vibecoding Workshop

**A hands-on, evidence-based workshop** for experienced developers who are skeptical about LLM-assisted coding.

> **Goal**: Don't just *tell* developers about vibecoding—let them *experience* the differences themselves.

---

## What Makes This Workshop Different

### ❌ Not Another "AI is Amazing" Talk
- No hype or cheerleading
- Honest about limitations ([see docs/WHEN_NOT_TO_USE.md](docs/WHEN_NOT_TO_USE.md))
- Evidence-based comparisons ([see docs/METRICS.md](docs/METRICS.md))
- Real maintenance scenarios ([see docs/MAINTENANCE_CHALLENGES.md](docs/MAINTENANCE_CHALLENGES.md))

### ✅ Practical, Hands-On Learning
- Build the same feature 3 different ways
- See actual conversation transcripts ([transcripts/](transcripts/))
- Try maintenance challenges yourself
- Make your own informed decision

---

## The Five Exercises

### Exercise 0: The Messy Reality
**Starting point**: Legacy code with no structure
- **Context**: 138 lines in one file, global state, TODOs everywhere
- **Challenge**: Try adding features to THIS
- **Learning**: LLMs struggle 2-3x more on brownfield code

**Why it matters**: Most real codebases look like this, not the clean examples.

[View details →](docs/EXERCISE-0.md)

---

### Exercise 1: Free-form Vibecoding
**Prompt**: "Create a chatbot"
- **Speed**: ⚡ 15 minutes
- **Quality**: ⚠️ Works but technical debt
- **Best for**: Prototypes, MVPs, quick demos

**Result**: Fast initial build, but maintenance slows you down over time.

[View transcript →](transcripts/EXERCISE-1-TRANSCRIPT.md)

---

### Exercise 2: DDD with Guardrails
**Prompt**: "Create a chatbot using Domain-Driven Design"
- **Speed**: 🐌 45 minutes
- **Quality**: ✅ Clean architecture
- **Best for**: Production apps, team projects

**Result**: Higher upfront cost, but consistent maintenance speed.

[View transcript →](transcripts/EXERCISE-2-TRANSCRIPT.md)

---

### Exercise 3: Prompt Engineering
**Approach**: Use comprehensive template
- **Speed**: ⚙️ 20 minutes
- **Quality**: ✅✅ Clean + testable
- **Best for**: Long-term projects, scaling teams

**Result**: Best of both worlds—fast AND maintainable.

[View template →](.prompts/NEW_FEATURE_TEMPLATE.md)

---

### Exercise 4: Brownfield Migration
**Strategy**: Strangler Fig Pattern for legacy code
- **Approach**: Don't rewrite, gradually replace
- **Tactics**: New features clean, migrate old code when touching it
- **Timeline**: Months of sustainable improvement, not weeks of chaos

**Why it matters**: Bridges the gap between greenfield examples and your day job.

[View migration guide →](docs/EXERCISE-4-BROWNFIELD-MIGRATION.md)

---

## Workshop Structure (2 hours)

> **📋 For Instructors**: See [docs/WORKSHOP_HANDS_ON.md](docs/WORKSHOP_HANDS_ON.md) for complete facilitation guide with timing, troubleshooting, and what to prepare.
>
> **📋 For Participants**: See [docs/PARTICIPANT_GUIDE.md](docs/PARTICIPANT_GUIDE.md) for setup instructions and how to use the template.

### Format: Hands-On Learning

Participants **build a feature** using the template, experiencing vibecoding firsthand.

### 1. Opening: The Problem (15 min)
- Show Exercise 1 free-form result
- Discuss concerns (all in one file, no tests)
- "What if we could keep speed but add structure?"

### 2. Introduce the Template (10 min)
- Walk through `.prompts/NEW_FEATURE_TEMPLATE.md`
- Architecture, quality requirements, testing
- "This guides the LLM to your standards"

### 3. Hands-On: Build with Template (50 min) 👩‍💻
- **Participants choose**: Message Reactions, User Profiles, or Message History
- Use template to guide LLM
- Work independently or in pairs
- Instructors help with troubleshooting

### 4. Comparison & Discussion (20 min)
- Share results: What worked? What broke?
- Show metrics: Speed, structure, tests
- Key insight: "Templates guide, not magic"

### 5. Pitfalls & Honesty (15 min)
- When NOT to use vibecoding
- Security, performance, skill atrophy concerns
- "You still need to understand the code"

### 6. Wrap-Up (10 min)
- Takeaways and next steps
- Q&A

**Note**: Exercise 2 (DDD) provides the starting architecture for the hands-on portion. Exercise 1 (free-form) is shown for comparison but not built live.

---

## Getting Started

### Prerequisites
- Node.js 18+
- .NET 8 SDK
- Git
- Text editor (VS Code recommended)

### Setup

```bash
# Clone the repo
git clone https://github.com/MatthijsRademaker/cl-session-vibe-coding.git
cd cl-session-vibe-coding

# Install frontend dependencies
cd frontend
npm install

# Verify frontend works
npm run dev
# → http://localhost:5173

# Verify backend works
cd ../backend/Api
dotnet run
# → http://localhost:5000
```

### Branch Structure

```
main (base layer)
  ├── exercise-0-messy-legacy (brownfield starting point)
  ├── exercise-1-freeform (zero constraints)
  │     └── exercise-2-ddd-guardrails (architectural patterns)
  │           └── exercise-3-prompt-engineering (templates)
  │                 └── exercise-4-brownfield-migration (migration strategy)
```

Switch between exercises:
```bash
git checkout exercise-0-messy-legacy           # Messy legacy code
git checkout exercise-1-freeform               # Free-form approach
git checkout exercise-2-ddd-guardrails         # DDD approach
git checkout exercise-3-prompt-engineering     # Template approach
git checkout exercise-4-brownfield-migration   # Migration strategy
```

---

## Key Documents

### For Participants
- 📊 [**docs/METRICS.md**](docs/METRICS.md) - Quantifiable comparisons (time, LOC, quality)
- 🔧 [**docs/MAINTENANCE_CHALLENGES.md**](docs/MAINTENANCE_CHALLENGES.md) - Hands-on scenarios
- ⚠️ [**docs/WHEN_NOT_TO_USE.md**](docs/WHEN_NOT_TO_USE.md) - Honest limitations
- 💬 [**transcripts/**](transcripts/) - Actual LLM conversations

### For Instructors
- 🎯 [**WORKSHOP_GUIDE_2HR.md**](WORKSHOP_GUIDE_2HR.md) - Complete 2-hour workshop script
- 📋 [**.prompts/NEW_FEATURE_TEMPLATE.md**](.prompts/NEW_FEATURE_TEMPLATE.md) - Reusable template
- 📚 [**docs/CLAUDE.md**](docs/CLAUDE.md) - Architecture guide

### For Self-Study
- 🗂️ [**docs/EXERCISE-0.md**](docs/EXERCISE-0.md) - Brownfield reality
- 🔄 [**docs/EXERCISE-1.md**](docs/EXERCISE-1.md) - Free-form analysis
- 🏗️ [**docs/EXERCISE-2.md**](docs/EXERCISE-2.md) - DDD deep-dive
- 📝 [**docs/EXERCISE-3.md**](docs/EXERCISE-3.md) - Template benefits
- 🌱 [**docs/EXERCISE-4-BROWNFIELD-MIGRATION.md**](docs/EXERCISE-4-BROWNFIELD-MIGRATION.md) - Migration strategy

---

## What Participants Will Learn

### You'll Experience
1. **Speed vs. Structure Trade-off**: Fast prototyping vs. maintainable code
2. **The Power of Good Prompts**: How constraints shape LLM output
3. **Long-term Thinking**: Initial speed isn't everything
4. **Honest Limitations**: When NOT to vibecode

### You'll Create
1. **Your Own Template**: For features you build often
2. **Mental Framework**: When to use which approach
3. **Confidence**: To try vibecoding at work (or not)

### You Won't Get
- ❌ "AI will replace developers" hype
- ❌ Unrealistic promises
- ❌ One-size-fits-all solutions

---

## Success Metrics

This workshop succeeds if participants can:

1. ✅ **Articulate**: "Here's when I'd use vibecoding, and when I wouldn't"
2. ✅ **Demonstrate**: "I saw a 3x speedup in this scenario"
3. ✅ **Create**: "I have a template I can use at work"
4. ✅ **Evaluate**: "I have concerns, but I understand the trade-offs"

**Not**: "Vibecoding solves everything!" (That's naive)

---

## Real-World Data

From the exercises:

| Metric | Exercise 1 | Exercise 2 | Exercise 3 |
|--------|-----------|-----------|-----------|
| Initial build | 15 min | 45 min | 20 min |
| Add new feature | 10-15 min | 15-20 min | 8-12 min |
| Lines of code | 350 | 850 | 1430 |
| Test coverage | 0% | 0% | 75% |
| 1-year cost (3 devs, 50 features) | ~16 weeks | ~13 weeks | ~11 weeks |

**Break-even point**: Exercise 3 becomes faster than Exercise 1 after ~12 features.

[See full metrics →](docs/METRICS.md)

---

## For Skeptics

### Common Concerns Addressed

**"Will I forget how to code?"**
→ Depends how you use it. [Read more](docs/WHEN_NOT_TO_USE.md#skill-atrophy-real-concern)

**"What about security?"**
→ Always review security-critical code. [Read more](docs/WHEN_NOT_TO_USE.md#3-security-sensitive-code)

**"LLMs make mistakes"**
→ Yes. That's why you review. [See examples](docs/WHEN_NOT_TO_USE.md)

**"This is just hype"**
→ This workshop is evidence-based. [See metrics](docs/METRICS.md)

**"My domain is too complex"**
→ LLMs handle patterns, you handle domain. [Read more](docs/WHEN_NOT_TO_USE.md#4-company-specific-business-logic)

---

## Workshop Materials

### Instructor Guide
- [Workshop flow and timing](docs/WORKSHOP_IMPROVEMENTS.md)
- Discussion prompts
- Common questions and answers

### Participant Worksheets
- Time tracking sheet
- Comparison table
- Reflection questions

---

## After the Workshop

### Keep Learning
1. Try vibecoding on a side project
2. Create templates for your common patterns
3. Share experiences with your team
4. Iterate on what works for you

### Share Feedback
- What worked?
- What was unclear?
- What would you change?

---

## The Bottom Line

**Vibecoding is a tool**, not magic.

Like any tool:
- ✅ Use it for the right job
- ✅ Learn its limitations
- ✅ Get better with practice
- ❌ Don't use it blindly

**This workshop helps you decide**:
- Is this tool right for me?
- For which projects?
- In which situations?
- With which safeguards?

---

## Running the Workshop

### As a Company
- License: MIT (free to use)
- Customize for your stack
- Add your own examples
- Create company-specific templates

### As an Individual
- Work through exercises yourself
- Time yourself
- Try the maintenance challenges
- Reflect on what you learned

---

## Credits

This workshop teaches **vibecoding** - a term for LLM-assisted development that emphasizes the conversational, iterative nature of working with AI.

Built to convince skeptical developers through **experience**, not **evangelism**.

---

## Quick Links

- 🚀 [Get started](#getting-started)
- 📊 [See the data](docs/METRICS.md)
- ⚠️ [Know the limits](docs/WHEN_NOT_TO_USE.md)
- 💬 [Read real conversations](transcripts/)
- 📋 [Get the template](.prompts/NEW_FEATURE_TEMPLATE.md)
- 🔧 [Try the challenges](docs/MAINTENANCE_CHALLENGES.md)

---

*For questions or feedback, open an issue or discussion.*
