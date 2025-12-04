using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Repository contract for conversation persistence
/// </summary>
public interface IConversationRepository
{
    Task<Conversation> GetByIdAsync(Guid id);
    Task<Conversation> CreateAsync(Conversation conversation);
    Task<Conversation> UpdateAsync(Conversation conversation);
    Task<IEnumerable<Conversation>> GetAllAsync();
}
