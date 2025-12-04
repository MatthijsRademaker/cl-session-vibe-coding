using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;

namespace Application.UseCases;

/// <summary>
/// Use case for sending a message and getting a bot response
/// Orchestrates domain logic without containing business rules
/// </summary>
public class SendMessageUseCase
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IChatbotService _chatbotService;

    public SendMessageUseCase(
        IConversationRepository conversationRepository,
        IChatbotService chatbotService)
    {
        _conversationRepository = conversationRepository;
        _chatbotService = chatbotService;
    }

    public async Task<ChatResponse> ExecuteAsync(SendMessageRequest request)
    {
        // Get or create conversation
        Conversation conversation;
        if (request.ConversationId.HasValue)
        {
            conversation = await _conversationRepository.GetByIdAsync(request.ConversationId.Value);
        }
        else
        {
            conversation = new Conversation();
            await _conversationRepository.CreateAsync(conversation);
        }

        // Add user message to conversation
        conversation.AddMessage(request.Message, MessageSender.User);

        // Generate bot response
        var botResponseText = await _chatbotService.GenerateResponseAsync(request.Message);

        // Add bot response to conversation
        var botMessage = conversation.AddMessage(botResponseText, MessageSender.Bot);

        // Persist conversation
        await _conversationRepository.UpdateAsync(conversation);

        return new ChatResponse(
            conversation.Id,
            botResponseText,
            botMessage.Timestamp
        );
    }
}
