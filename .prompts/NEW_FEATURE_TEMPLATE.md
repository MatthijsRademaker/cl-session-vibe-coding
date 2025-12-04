# Feature Implementation Template

This document provides a **reusable template** for implementing new features using DDD + Vertical Slice Architecture with BDD testing.

## Architecture Overview

### Vertical Slice Architecture
We use **vertical slicing** where each feature is self-contained rather than organized by technical layers.

```
Domain/
  Features/
    {FeatureName}/
      Entities/
      ValueObjects/
      Events/
      Interfaces/
      Services/

Application/
  Features/
    {FeatureName}/
      Commands/
      Queries/
      DTOs/
      Handlers/

Infrastructure/
  Features/
    {FeatureName}/
      Repositories/
      Services/
      Configurations/

Api/
  Features/
    {FeatureName}/
      Controllers/

Tests/
  Features/
    {FeatureName}/
      Domain.Tests/
      Application.Tests/
      {FeatureName}.feature  (BDD scenarios)
```

### Frontend Modularity

```
frontend/src/
  features/
    {feature-name}/
      components/
      composables/
      types/
      api/
```

## Step-by-Step Feature Implementation

### 1. Domain Layer (Business Logic)

**Location**: `Domain/Features/{FeatureName}/`

**Create**:
- `Entities/` - Aggregate roots and entities
- `ValueObjects/` - Immutable domain concepts
- `Events/` - Domain events
- `Interfaces/` - Repository and service contracts

**Rules**:
- ✅ No dependencies on other layers
- ✅ Pure business logic only
- ✅ Rich domain models with behavior
- ✅ Private setters for encapsulation
- ✅ Validation in constructors
- ❌ No infrastructure concerns (DB, HTTP, etc.)
- ❌ No DTOs (those are in Application)

**Example**:
```csharp
// Domain/Features/Notifications/Entities/Notification.cs
namespace Domain.Features.Notifications.Entities;

public class Notification
{
    public Guid Id { get; private set; }
    public string Message { get; private set; }
    public NotificationType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsRead { get; private set; }

    private Notification() { }

    public Notification(string message, NotificationType type)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be empty");

        Id = Guid.NewGuid();
        Message = message;
        Type = type;
        CreatedAt = DateTime.UtcNow;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}

public enum NotificationType
{
    Info,
    Warning,
    Success,
    Error
}
```

### 2. Application Layer (Use Cases)

**Location**: `Application/Features/{FeatureName}/`

**Create**:
- `Commands/` - Write operations (CreateCommand, UpdateCommand)
- `Queries/` - Read operations (GetByIdQuery, ListQuery)
- `DTOs/` - Data transfer objects
- `Handlers/` - Command/Query handlers

**Rules**:
- ✅ Orchestrates domain logic
- ✅ Uses repository interfaces
- ✅ Returns DTOs (not domain entities)
- ✅ Handles transactions
- ❌ No business rules (delegate to Domain)
- ❌ No infrastructure details

**Example**:
```csharp
// Application/Features/Notifications/Commands/CreateNotificationCommand.cs
namespace Application.Features.Notifications.Commands;

public record CreateNotificationCommand(
    string Message,
    NotificationType Type
);

// Application/Features/Notifications/Handlers/CreateNotificationHandler.cs
public class CreateNotificationHandler
{
    private readonly INotificationRepository _repository;

    public CreateNotificationHandler(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotificationDto> HandleAsync(CreateNotificationCommand command)
    {
        // Domain handles validation and business rules
        var notification = new Notification(command.Message, command.Type);

        // Infrastructure handles persistence
        await _repository.CreateAsync(notification);

        // Return DTO, not domain entity
        return new NotificationDto(
            notification.Id,
            notification.Message,
            notification.Type.ToString(),
            notification.CreatedAt,
            notification.IsRead
        );
    }
}

// Application/Features/Notifications/DTOs/NotificationDto.cs
public record NotificationDto(
    Guid Id,
    string Message,
    string Type,
    DateTime CreatedAt,
    bool IsRead
);
```

### 3. Infrastructure Layer (Implementation)

**Location**: `Infrastructure/Features/{FeatureName}/`

**Create**:
- `Repositories/` - Repository implementations
- `Services/` - External service integrations
- `Configurations/` - EF Core configurations (if using DB)

**Rules**:
- ✅ Implements domain interfaces
- ✅ External dependencies (DB, APIs, file system)
- ✅ Can depend on Domain and Application
- ❌ Domain should NOT depend on Infrastructure

