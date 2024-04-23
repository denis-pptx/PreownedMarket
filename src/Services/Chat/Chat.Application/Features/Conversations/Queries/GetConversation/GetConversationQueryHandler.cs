using AutoMapper;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetConversation;

public class GetConversationQueryHandler(
    IMapper _mapper,
    IUserContext _userContext,
    IConversationRepository _conversationRepository,
    IMessageRepository _messageRepository,
    IUserRepository _userRepository)
    : IQueryHandler<GetConversationQuery, ConversationResponse>
{
    public async Task<ConversationResponse> Handle(
        GetConversationQuery query, 
        CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByIdAsync(query.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var userId = _userContext.UserId;

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        if (!conversation.Members.Contains(user)) 
        {
            throw new ForbiddenException(ConversationErrorMessages.AlienConversation);
        }

        var messages = await _messageRepository.GetByConversationIdAsync(conversation.Id, cancellationToken);
        var messagesResponse = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResponse>>(messages);

        var response = new ConversationResponse
        {
            ConversationId = conversation.Id,
            Item = conversation.Item,
            Members = conversation.Members,
            Messages = messagesResponse
        };

        return response;
    }
}