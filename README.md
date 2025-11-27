# Vibecoding Workshop

**A hands-on, evidence-based workshop** for experienced developers who are skeptical about LLM-assisted coding.

> **Goal**: Don't just *tell* developers about vibecoding—let them *experience* the differences themselves.

---

## What Makes This Workshop Different

### ❌ Not Another "AI is Amazing" Talk
- No hype or cheerleading
- Honest about limitations ([see WHEN_NOT_TO_USE.md](WHEN_NOT_TO_USE.md))
- Evidence-based comparisons ([see METRICS.md](METRICS.md))
- Real maintenance scenarios ([see MAINTENANCE_CHALLENGES.md](MAINTENANCE_CHALLENGES.md))

### ✅ Practical, Hands-On Learning
- Build the same feature 3 different ways
- See actual conversation transcripts ([transcripts/](transcripts/))
- Try maintenance challenges yourself
- Make your own informed decision

---

## The Three Approaches

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

## Workshop Structure (4 hours)

### Part 1: The Hook (30 min)
- Live demo: Build something impressive with LLM
- Challenge: "You try it without LLM"
- Compare results

### Part 2: Exercise 1 - Free-form (45 min)
- Review Exercise 1 code
- Discuss: What worked? What's concerning?
- **Your turn**: Add a feature (track your time)

### Part 3: Exercise 2 - DDD (45 min)
- Review Exercise 2 architecture
- Compare with Exercise 1
- **Your turn**: Same feature, different architecture

### Part 4: Exercise 3 - Templates (60 min)
- Introduce template approach
- **Your turn**: Use template for new feature
- Compare all three

### Part 5: Maintenance Reality (30 min)
- Work through maintenance challenges
- See which architecture holds up best
- Discussion: Long-term costs

### Part 6: Honest Discussion (30 min)
- When NOT to use vibecoding
- Addressing concerns (security, skill atrophy, etc.)
- Q&A

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
git clone <repo-url>
cd cl-sessie

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
master (base layer)
  └── exercise-1-freeform (zero constraints)
      └── exercise-2-ddd-guardrails (architectural patterns)
          └── exercise-3-prompt-engineering (templates)
```

Switch between exercises:
```bash
git checkout exercise-1-freeform    # See free-form approach
git checkout exercise-2-ddd-guardrails  # See DDD approach
git checkout exercise-3-prompt-engineering  # See template approach
```

---

## Key Documents

### For Participants
- 📊 [**METRICS.md**](METRICS.md) - Quantifiable comparisons (time, LOC, quality)
- 🔧 [**MAINTENANCE_CHALLENGES.md**](MAINTENANCE_CHALLENGES.md) - Hands-on scenarios
- ⚠️ [**WHEN_NOT_TO_USE.md**](WHEN_NOT_TO_USE.md) - Honest limitations
- 💬 [**transcripts/**](transcripts/) - Actual LLM conversations

### For Implementers
- 📋 [**.prompts/NEW_FEATURE_TEMPLATE.md**](.prompts/NEW_FEATURE_TEMPLATE.md) - Reusable template
- 📚 [**CLAUDE.md**](CLAUDE.md) - Architecture guide
- 🔄 [**EXERCISE-1.md**](EXERCISE-1.md) - Free-form analysis
- 🏗️ [**backend/EXERCISE-2.md**](backend/EXERCISE-2.md) - DDD deep-dive
- 📝 [**EXERCISE-3.md**](EXERCISE-3.md) - Template benefits

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

[See full metrics →](METRICS.md)

---

## For Skeptics

### Common Concerns Addressed

**"Will I forget how to code?"**
→ Depends how you use it. [Read more](WHEN_NOT_TO_USE.md#skill-atrophy-real-concern)

**"What about security?"**
→ Always review security-critical code. [Read more](WHEN_NOT_TO_USE.md#3-security-sensitive-code)

**"LLMs make mistakes"**
→ Yes. That's why you review. [See examples](WHEN_NOT_TO_USE.md)

**"This is just hype"**
→ This workshop is evidence-based. [See metrics](METRICS.md)

**"My domain is too complex"**
→ LLMs handle patterns, you handle domain. [Read more](WHEN_NOT_TO_USE.md#4-company-specific-business-logic)

---

## Workshop Materials

### Instructor Guide
- [Workshop flow and timing](WORKSHOP_IMPROVEMENTS.md)
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
- 📊 [See the data](METRICS.md)
- ⚠️ [Know the limits](WHEN_NOT_TO_USE.md)
- 💬 [Read real conversations](transcripts/)
- 📋 [Get the template](.prompts/NEW_FEATURE_TEMPLATE.md)
- 🔧 [Try the challenges](MAINTENANCE_CHALLENGES.md)

---

*For questions or feedback, open an issue or discussion.*
