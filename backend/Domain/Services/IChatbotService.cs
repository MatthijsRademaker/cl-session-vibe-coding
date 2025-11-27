namespace Domain.Services;

/// <summary>
/// Domain service for chatbot response generation
/// </summary>
public interface IChatbotService
{
    Task<string> GenerateResponseAsync(string userMessage);
}