**Example**:
```csharp
// Infrastructure/Features/Notifications/Repositories/InMemoryNotificationRepository.cs
namespace Infrastructure.Features.Notifications.Repositories;

public class InMemoryNotificationRepository : INotificationRepository
{
    private readonly List<Notification> _notifications = new();

    public Task<Notification> CreateAsync(Notification notification)
    {
        _notifications.Add(notification);
        return Task.FromResult(notification);
    }

    public Task<Notification?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_notifications.FirstOrDefault(n => n.Id == id));
    }

    public Task<IEnumerable<Notification>> GetUnreadAsync()
    {
        return Task.FromResult(_notifications.Where(n => !n.IsRead));
    }
}
```

### 4. API Layer (HTTP Endpoints)

**Location**: `Api/Features/{FeatureName}/Controllers/`

**Create**:
- `{Feature}Controller.cs` - REST endpoints

**Rules**:
- ✅ Thin controllers (delegate to handlers)
- ✅ HTTP concerns only (routing, status codes)
- ✅ Input validation (basic)
- ❌ No business logic
- ❌ Don't call repositories directly

**Example**:
```csharp
// Api/Features/Notifications/Controllers/NotificationsController.cs
namespace Api.Features.Notifications.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly CreateNotificationHandler _createHandler;

    public NotificationsController(CreateNotificationHandler createHandler)
    {
        _createHandler = createHandler;
    }

    [HttpPost]
    public async Task<ActionResult<NotificationDto>> Create(
        [FromBody] CreateNotificationCommand command)
    {
        try
        {
            var notification = await _createHandler.HandleAsync(command);
            return CreatedAtAction(
                nameof(GetById),
                new { id = notification.Id },
                notification
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationDto>> GetById(Guid id)
    {
        // Implementation
    }
}
```

### 5. BDD Tests (Reqnroll/SpecFlow)

**Location**: `Tests/Features/{FeatureName}/`

**Create**:
- `{FeatureName}.feature` - Gherkin scenarios
- `StepDefinitions/{FeatureName}Steps.cs` - Step implementations
- `{FeatureName}Tests.cs` - Unit tests

**Feature File Template**:
```gherkin
# Tests/Features/Notifications/Notifications.feature
Feature: Notifications
    As a user
    I want to receive notifications
    So that I stay informed about important events

Background:
    Given the notification system is initialized

Scenario: Create a new notification
    When I create a notification with message "Hello World" and type "Info"
    Then the notification should be created successfully
    And the notification should have type "Info"
    And the notification should be marked as unread

Scenario: Mark notification as read
    Given a notification exists with message "Test" and type "Info"
    When I mark the notification as read
    Then the notification should be marked as read

Scenario: Cannot create notification with empty message
    When I attempt to create a notification with empty message
    Then the operation should fail with "Message cannot be empty"

Scenario Outline: Create notifications with different types
    When I create a notification with message "<message>" and type "<type>"
    Then the notification should be created successfully
    And the notification type should be "<type>"

    Examples:
        | message        | type    |
        | Info message   | Info    |
        | Warning alert  | Warning |
        | Success note   | Success |
        | Error occurred | Error   |
```

**Step Definitions Template**:
```csharp
// Tests/Features/Notifications/StepDefinitions/NotificationSteps.cs
using TechTalk.SpecFlow;

[Binding]
public class NotificationSteps
{
    private INotificationRepository _repository;
    private Notification? _notification;
    private Exception? _exception;

    [Given(@"the notification system is initialized")]
    public void GivenTheNotificationSystemIsInitialized()
    {
        _repository = new InMemoryNotificationRepository();
    }

    [When(@"I create a notification with message ""(.*)"" and type ""(.*)""")]
    public void WhenICreateANotification(string message, string type)
    {
        try
        {
            var notifType = Enum.Parse<NotificationType>(type);
            _notification = new Notification(message, notifType);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
    }

    [Then(@"the notification should be created successfully")]
    public void ThenTheNotificationShouldBeCreatedSuccessfully()
    {
        _notification.Should().NotBeNull();
        _notification!.Id.Should().NotBeEmpty();
    }

    [Then(@"the notification should have type ""(.*)""")]
    public void ThenTheNotificationShouldHaveType(string type)
    {
        _notification!.Type.ToString().Should().Be(type);
    }
}
```

### 6. Frontend Module

**Location**: `frontend/src/features/{feature-name}/`

**Create**:
- `components/` - Vue components for this feature
- `composables/` - Reusable composition functions
- `types/` - TypeScript interfaces
- `api/` - API client functions

**Structure**:
```
frontend/src/features/notifications/
  ├── components/
  │   ├── NotificationToast.vue
  │   └── NotificationList.vue
  ├── composables/
  │   └── useNotifications.ts
  ├── types/
  │   └── notification.types.ts
  └── api/
      └── notifications.api.ts
```

