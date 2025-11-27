using Domain.Services;

namespace Infrastructure.Services;

/// <summary>
/// Rule-based chatbot implementation
/// Implements domain service interface in infrastructure layer
/// </summary>
public class RuleBasedChatbotService : IChatbotService
{
    private readonly string[] _responses = new[]
    {
        "That's an interesting point! Tell me more.",
        "I understand. How does that make you feel?",
        "Fascinating! I'd love to hear your thoughts on that.",
        "That's a great question! Let me think about it...",
        "I see what you mean. Have you considered other perspectives?",
        "Thanks for sharing that with me!",
        "That's definitely something worth exploring further.",
        "Interesting perspective! What led you to that conclusion?",
        "I appreciate you telling me about this.",
        "That's quite thought-provoking!"
    };

    private readonly string[] _greetings = { "hello", "hi", "hey", "greetings", "howdy" };
    private readonly string[] _questions = { "how are you", "what's up", "how's it going" };

    public Task<string> GenerateResponseAsync(string userMessage)
    {
        var message = userMessage.ToLower().Trim();
        string response;

        // Rule-based response logic
        if (_greetings.Any(g => message.Contains(g)))
        {
            response = "Hello! I'm doing great, thanks for asking. How can I help you today?";
        }
        else if (_questions.Any(q => message.Contains(q)))
        {
            response = "I'm doing wonderfully! Thanks for asking. How about you?";
        }
        else if (message.Contains("bye") || message.Contains("goodbye"))
        {
            response = "Goodbye! It was nice chatting with you. Come back anytime!";
        }
        else if (message.Contains("help"))
        {
            response = "I'm here to chat with you! Ask me anything or just share what's on your mind.";
        }
        else if (message.Contains("?"))
        {
            response = $"That's a great question! {_responses[Random.Shared.Next(_responses.Length)]}";
        }
        else
        {
            response = _responses[Random.Shared.Next(_responses.Length)];
        }

        return Task.FromResult(response);
    }
}
