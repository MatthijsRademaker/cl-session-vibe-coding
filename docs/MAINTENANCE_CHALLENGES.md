# Maintenance Challenges

This document contains **hands-on scenarios** to test how well each architecture handles real-world maintenance tasks.

> **Workshop Instructions**: Complete these challenges for each exercise and compare the results.

---

## Challenge 1: Add New Message Type

### Requirements
Currently messages are either from "user" or "bot". Add a third type: "system" for automated notifications.

**System messages should:**
- Be styled differently (gray background)
- Not require bot response
- Show timestamp only, no sender name
- Examples: "User joined", "Conversation saved", "Connection lost"

### Exercise 1: Free-form Architecture

**Files to Change**: ❓ (figure it out)

**Steps**:
1. Find where messages are created
2. Add "system" type
3. Update UI to handle system messages
4. Style system messages differently

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐⭐ (medium-hard, logic is intertwined)

**Issues Encountered**:
- [ ] Had to search through code to find relevant parts
- [ ] Unsure where to add the logic
- [ ] Changes affected unrelated functionality
- [ ] No way to test in isolation

### Exercise 2: DDD Architecture

**Files to Change**:
1. `Domain/Entities/Message.cs` - Add System to enum
2. `frontend/src/components/ChatBot.vue` - Add styling for system messages
3. `Application/UseCases/SendMessageUseCase.cs` - Skip bot response for system messages

**Steps**:
1. Update `MessageSender` enum
2. Add conditional logic in use case
3. Add CSS class for system messages
4. Test

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐ (medium, clear where changes go)

**Advantages**:
- Clear where to add enum value (Domain)
- Business logic isolated (Application)
- UI changes isolated (Frontend)

### Exercise 3: Template-driven Architecture

**Files to Change**:
1. `Domain/Features/Chat/Entities/Message.cs` - Add System to enum
2. `frontend/src/features/chat/components/ChatBot.vue` - Add styling
3. `Application/Features/Chat/UseCases/SendMessageUseCase.cs` - Skip bot response

**Steps** (follow template pattern):
1. Update domain model
2. Update use case logic
3. Update frontend component
4. Add test case for system messages

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐ (easy, template shows where everything goes)

**Advantages**:
- Vertical slice makes all chat-related code co-located
- Template shows the pattern to follow
- Tests ensure nothing broke

---

## Challenge 2: Add Message Persistence

### Requirements
Messages should be saved so when you refresh the page, the conversation history is still there.

**Implementation**:
- Save to LocalStorage (frontend)
- Load on component mount
- Clear history button

### Exercise 1

**Files to Change**: ❓

**Considerations**:
- Where should localStorage logic go?
- How to serialize/deserialize messages?
- When to save? (after each message, on unmount, debounced?)

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐⭐⭐ (hard, no clear separation)

**Common Issues**:
- Logic mixes with component code
- Hard to test localStorage behavior
- Easy to forget edge cases (storage full, corrupted data)

### Exercise 2

**Files to Change**:
1. Create `Infrastructure/Services/LocalStorageConversationRepository.cs`
2. Update DI registration to use LocalStorage instead of InMemory
3. Frontend stays the same (backend handles persistence)

**OR** (frontend persistence):
1. Create composable: `frontend/src/composables/usePersistedMessages.ts`
2. Update `ChatBot.vue` to use composable

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐ (medium, know where it belongs)

**Advantages**:
- Can swap repository implementation
- Testable with mock storage
- Doesn't affect business logic

### Exercise 3

**Files to Change**:
1. Create `frontend/src/features/chat/composables/usePersistence.ts`
2. Update `chat.api.ts` to use persistence layer
3. Add tests for persistence logic

**Template guidance**: See `.prompts/NEW_FEATURE_TEMPLATE.md` section on "Adding Cross-Cutting Concerns"

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐ (easy, template pattern applies)

---

## Challenge 3: Handle Connection Failures

### Requirements
When the backend is unreachable:
- Show "Connecting..." status
- Retry 3 times with exponential backoff
- After 3 failures, show "Offline mode" banner
- Queue messages to send when back online

### Exercise 1

**Files to Change**: ❓

**Complexity**:
- Retry logic
- Backoff calculation
- State management (online/offline)
- Message queue
- All in one component

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐⭐⭐⭐ (very hard, lots of mixed concerns)

**Risk**: High chance of introducing bugs in existing functionality

### Exercise 2

**Files to Change**:
1. Create `Infrastructure/Services/ResilientChatbotService.cs` (decorator pattern)
2. Implement retry logic with Polly library
3. Update DI registration
4. Frontend shows connection status from API responses

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐⭐ (medium-hard, but isolated)

**Advantages**:
- Retry logic separate from business logic
- Can test retry behavior in isolation
- Doesn't affect domain model

### Exercise 3

**Files to Change**:
1. Create `frontend/src/features/chat/composables/useConnectionStatus.ts`
2. Create `frontend/src/features/chat/composables/useMessageQueue.ts`
3. Update `chat.api.ts` with retry logic
4. Add connection banner component

