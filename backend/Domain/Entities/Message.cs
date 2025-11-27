namespace Domain.Entities;

/// <summary>
/// Value object representing a single message in a conversation
/// </summary>
public class Message
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public MessageSender Sender { get; private set; }
    public DateTime Timestamp { get; private set; }

    private Message() { }

    public Message(string content, MessageSender sender)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Message content cannot be empty", nameof(content));

        Id = Guid.NewGuid();
        Content = content;
        Sender = sender;
        Timestamp = DateTime.UtcNow;
    }
}

public enum MessageSender
{
    User,
    Bot
}