**Example - Types**:
```typescript
// frontend/src/features/notifications/types/notification.types.ts
export interface Notification {
  id: string
  message: string
  type: 'Info' | 'Warning' | 'Success' | 'Error'
  createdAt: Date
  isRead: boolean
}

export interface CreateNotificationRequest {
  message: string
  type: string
}
```

**Example - API Client**:
```typescript
// frontend/src/features/notifications/api/notifications.api.ts
import type { Notification, CreateNotificationRequest } from '../types/notification.types'

const API_BASE = 'http://localhost:5000/api'

export const notificationsApi = {
  async create(request: CreateNotificationRequest): Promise<Notification> {
    const response = await fetch(`${API_BASE}/notifications`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request)
    })
    if (!response.ok) throw new Error('Failed to create notification')
    return response.json()
  },

  async getUnread(): Promise<Notification[]> {
    const response = await fetch(`${API_BASE}/notifications/unread`)
    if (!response.ok) throw new Error('Failed to fetch notifications')
    return response.json()
  }
}
```

**Example - Composable**:
```typescript
// frontend/src/features/notifications/composables/useNotifications.ts
import { ref } from 'vue'
import { notificationsApi } from '../api/notifications.api'
import type { Notification } from '../types/notification.types'

export function useNotifications() {
  const notifications = ref<Notification[]>([])
  const isLoading = ref(false)

  const fetchUnread = async () => {
    isLoading.value = true
    try {
      notifications.value = await notificationsApi.getUnread()
    } finally {
      isLoading.value = false
    }
  }

  const showNotification = (message: string, type: 'Info' | 'Warning' | 'Success' | 'Error') => {
    return notificationsApi.create({ message, type })
  }

  return {
    notifications,
    isLoading,
    fetchUnread,
    showNotification
  }
}
```

**Example - Component**:
```vue
<!-- frontend/src/features/notifications/components/NotificationToast.vue -->
<script setup lang="ts">
import { computed } from 'vue'
import type { Notification } from '../types/notification.types'

const props = defineProps<{
  notification: Notification
}>()

const emit = defineEmits<{
  close: []
}>()

const typeStyles = computed(() => {
  const styles = {
    Info: 'bg-blue-500',
    Warning: 'bg-yellow-500',
    Success: 'bg-green-500',
    Error: 'bg-red-500'
  }
  return styles[props.notification.type]
})
</script>

<template>
  <div :class="['toast', typeStyles]">
    <p>{{ notification.message }}</p>
    <button @click="emit('close')">×</button>
  </div>
</template>

<style scoped>
.toast {
  padding: 1rem;
  border-radius: 8px;
  color: white;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
```

## Dependency Injection Setup

**Update** `Api/Program.cs`:

```csharp
// Feature: Notifications
builder.Services.AddScoped<INotificationRepository, InMemoryNotificationRepository>();
builder.Services.AddScoped<CreateNotificationHandler>();
builder.Services.AddScoped<GetUnreadNotificationsHandler>();
```

## Testing Checklist

- [ ] Domain entity validates correctly
- [ ] Domain entity throws on invalid input
- [ ] Handler creates entity through repository
- [ ] Handler returns DTO (not entity)
- [ ] Controller returns correct status codes
- [ ] BDD scenarios cover happy path
- [ ] BDD scenarios cover error cases
- [ ] Frontend composable handles loading states
- [ ] Frontend component displays correctly
- [ ] End-to-end flow works

## Code Quality Standards

### Naming Conventions
- **Commands**: `{Action}{Entity}Command` (e.g., `CreateNotificationCommand`)
- **Handlers**: `{Action}{Entity}Handler` (e.g., `CreateNotificationHandler`)
- **DTOs**: `{Entity}Dto` (e.g., `NotificationDto`)
- **Repositories**: `I{Entity}Repository` / `{Implementation}{Entity}Repository`

### File Organization
- One class per file
- File name matches class name
- Namespace matches folder structure

### Testing
- BDD features in Gherkin (Given/When/Then)
- Descriptive scenario names
- Cover positive and negative cases
- Use scenario outlines for similar tests

## Summary

This template ensures:
✅ **Vertical slices** - Each feature is self-contained
✅ **DDD principles** - Clear separation of concerns
✅ **Testability** - BDD scenarios, unit tests
✅ **Consistency** - Every feature follows same structure
✅ **Modularity** - Frontend mirrors backend organization
✅ **Maintainability** - Easy to find and modify code

**Use this template every time you add a new feature to maintain consistency across the codebase.**