**Template guidance**: Composables section shows how to structure this

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐ (medium, composables pattern is clear)

---

## Challenge 4: Add User Typing Indicator

### Requirements
Show "Bot is typing..." when waiting for response, and "User is typing..." to other users (future multiplayer feature)

**Frontend**:
- Typing indicator component
- Show/hide based on state

**Backend** (for multiplayer):
- WebSocket endpoint to broadcast typing status
- Typing timeout (stop after 3s of no activity)

### Exercise 1

**Files to Change**: ❓

**Concerns**:
- When to show indicator?
- How to manage typing state?
- Where does WebSocket logic go?

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐⭐ (medium, but gets messy)

### Exercise 2

**Files to Change**:
1. `Domain/Events/UserTypingEvent.cs` (domain event)
2. `Infrastructure/Services/WebSocketService.cs`
3. `Application/Handlers/UserTypingHandler.cs`
4. Frontend: TypingIndicator component

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐ (medium, domain events help)

### Exercise 3

**Files to Change**:
1. `frontend/src/features/chat/components/TypingIndicator.vue`
2. `frontend/src/features/chat/composables/useTypingIndicator.ts`
3. `frontend/src/features/chat/api/websocket.ts`

**Template guidance**: Real-time features section

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐ (easy, feature module contains everything)

---

## Challenge 5: Performance Optimization

### Requirements
Chat is slow with 1000+ messages. Optimize:
- Virtual scrolling (only render visible messages)
- Pagination (load older messages on scroll)
- Lazy loading of message content

### Exercise 1

**Files to Change**: ❓ (heavy refactoring needed)

**Problem**: Everything in one component, hard to optimize without breaking

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐⭐⭐⭐ (very hard, likely requires rewrite)

**Risk**: May need to refactor entire component

### Exercise 2

**Files to Change**:
1. `Application/Queries/GetMessagesPagedQuery.cs` (new query)
2. `Infrastructure/Repositories/ConversationRepository.cs` (add pagination)
3. Frontend: Virtual scroll library integration

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐⭐ (medium-hard, but clean boundaries)

**Advantages**:
- Repository handles pagination
- Query is separate concern
- Frontend can optimize independently

### Exercise 3

**Files to Change**:
1. `Application/Features/Chat/Queries/GetMessagesPaged.cs`
2. `frontend/src/features/chat/composables/useVirtualScroll.ts`
3. `frontend/src/features/chat/components/MessageList.vue` (separate from ChatBot)

**Expected Time**: ______ minutes

**Actual Time**: ______ minutes

**Difficulty**: ⭐⭐ (medium, composable pattern is perfect for this)

---

## Challenge 6: Add Feature Flag

### Requirements
Add a feature flag to enable/disable the chatbot for specific users.

**Implementation**:
- Check flag before showing chatbot
- If disabled, show "Chat unavailable" message
- Flag controlled by backend endpoint

### Exercise 1

**Difficulty**: ⭐⭐⭐ (where does this check go? component? API call?)

### Exercise 2

**Files to Change**:
1. `Application/Services/FeatureFlagService.cs`
2. `Api/Middleware/FeatureFlagMiddleware.cs`
3. Frontend checks API response

**Difficulty**: ⭐⭐ (middleware is right place)

### Exercise 3

**Files to Change**:
1. `frontend/src/features/chat/composables/useFeatureFlag.ts`
2. Conditional rendering in component

**Difficulty**: ⭐ (composable pattern again)

---

## Scoring Your Experience

For each challenge, rate:
1. How easy was it to find where code goes? (1-5)
2. How confident were you in your changes? (1-5)
3. How likely is it you introduced bugs? (1-5, lower is better)
4. Would you want to maintain this codebase? (Yes/No)

### Exercise 1 Scores:
- Clarity: ___ / 5
- Confidence: ___ / 5
- Bug Risk: ___ / 5
- Maintainable: Yes / No

### Exercise 2 Scores:
- Clarity: ___ / 5
- Confidence: ___ / 5
- Bug Risk: ___ / 5
- Maintainable: Yes / No

### Exercise 3 Scores:
- Clarity: ___ / 5
- Confidence: ___ / 5
- Bug Risk: ___ / 5
- Maintainable: Yes / No

---

## Discussion Questions

1. Which architecture made it **easiest to find** where code should go?
2. Which architecture made you most **confident** the change wouldn't break anything?
3. Which architecture would you choose for a **6-month project**? Why?
4. What surprised you about the maintenance experience?
5. How does this change your view on upfront architectural investment?

---

## Key Takeaways

The goal isn't to prove one approach is always better. It's to show:

1. **Exercise 1** is fast initially but maintenance slows you down
2. **Exercise 2** has upfront cost but consistent maintenance speed
3. **Exercise 3** balances both: fast to build AND maintain

The right choice depends on:
- Project lifetime
- Team size
- Feature complexity
- Maintenance frequency

**For skeptical devs**: Try these challenges yourself. The difference becomes obvious when you actually work with the code.
