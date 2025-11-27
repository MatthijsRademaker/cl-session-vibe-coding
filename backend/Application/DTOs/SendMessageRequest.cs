namespace Application.DTOs;

public record SendMessageRequest(Guid? ConversationId, string Message);
