namespace Domain.Entities;

/// <summary>
/// Represents a chat conversation aggregate root
/// </summary>
public class Conversation
{
    private readonly List<Message> _messages = new();

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastMessageAt { get; private set; }
    public IReadOnlyList<Message> Messages => _messages.AsReadOnly();

    private Conversation() { }

    public Conversation(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public Message AddMessage(string content, MessageSender sender)
    {
        var message = new Message(content, sender);
        _messages.Add(message);
        LastMessageAt = DateTime.UtcNow;
        return message;
    }

    public Message? GetLastUserMessage()
    {
        return _messages
            .Where(m => m.Sender == MessageSender.User)
            .OrderByDescending(m => m.Timestamp)
            .FirstOrDefault();
    }
}
