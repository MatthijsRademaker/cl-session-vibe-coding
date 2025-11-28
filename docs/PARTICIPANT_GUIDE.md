# Participant Guide - Vibecoding Workshop

**Welcome!** This hands-on workshop explores template-driven LLM-assisted coding (vibecoding).

---

## Before the Workshop

### 1. Clone the Repository
```bash
git clone <repo-url>
cd cl-sessie
```

### 2. Install Dependencies

**Frontend**:
```bash
cd frontend
npm install
```

**Backend**:
```bash
cd backend/Api
dotnet restore
```

### 3. Verify Setup

**Terminal 1 - Frontend**:
```bash
cd frontend
npm run dev
# Should open: http://localhost:5173
```

**Terminal 2 - Backend**:
```bash
cd backend/Api
dotnet run
# Should start: http://localhost:5000
```

**Test it**: Open http://localhost:5173 - you should see a blank Vue app.

### 4. Checkout Starting Branch

```bash
git checkout exercise-2-ddd-guardrails
```

This gives you a DDD-structured backend to build on.

### 5. LLM Access

Ensure you have access to ONE of:
- **Claude Code** (recommended for this workshop)
- **ChatGPT** (GPT-4)
- **GitHub Copilot Chat**

Test it works before the workshop!

---

## During the Workshop

### You Will Build

