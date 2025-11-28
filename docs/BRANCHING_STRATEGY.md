# Branching Strategy

This document explains the workshop's Git branching structure and how to navigate between exercises.

---

## Branch Structure

```
master (base + docs)
├── Scaffolded frontend & backend
├── All documentation (transcripts, metrics, etc.)
│
├── exercise-1-freeform
│   ├── Everything from master
│   ├── + Free-form chatbot implementation
│   └── + All documentation
│
├── exercise-2-ddd-guardrails
│   ├── Everything from exercise-1
│   ├── + DDD restructured backend
│   └── + All documentation
│
└── exercise-3-prompt-engineering
    ├── Everything from exercise-2
    ├── + Feature template
    ├── + BDD test setup
    └── + All documentation
```

---

## Why This Structure?

### Documentation Lives Everywhere

The workshop documentation (transcripts, metrics, challenges) should be accessible from **every branch** because:

1. **Meta-content**: Explains the workshop itself, not part of exercises
2. **Comparison**: Participants need to compare exercises from any branch
3. **Context**: Each exercise is better understood with full context

### Exercise Code Lives On Its Branch

The actual implementations are branch-specific:
- `master`: Just scaffolding
- `exercise-1-freeform`: Chatbot built free-form
- `exercise-2-ddd-guardrails`: Same chatbot, DDD structure
- `exercise-3-prompt-engineering`: Templates + examples

---

## Navigating the Workshop

### For Participants

**Start here:**
```bash
git checkout master
# Read README.md, METRICS.md, transcripts/
```

**View Exercise 1:**
```bash
git checkout exercise-1-freeform
# See the free-form implementation
# All docs still available
```

**View Exercise 2:**
```bash
git checkout exercise-2-ddd-guardrails
# See the DDD implementation
# Compare with Exercise 1
```

**View Exercise 3:**
```bash
git checkout exercise-3-prompt-engineering
# See the template approach
# All previous work + new template
```

**Compare Exercises:**
```bash
# From any branch, you can:
cat METRICS.md              # See quantifiable comparisons
cat transcripts/EXERCISE-1-TRANSCRIPT.md  # See how Exercise 1 was built
cat MAINTENANCE_CHALLENGES.md  # Try challenges
```

---

## What's On Each Branch

### `master` (Base Layer)
**Code**:
- Scaffolded Vue 3 frontend
- Scaffolded .NET 8 backend
- No custom features yet

**Documentation**:
- ✅ README.md (workshop overview)
- ✅ CLAUDE.md (architecture guide)
- ✅ METRICS.md (quantifiable comparisons)
- ✅ transcripts/ (conversation histories)
- ✅ MAINTENANCE_CHALLENGES.md (hands-on scenarios)
- ✅ WHEN_NOT_TO_USE.md (limitations)
- ✅ WORKSHOP_IMPROVEMENTS.md (roadmap)

### `exercise-1-freeform`
**Code** (additional):
- ChatBot.vue component
- Minimal API endpoint in Program.cs
- ~350 LOC, 2 files

**Documentation**:
- ✅ All from master
- ✅ EXERCISE-1.md (analysis of this exercise)

### `exercise-2-ddd-guardrails`
**Code** (additional):
- Complete DDD restructure:
  - Domain/ (4 files)
  - Application/ (3 files)
  - Infrastructure/ (2 files)
  - Api/Controllers/ (1 file)
- ~850 LOC, 13 files
- Frontend unchanged from Exercise 1

**Documentation**:
- ✅ All from exercise-1
- ✅ backend/EXERCISE-2.md (DDD deep-dive)

### `exercise-3-prompt-engineering`
**Code** (additional):
- Tests project with Reqnroll
- Test configuration
- ~1430 LOC total (includes tests)

**Documentation**:
- ✅ All from exercise-2
- ✅ .prompts/NEW_FEATURE_TEMPLATE.md (reusable template)
- ✅ EXERCISE-3.md (template benefits)

---

## For Instructors

