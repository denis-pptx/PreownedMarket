using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Abstractions.Notifications;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Commands.CreateConversation;

public class CreateConversationCommandHandler(
    IUserContext _userContext,
    IConversationNotificationService _notificationService,
    IItemService _itemService,
    IUserRepository _userRepository,
    IConversationRepository _conversationRepository)
    : ICommandHandler<CreateConversationCommand, CreateConversationResponse>
{
    public async Task<CreateConversationResponse> Handle(
        CreateConversationCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var customer = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(customer);

        var item = await _itemService.GetByIdAsync(command.Request.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        if (item.UserId == customer.Id)
        {
            throw new ConflictException(ConversationErrorMessages.MyselfConversation);
        }

        var existingConversation = await _conversationRepository.FirstOrDefaultAsync(
            conversation => 
                conversation.ItemId == item.Id && 
                conversation.MembersIds.Contains(customer.Id),
            cancellationToken);

        if (existingConversation is not null)
        {
            throw new ConflictException(ConversationErrorMessages.AlreadyExists);
        }

        var seller = await _userRepository.GetByIdAsync(item.UserId, cancellationToken);
        NotFoundException.ThrowIfNull(seller);

        var conversation = new Conversation
        {
            ItemId = item.Id,
            MembersIds = [customer.Id, seller.Id]
        };

        await _conversationRepository.AddAsync(conversation, cancellationToken);

        await _notificationService.CreateConversationAsync(conversation, cancellationToken);

        return new CreateConversationResponse(
            conversation.Id,
            item,
            [customer, seller]);
    }
}