Choose ONE feature to build (we'll guide you):

**Option A: Message Reactions** ⭐ (Recommended)
- Add emoji reactions (👍, ❤️, 😂) to messages
- Backend stores reactions per message
- Frontend displays reaction counts

**Option B: User Profiles**
- Users have names and avatar colors
- Backend stores user info
- Frontend shows user identity in chat

**Option C: Message History**
- Persist messages in-memory
- Retrieve last 50 messages on page load
- Backend provides history endpoint

### You Will Use

**The Template**: `.prompts/NEW_FEATURE_TEMPLATE.md`
- Guides LLM to produce structured code
- Includes architecture, testing, and quality requirements
- Your job: Point the LLM to this template

### Expected Outcome

After ~40 minutes, you should have:
- ✅ Feature working (even if buggy)
- ✅ Code organized in vertical slice structure
- ✅ At least one test
- ✅ Understanding of what was generated

**It's OK if**:
- ⚠️ Some tests fail
- ⚠️ You needed multiple iterations
- ⚠️ Code isn't perfect

**The point**: Experience template-guided development.

---

## How to Use the Template

### Step 1: Read the Template

Open `.prompts/NEW_FEATURE_TEMPLATE.md` and skim it.

**Key sections**:
- Architecture (where files go)
- Quality requirements (validation, errors)
- Testing (BDD with Gherkin)
- Example (chat feature pattern)

### Step 2: Craft Your Prompt

**Example prompt for Message Reactions**:

```
Using the template in .prompts/NEW_FEATURE_TEMPLATE.md,
create a Message Reactions feature.

Requirements:
- Users can react to messages with emoji (👍, ❤️, 😂)
- Backend stores reactions per message
- Frontend displays reaction counts next to messages

Follow the template exactly:
1. Use vertical slice architecture (Domain/Features/Reactions/)
2. Include validation and error handling
3. Write BDD tests with Gherkin scenarios
4. Follow the Chat feature example structure
```

### Step 3: Generate Code

Paste the prompt into your LLM tool.

**LLM will generate**:
- Domain entities
- Use cases
- Repository interfaces
- Infrastructure implementations
- API endpoints
- Frontend components
- Tests

### Step 4: Implement

Copy the generated code into your project.

**Expected structure**:
```
backend/
  Domain/Features/Reactions/
    Reaction.cs
    IReactionRepository.cs
  Application/Features/Reactions/
    AddReactionUseCase.cs
  Infrastructure/Features/Reactions/
    InMemoryReactionRepository.cs
  Api/Controllers/
    ReactionsController.cs

frontend/src/features/reactions/
  ReactionButton.vue
  ReactionList.vue
```

### Step 5: Test & Iterate

Run the apps. **Expect issues!**

Common problems:
- Compilation errors
- Tests don't run
- Feature doesn't work

**Fix them by asking the LLM**:
```
The code has this error: [paste error]
Fix it.
```

**Or**:
```
The test fails with: [paste failure]
What's wrong and how do I fix it?
```

### Step 6: Understand

**Don't just copy code blindly!**

If you don't understand something:
```
Explain this code to me:
[paste code]

What does each part do?
```

---

## Tips for Success

### Do ✅
- Read the template first
- Be specific in your prompts
- Iterate with the LLM (it won't be perfect first try)
- Ask the LLM to explain code you don't understand
- Review every line generated

### Don't ❌
- Copy code blindly without understanding
- Expect perfection on first generation
- Skip the template (defeats the purpose)
- Panic when errors occur (iteration is normal)
- Ship code you don't understand

---

## Common Issues & Solutions

### Issue: "LLM didn't follow the template"

**Solution**: Be more explicit
```
You didn't follow the template structure.
Please regenerate using vertical slice architecture
as shown in .prompts/NEW_FEATURE_TEMPLATE.md.

Put files in Domain/Features/Reactions/.
```

### Issue: "I got 50 files and I'm overwhelmed"

**Solution**: Simplify
```
That's too much. Create a minimal version:
- One domain entity
- One API endpoint
- One test

Keep it simple.
```

### Issue: "Tests won't compile"

**Solution**: Ask for fixes
```
Tests have these compilation errors:
[paste errors]

Fix the test code.
```

### Issue: "Feature doesn't work"

**Solution**: Debug with LLM
```
The feature fails with this error:
[paste error or describe behavior]

What's wrong? How do I fix it?
```

---

## What to Expect

### Realistic Timeline

- **0-10 min**: Generate initial code
- **10-25 min**: Copy code, fix compilation errors
- **25-35 min**: Get it running, fix bugs
- **35-40 min**: Final touches, basic testing

### Realistic Outcomes

**Good outcome**:
- Feature mostly works
- Structure is clean
- You understand the code
- 2-3 iterations needed

**Great outcome**:
- Feature fully works
- Tests pass
- You learned something
- Can explain what you built

**OK outcome**:
- Feature partially works
- You struggled but learned
- Got unstuck with help
- See the potential

**Bad outcome**:
- Copied code blindly
- Don't understand what you built
- Never asked LLM questions
- Gave up at first error

---

## After the Workshop

### Try It at Work

**Start small**:
1. Pick a simple feature you need
2. Customize the template for your stack
3. Use it with your LLM
4. Review the output carefully
5. Measure the results

### Share Your Experience

- What worked?
- What didn't?
- How long did it take?
- Would you use this again?

### Keep Learning

**Read**:
- `METRICS.md` - Quantifiable comparisons
- `WHEN_NOT_TO_USE.md` - Honest limitations
- `transcripts/` - See how others iterated
- `MAINTENANCE_CHALLENGES.md` - Try more scenarios

**Practice**:
- Build another feature from scratch
- Create your own template
- Try different LLMs
- Compare approaches

---

## Key Takeaways

**Templates guide LLMs to produce structured code**
- Not magic, but guided
- You still iterate
- You still review

**Speed + Structure is possible**
- Exercise 1 (free-form): Fast but messy
- Exercise 3 (template): Fast AND clean
- Break-even: ~12 features

**You still need to understand the code**
- Review every line
- Ask questions
- Don't ship what you don't understand

**Vibecoding is a tool, not autopilot**
- Use it where it makes sense
- Skip it where it doesn't
- Stay skeptical, measure results

---

## Questions?

During the workshop:
- Raise your hand
- Ask the instructor
- Help each other

After the workshop:
- Open an issue in the repo
- Email the instructor
- Share what you learned

---

**Good luck! Remember: iteration is normal, perfection is not the goal.**