### Adding Documentation

Documentation should be added to **all branches** using cherry-pick:

```bash
# 1. Create docs on master
git checkout master
# ... create/edit docs ...
git add .
git commit -m "Add new documentation"

# 2. Get the commit hash
COMMIT_HASH=$(git rev-parse HEAD)

# 3. Cherry-pick to all exercise branches
git checkout exercise-1-freeform
git cherry-pick $COMMIT_HASH

git checkout exercise-2-ddd-guardrails
git cherry-pick $COMMIT_HASH

git checkout exercise-3-prompt-engineering
git cherry-pick $COMMIT_HASH
```

### Adding Exercise Code

Exercise-specific code stays on its branch:

```bash
# Exercise 1 changes
git checkout exercise-1-freeform
# ... make changes ...
git commit -m "Update Exercise 1"

# Exercise 2 builds on Exercise 1
git checkout exercise-2-ddd-guardrails
git merge exercise-1-freeform  # or cherry-pick specific commits
# ... add DDD structure ...
git commit -m "Update Exercise 2"

# Exercise 3 builds on Exercise 2
git checkout exercise-3-prompt-engineering
git merge exercise-2-ddd-guardrails
# ... add templates ...
git commit -m "Update Exercise 3"
```

---

## Verification Commands

### Check What's On Current Branch

```bash
# See all docs
ls *.md

# See transcripts
ls transcripts/

# See exercises
ls frontend/src/components/
ls backend/

# See prompts/templates
ls .prompts/
```

### Compare Branches

```bash
# See differences between exercises
git diff master exercise-1-freeform -- frontend/
git diff exercise-1-freeform exercise-2-ddd-guardrails -- backend/
git diff exercise-2-ddd-guardrails exercise-3-prompt-engineering -- .prompts/
```

### Verify Documentation Exists

```bash
# From any branch, these should all work:
cat README.md
cat METRICS.md
cat transcripts/EXERCISE-1-TRANSCRIPT.md
cat MAINTENANCE_CHALLENGES.md
cat WHEN_NOT_TO_USE.md
```

---

## Branch Maintenance

### If Documentation Is Missing From A Branch

```bash
# Find the commit that added it
git log master --oneline | grep "documentation"

# Cherry-pick it
git checkout <branch-missing-docs>
git cherry-pick <commit-hash>
```

### If Exercises Get Out Of Sync

```bash
# Rebase exercise branches if needed
git checkout exercise-2-ddd-guardrails
git rebase exercise-1-freeform

git checkout exercise-3-prompt-engineering
git rebase exercise-2-ddd-guardrails
```

---

## Common Issues

### "I don't see METRICS.md on exercise-1"

**Problem**: Documentation wasn't cherry-picked to that branch
**Solution**: See "If Documentation Is Missing" above

### "Exercise 2 doesn't have Exercise 1 code"

**Problem**: Branches got out of sync
**Solution**: See "If Exercises Get Out Of Sync" above

### "I made docs changes on exercise-3 but they're not on master"

**Problem**: Wrong workflow - docs should start on master
**Solution**: Cherry-pick back to master, then to other branches

---

## Quick Reference

| Task | Command |
|------|---------|
| **View base layer** | `git checkout master` |
| **View Exercise 1** | `git checkout exercise-1-freeform` |
| **View Exercise 2** | `git checkout exercise-2-ddd-guardrails` |
| **View Exercise 3** | `git checkout exercise-3-prompt-engineering` |
| **See all branches** | `git branch -a` |
| **See branch history** | `git log --oneline --graph --all` |
| **Compare exercises** | `git diff branch1 branch2` |

---

## Summary

**Key Principle**: Documentation is everywhere, code is branch-specific.

This ensures:
- ✅ Participants always have context
- ✅ Comparisons are easy
- ✅ Each exercise stands alone
- ✅ Code changes are isolated

**Result**: A clean, navigable workshop structure where participants can easily switch between exercises while always having access to supporting materials.
