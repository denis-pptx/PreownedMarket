using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetConversation;

public class GetConversationHandler(
    IConversationRepository _conversationRepository,
    IMessageRepository _messageRepository)
    : IQueryHandler<GetConversationQuery, GetConversationResponse>
{
    public async Task<GetConversationResponse> Handle(
        GetConversationQuery query, 
        CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByIdAsync(query.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var messages = await _messageRepository.GetByConversationIdAsync(conversation.Id, cancellationToken);

        var response = new GetConversationResponse
        {
            ConversationId = conversation.Id,
            Item = conversation.Item,
            Members = conversation.Members,
            Messages = messages
        };

        return response;
    }
}