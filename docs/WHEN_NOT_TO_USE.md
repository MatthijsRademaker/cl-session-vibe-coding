# When NOT to Use Vibecoding

**Honesty builds credibility.** This workshop shows what works, but skeptical developers need to know the limitations too.

---

## ❌ Don't Use LLMs For

### 1. Novel Algorithms or Cutting-Edge Techniques

**Why Not**:
- LLMs are trained on existing code
- They suggest *common* solutions, not *innovative* ones
- Novel approaches require human creativity

**Example**:
```
❌ "Design a new compression algorithm better than gzip"
→ LLM will give you standard compression techniques

✅ "Implement gzip compression in TypeScript"
→ LLM excels at implementing known algorithms
```

**Alternative**: Use LLMs to implement *parts* of your novel approach, not design it.

---

### 2. Performance-Critical Code

**Why Not**:
- LLMs prioritize *readability* over *performance*
- Don't understand your specific performance constraints
- May suggest suboptimal data structures

**Example**:
```
❌ "Write a high-frequency trading algorithm"
→ LLM won't optimize for microseconds

❌ "Optimize this hot loop"
→ LLM might miss CPU cache effects, branch prediction, etc.

✅ "Write a first draft of this algorithm"
→ Then manually optimize the hot paths
```

**Alternative**: Let LLM write the first version, then profile and optimize yourself.

---

### 3. Security-Sensitive Code

**Why Not**:
- LLMs can suggest insecure patterns
- May use outdated security practices
- Don't understand your threat model

**Examples of LLM Security Mistakes**:
```typescript
// ❌ LLM might suggest this:
const query = `SELECT * FROM users WHERE id = ${userId}`;
// SQL injection vulnerability!

// ❌ Or this:
const token = Math.random().toString(36);
// Not cryptographically secure!

// ❌ Or this:
eval(userInput);
// Remote code execution vulnerability!
```

**Alternative**:
- Use LLM for boilerplate
- **Always** security review LLM-generated code
- Use security linters (ESLint security plugins, Semgrep)
- Have security expert review auth/crypto code

---

### 4. Company-Specific Business Logic

**Why Not**:
- LLM doesn't know your domain
- Can't learn from your internal documentation
- Will make incorrect assumptions

**Example**:
```
❌ "Calculate customer discount based on our loyalty program"
→ LLM will invent generic discount logic

✅ "Create a discount calculator interface"
→ You fill in the business rules
```

**Alternative**: Use LLM for structure, add domain logic yourself.

---

### 5. Complex State Management

**Why Not**:
- LLMs struggle with intricate state machines
- May create race conditions
- Don't understand all edge cases

**Example**:
```
❌ "Implement WebRTC peer connection state management"
→ Too many states and transitions for LLM to handle correctly

✅ "Implement a simple state machine for [specific states]"
→ Clearly define states and transitions
```

**Alternative**: Break complex state into simpler pieces.

---

### 6. When You Need to Learn

**Why Not**:
- LLM gives you the answer, you don't learn
- Can't explain *why* something works
- Creates dependency on AI

**Example**:
If you're learning React:
```
❌ "Build a React app with hooks, context, and routing"
→ You get working code but no understanding

✅ Build it yourself, use LLM for:
   - "Explain useEffect dependencies"
   - "Show example of custom hook"
   - "Debug this specific error"
```

**Alternative**: Use LLM as a **tutor**, not a **ghostwriter**.

---

### 7. Debugging Complex Production Issues

**Why Not**:
- LLM lacks context about your system
- Can't see logs, metrics, or state
- May suggest generic fixes that don't apply

**Example**:
```
❌ "Why is my app crashing in production?"
→ LLM will guess wildly

✅ "Given this stack trace, what could cause it?"
→ LLM helps interpret errors you've already captured
```

**Alternative**: Use LLM to *interpret* data you've collected, not to diagnose blind.

---

### 8. When Requirements Are Unclear

**Why Not**:
- Garbage in, garbage out
- LLM will make assumptions (possibly wrong)
- Rebuilding costs more than clarifying upfront

**Example**:
```
❌ "Build a social media app"
→ Way too vague, LLM will build something generic

✅ First: Define requirements clearly
   Then: "Build a chat component with [specific features]"
```

**Alternative**: Clarify requirements first, then use LLM.

---

### 9. Regulated Industries (Healthcare, Finance, Aviation)

**Why Not**:
- Compliance requirements
- Audit trails needed
- Liability concerns
- Certification processes don't account for AI-generated code

**Example**:
```
❌ "Generate medical diagnosis logic"
→ Regulatory nightmare, life-critical

❌ "Write aviation control software"
→ Safety-critical, must be certifiable

✅ Use for non-critical supporting tools only
```

**Alternative**: Extreme caution. Understand legal implications first.

---

### 10. When Working With Proprietary/Confidential Code

**Why Not**:
- Pasting into ChatGPT/Claude = sending to external server
- Potential data leak
- May violate NDA or company policy

