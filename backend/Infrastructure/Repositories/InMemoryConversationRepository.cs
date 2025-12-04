using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of conversation repository
/// In production, this would be replaced with database implementation
/// </summary>
public class InMemoryConversationRepository : IConversationRepository
{
    private readonly Dictionary<Guid, Conversation> _conversations = new();

    public Task<Conversation> GetByIdAsync(Guid id)
    {
        if (!_conversations.TryGetValue(id, out var conversation))
        {
            throw new KeyNotFoundException($"Conversation with ID {id} not found");
        }

        return Task.FromResult(conversation);
    }

    public Task<Conversation> CreateAsync(Conversation conversation)
    {
        _conversations[conversation.Id] = conversation;
        return Task.FromResult(conversation);
    }

    public Task<Conversation> UpdateAsync(Conversation conversation)
    {
        if (!_conversations.ContainsKey(conversation.Id))
        {
            throw new KeyNotFoundException($"Conversation with ID {conversation.Id} not found");
        }

        _conversations[conversation.Id] = conversation;
        return Task.FromResult(conversation);
    }

    public Task<IEnumerable<Conversation>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Conversation>>(_conversations.Values);
    }
}
