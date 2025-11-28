# Vibecoding Workshop - Hands-On Format (2 hours)

**Target**: Experienced developers skeptical about LLM-assisted coding
**Format**: Demo → Hands-on → Discussion
**Key**: Participants EXPERIENCE template-driven vibecoding themselves

---

## Workshop Structure

### 1. Opening: The Problem (15 min)

**Goal**: Show free-form vibecoding results and why we need something better

#### What to Show
- Switch to `exercise-1-freeform` branch
- Show the chatbot (it works!)
- Open `backend/Api/Program.cs` - all in one file
- Quick look at transcript - took 15 min, had bugs

#### Discussion (5 min)
**Ask**: "What concerns you about this code?"

Expected answers:
- All in one file
- No tests
- Hard to add features
- Where do business rules go?

**Transition**: "What if we could keep the speed but add structure? That's what templates do."

---

### 2. Introduce the Template (10 min)

**Goal**: Walk through template sections so participants understand what to do

#### Show `.prompts/NEW_FEATURE_TEMPLATE.md`

**Key sections** (just highlight, don't read everything):

1. **Architecture** (2 min)
   ```markdown
   Domain/Features/{FeatureName}/
     ├── {Entity}.cs
     ├── I{Repository}.cs
     └── {UseCase}.cs
   ```

   **Say**: "Template tells LLM exactly where files go"

2. **Quality Requirements** (2 min)
   - Validation rules
   - Error handling
   - Null checks

   **Say**: "Your standards, encoded"

3. **Testing** (3 min)
   ```gherkin
   Feature: {Feature}
     Scenario: {Scenario}
       Given...
       When...
       Then...
   ```

   **Say**: "Tests aren't optional - template requires them"

4. **Example** (3 min)
   - Show the chat example already in template
   - "Copy this pattern for your feature"

**Transition**: "Now you're going to use this template to build a feature."

---

### 3. Hands-On Exercise (50 min)

**Goal**: Participants build a feature using the template

#### Setup (5 min)

**Prerequisites check**:
```bash
# Participants should have already:
# - Cloned the repo
# - Ran npm install
# - Verified apps run

# Now checkout the base branch
git checkout exercise-2-ddd-guardrails
# This gives them DDD structure but no template usage yet
```

**LLM Access**: Ensure everyone has access to Claude/ChatGPT/etc.

#### The Assignment (2 min)

**Choose ONE feature to build**:

**Option A: Message Reactions** (recommended, simpler)
- Users can react to messages with emoji (👍, ❤️, 😂)
- Backend stores reactions per message
- Frontend shows reaction counts

**Option B: User Profiles**
- Users have profiles (name, avatar color)
- Backend stores user info
- Frontend shows user name/avatar in chat

**Option C: Message History**
- Persist messages to in-memory store
- Retrieve last 50 messages on page load
- Backend provides GET endpoint

**Say**: "Pick the feature that interests you. Use the template. Let's see what happens."

#### Work Time (40 min)

**Participants work independently or in pairs**

**Instructor role**:
- Walk around
- Answer questions
- **Don't solve problems** - let LLM do it
- If stuck: "Try asking the LLM that question"

**Common issues**:

1. **"LLM didn't follow template"**
   → "Add to your prompt: 'Follow the structure exactly as shown in the template'"

2. **"Tests don't run"**
   → "Ask the LLM: 'The tests fail with [error]. Fix this.'"

3. **"I don't understand the generated code"**
   → "Good! Ask the LLM: 'Explain this code to me line by line'"

#### Checkpoints

**⏰ 10 min in**: "How many have code generated? Any issues?"

**⏰ 25 min in**: "How many have it running? Still debugging?"

**⏰ 35 min in**: "Let's start wrapping up. Get it to a working state."

**⏰ 40 min in**: "Finish up. We'll discuss in 5 minutes."

#### What Success Looks Like

Participants should have:
- ✅ Feature working (backend + frontend)
- ✅ Files organized in vertical slice structure
- ✅ At least one test (even if failing)
- ✅ Some understanding of generated code

**It's OK if**:
- ⚠️ Tests don't all pass
- ⚠️ Some bugs exist
- ⚠️ Code isn't perfect

**The point**: Experience template-guided development

---

### 4. Comparison & Discussion (20 min)

**Goal**: Reflect on what just happened

#### Share Experiences (10 min)

**Go around the room** (or call on 3-4 people):

**Questions**:
1. "Did it work? What broke?"
2. "How long did it take?"
3. "How many iterations with the LLM?"
4. "Did you understand the code it generated?"

**Key observations to highlight**:

- **Speed**: Most should finish in 30-40 min
- **Structure**: Files are organized (vs. Exercise 1's single file)
- **Tests**: Some have tests (vs. Exercise 1's zero)
- **Iteration**: Still needed multiple tries (that's normal!)

#### Show the Metrics (5 min)

Open `METRICS.md`, highlight:

| Metric | Exercise 1 (Free-form) | Exercise 3 (Template) |
|--------|-----------|-----------|
| Initial build | 15 min | 20 min |
| Files created | 2 | 19 |
| Test coverage | 0% | 75% |
| Add new feature | 10-15 min | 8-12 min |

**Say**:
- "Exercise 1 is faster initially"
- "But you just built structured code in ~30-40 min"
- "With tests included"
- "After 12 features, template approach is faster overall"

#### The Key Insight (5 min)

**Ask**: "What was different about using the template vs. free-form?"

Expected answers:
- "LLM knew where to put things"
- "Got tests automatically"
- "Easier to find code later"
- "Still needed iteration"

**Key Message**:
> "Templates don't eliminate iteration - they guide it. You still work with the LLM, but it produces structured code."

---

### 5. Pitfalls & Honesty (15 min)

**Goal**: Build credibility by being honest

Open `WHEN_NOT_TO_USE.md`

#### Quick Walkthrough (10 min)

**❌ Don't Use LLMs For** (5 min):

1. **Security-critical code**
   - "Always review auth, crypto, SQL queries"
   - Example: Generated JWT with plain text secrets

2. **Performance-critical code**
   - "LLM optimizes for readability, not speed"
   - Example: In-memory file upload crashes on large files

3. **Novel algorithms**
   - "LLM gives you common patterns, not innovative solutions"
   - Example: Asked for sorting, got bubble sort

**Say**: "You need to know enough to catch these issues"

#### Skill Atrophy (5 min)

**The big question**: "Will I forget how to code?"

**Honest answer**:
- If you ONLY vibecode blindly: Yes
- If you review and understand: No

**Safeguards**:
1. Review every line
2. Ask LLM to explain code you don't understand
3. Build some things from scratch still
4. Use for boilerplate, not learning

**Key Message**:
> "Vibecoding is a tool, like Stack Overflow. It can make you faster or make you lazy - your choice."

---

### 6. Wrap-Up (10 min)

**Goal**: Actionable takeaways

#### What You Learned (3 min)

**Recap**:
1. ✅ Templates guide LLM output to your standards
2. ✅ Still requires iteration (that's normal)
3. ✅ Initial speed vs. long-term maintainability
4. ⚠️ You still need to understand the code
5. ⚠️ Watch for security and performance issues

#### Next Steps (3 min)

**For Skeptics**:
- Try it on a side project
- Create one template for a pattern you use
- Review every line generated
- Stay skeptical - that's healthy

**For Believers**:
- Read WHEN_NOT_TO_USE.md carefully
- Watch for LLM assumptions
- Share experience with your team

**Take Away**:
- This repo (MIT license)
- `.prompts/NEW_FEATURE_TEMPLATE.md` (customize for your stack)
- `METRICS.md` (show management)

#### Q&A (4 min)

**Common questions**:

**Q: "Which LLM?"**
A: "Claude, GPT-4, Copilot - try a few. I prefer Claude for architecture."

**Q: "How do I customize the template?"**
A: "Replace the architecture with yours. Add your naming conventions. It's just markdown."

**Q: "Will this replace junior developers?"**
A: "No. You need experience to review. But it changes what we focus on."

**Q: "What if it generates insecure code?"**
A: "It will. That's why you review. Same as reviewing a junior's PR."

#### Closing

> "You just experienced template-driven vibecoding. Not a demo - you built something.
>
> Key takeaway: It's not magic, it's guided. Templates encode your standards.
>
> Try it at work. Measure it. Decide for yourself based on evidence, not hype.
>
> Thanks for coming!"

---

## Instructor Prep Checklist

### Before Workshop (1 day before)

**Send to participants**:
```markdown
# Pre-Workshop Setup

Please complete before the workshop:

1. Clone the repo: [URL]
2. Install dependencies:
   ```bash
   cd frontend && npm install
   cd ../backend/Api && dotnet restore
   ```
3. Verify it runs:
   ```bash
   # Terminal 1
   cd frontend && npm run dev

   # Terminal 2
   cd backend/Api && dotnet run
   ```
4. Ensure you have LLM access (Claude Code, ChatGPT, or GitHub Copilot)
5. Checkout exercise-2-ddd-guardrails branch

See you tomorrow!
```

### Morning of Workshop

```bash
# Test the setup
git checkout exercise-1-freeform  # For demo
git checkout exercise-2-ddd-guardrails  # For hands-on

# Ensure apps run
cd frontend && npm run dev
cd backend/Api && dotnet run

# Have these open:
# - .prompts/NEW_FEATURE_TEMPLATE.md
# - METRICS.md
# - WHEN_NOT_TO_USE.md
# - transcripts/EXERCISE-1-TRANSCRIPT.md (backup, if time)
```

### Materials Needed

- [ ] Projector/screen share working
- [ ] Participants have laptops with LLM access
- [ ] Wi-Fi stable
- [ ] Backup: pre-built example in case demo fails

---

## Timing Checkpoints

- ⏰ 0:15 - Done with opening, starting template intro
- ⏰ 0:25 - Participants starting hands-on
- ⏰ 0:50 - Halfway through hands-on
- ⏰ 1:15 - Wrapping up hands-on, starting discussion
- ⏰ 1:35 - Done with comparison, starting pitfalls
- ⏰ 1:50 - Starting wrap-up
- ⏰ 2:00 - Workshop complete

### If Running Over

**Cut** (priority order):
1. Transcript walkthrough (just mention it exists)
2. Detailed metrics (just show the summary table)
3. Exercise 2 mention

**Keep**:
- Hands-on time (the core value)
- Pitfalls discussion (builds credibility)
- Comparison (the insight)

### If Running Under

**Add**:
- Walk through transcript
- Show Exercise 2 architecture
- Deeper Q&A

---

## Success Criteria

**Workshop succeeds if participants**:
1. ✅ Built a feature using the template (even if buggy)
2. ✅ Experienced iteration with LLM
3. ✅ Understood the structure vs. free-form
4. ✅ Know when NOT to use vibecoding

**Workshop fails if**:
- ❌ Participants just watched (need hands-on)
- ❌ Everything worked perfectly (unrealistic)
- ❌ Only talked about benefits (need honesty)

---

## Participant Feature Assignments

To keep things organized, you can assign features:

**Group A: Message Reactions**
- Simpler, good for those less comfortable

**Group B: User Profiles**
- Medium complexity

**Group C: Message History**
- Backend-focused, good for backend devs

Or let everyone choose their own.

---

## Troubleshooting During Hands-On

### "LLM isn't following the template"

**Solution**: Ask them to be more explicit:
```
Using the template in .prompts/NEW_FEATURE_TEMPLATE.md,
create a Message Reactions feature.

Follow these requirements:
1. Use vertical slice architecture (Domain/Features/Reactions/)
2. Include validation and error handling
3. Write BDD tests with Gherkin scenarios
4. Follow the example structure shown for Chat feature
```

### "Tests won't compile"

**Solution**:
```
The tests have compilation errors: [paste errors]
Fix the test code to compile and run.
```

### "I don't understand the generated code"

**Solution**:
```
Explain this code to me:
[paste code]

What does each part do? Why did you structure it this way?
```

### "It generated 50 files and I'm lost"

**Solution**: Simplify the ask
```
That's too complex. Create a simpler version:
- Just the domain entity
- Just the API endpoint
- One test

Keep it minimal.
```

---

## Post-Workshop Follow-Up

**1 week later**, send email:

```markdown
Hi everyone!

Thanks for attending the vibecoding workshop. Quick follow-up:

1. Did anyone try template-driven vibecoding this week?
2. What worked? What didn't?
3. Any questions that came up?

Resources:
- Workshop repo: [URL]
- Template: .prompts/NEW_FEATURE_TEMPLATE.md
- Metrics: METRICS.md
- Limitations: WHEN_NOT_TO_USE.md

Feel free to customize the template for your stack and share back!

[Your name]
```

---

*This hands-on format is more engaging and memorable than pure demo. Participants remember what they BUILD, not what they SEE.*