**Example**:
```
❌ Pasting company codebase into ChatGPT
→ Your code is now in OpenAI's servers

✅ Use local LLM (Ollama, LM Studio) or corporate-licensed solutions
```

**Alternative**: Use approved tools only, redact sensitive parts.

---

## ⚠️ Use With Caution

### These scenarios *can* work but require extra care:

### 1. APIs You're Not Familiar With

**Issue**: LLM might use outdated API versions
**Solution**: Always check official docs, verify API version

### 2. Complex RegEx

**Issue**: LLM-generated regex can be incorrect or inefficient
**Solution**: Test thoroughly with edge cases, use regex validators

### 3. Database Queries

**Issue**: May generate inefficient queries or miss indexes
**Solution**: Run EXPLAIN PLAN, profile performance

### 4. Concurrent Code

**Issue**: Race conditions, deadlocks
**Solution**: Extra code review, stress testing

### 5. Error Handling

**Issue**: LLM may not cover all edge cases
**Solution**: Think through failure scenarios yourself

---

## ✅ LLMs Excel At

Let's be clear about what LLMs **are** good for:

### 1. Boilerplate Code
- DTOs, interfaces, basic CRUD
- 10x faster than manual typing

### 2. Standard Patterns
- Repository pattern, Factory pattern, Builder pattern
- LLM knows these cold

### 3. Test Scaffolding
- Setup, teardown, mock objects
- Test data generation

### 4. Documentation
- JSDoc comments, OpenAPI specs
- README files, changelogs

### 5. Code Translation
- Python → TypeScript
- REST API → GraphQL

### 6. Refactoring Assistance
- Extracting methods, renaming variables
- Converting class → functional components

### 7. Quick Prototypes
- MVPs, demos, proof-of-concepts
- When you're exploring ideas

### 8. Learning & Exploration
- "Show me how to use this library"
- "Explain this error message"

---

## The Vibecoding Workflow That Works

```
1. ✅ Use LLM for initial structure
   ↓
2. ✅ Review code critically
   ↓
3. ✅ Test thoroughly
   ↓
4. ✅ Refine manually where needed
   ↓
5. ✅ Add domain-specific logic yourself
   ↓
6. ✅ Security review
   ↓
7. ✅ Performance test
   ↓
8. ✅ Commit with confidence
```

**Never**: Generate → Paste → Ship

**Always**: Generate → Review → Test → Refine → Ship

---

## Red Flags: When to Stop Using the LLM

Stop and do it manually if:

- 🚩 LLM keeps making the same mistake
- 🚩 You're spending more time fixing than writing
- 🚩 The code works but you don't understand it
- 🚩 LLM suggests something that "feels wrong"
- 🚩 You're in a domain you don't understand
- 🚩 Security or performance is critical
- 🚩 The problem is actually hard (not just tedious)

**Rule of thumb**: If you couldn't code-review the LLM's output confidently, don't use it.

---

## Skill Atrophy: Real Concern

**The Fear**: "If I use LLMs, will I forget how to code?"

**The Reality**: Depends on *how* you use them.

### ❌ Atrophy Risk (Don't do this)
```
- Never read the code LLM generates
- Accept everything blindly
- Use LLM for everything, including simple tasks
- Don't understand the patterns it uses
```

### ✅ Skill Building (Do this)
```
- Understand every line before accepting
- Use LLM for tedious parts, code complex parts yourself
- Learn from LLM's patterns and techniques
- Challenge yourself regularly without LLM
```

**Analogy**: Calculators didn't make us forget math. We just don't do arithmetic by hand anymore.

---

## The Honest Truth

Vibecoding is a **tool**, not a **replacement**.

**Good developers with LLMs** > Good developers without LLMs > Bad developers with LLMs

The LLM amplifies your skills, it doesn't replace them.

**You still need**:
- Problem decomposition skills
- Code review skills
- Debugging skills
- Domain knowledge
- Security awareness
- Performance intuition

**The LLM helps with**:
- Syntax recall
- Boilerplate generation
- Pattern implementation
- First drafts

---

## Discussion Questions for Workshop

1. Which of these limitations surprises you most?
2. Have you tried LLM coding and hit any of these walls?
3. What checks would you put in place at your company?
4. How do you balance speed vs. safety?
5. What's your line in the sand for "I'll do this manually"?

---

## Final Advice

**Use vibecoding when**:
- It makes you faster at your *current skill level*
- You can confidently review the output
- The stakes are low or you have safety nets (tests, reviews)
- It's solving a tedious problem, not a hard problem

**Don't use vibecoding when**:
- You don't understand the domain
- Security or performance is critical
- You can't test the output
- Compliance requires human authorship
- You're trying to skip learning something important

**The Goal**: 10x your productivity on the 80% of code that's boilerplate and patterns, so you can spend more time on the 20% that's actually hard.

---

*This workshop teaches you HOW to vibecode effectively. But knowing WHEN to vibecode is equally important.*
