using AutoMapper;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;
using Microsoft.VisualBasic;

namespace Chat.Application.Features.Conversations.Queries.GetUserConversations;

public class GetUserConversationsQueryHandler(
    IMapper _mapper,
    IUserContext _userContext,
    IUserRepository _userRepository,
    IConversationRepository _conversationRepository,
    IMessageRepository _messageRepository) 
    : IQueryHandler<GetUserConversationsQuery, IEnumerable<GetUserConversationResponse>>
{
    public async Task<IEnumerable<GetUserConversationResponse>> Handle(
        GetUserConversationsQuery query, 
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        var conversations = await _conversationRepository.GetByUserIdAsync(userId, cancellationToken);

        var response = conversations.Select(async x =>
        {
            var lastMessage = await _messageRepository.GetLastMessageInConversationAsync(x.Id, cancellationToken);

            var lastMessageResponse = lastMessage switch
            {
                null => default,
                _ => _mapper.Map<Message, MessageResponse>(lastMessage)
            };

            return new GetUserConversationResponse(
                x.Id, 
                x.Item, 
                lastMessageResponse, 
                x.Members);
        });

        return await Task.WhenAll(response);
    }
}