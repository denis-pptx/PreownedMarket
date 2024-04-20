using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Commands;

public class CreateConversationCommandHandler(
    IUserContext _userContext, 
    IUserRepository _userRepository,
    IItemRepository _itemRepository,
    IConversationRepository _conversationRepository)
    : ICommandHandler<CreateConversationCommand, CreateConversationResponse>
{
    public async Task<CreateConversationResponse> Handle(
        CreateConversationCommand command, 
        CancellationToken cancellationToken)
    {
        var request = command.Request;
        
        var userId = _userContext.UserId;

        var customer = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(customer);

        var item = await _itemRepository.GetByIdAsync(request.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        if (item.UserId == customer.Id)
        {
            throw new ConflictException(ConversationErrorMessages.MyselfConversation);
        }

        var existingConversation = await _conversationRepository.FirstOrDefaultAsync(
            conversation => conversation.Item.Id == item.Id && conversation.Members.Contains(customer),
            cancellationToken);

        if (existingConversation != null)
        {
            throw new ConflictException(ConversationErrorMessages.AlreadyExists);
        }
        
        var seller = await _userRepository.GetByIdAsync(item.UserId, cancellationToken);
        NotFoundException.ThrowIfNull(seller);

        IEnumerable<User> conversationMembers = [customer, seller];

        var conversation = new Conversation
        {
            Item = item,
            Members = conversationMembers
        };

        await _conversationRepository.AddAsync(conversation, cancellationToken);

        return new CreateConversationResponse(
            conversation.Id, 
            item,
            conversationMembers);
    }
}