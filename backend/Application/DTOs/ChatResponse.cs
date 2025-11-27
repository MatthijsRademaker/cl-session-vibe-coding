namespace Application.DTOs;

public record ChatResponse(
    Guid ConversationId,
    string Response,
    DateTime Timestamp
);
