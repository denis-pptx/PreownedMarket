using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Abstractions.Notifications;
using Chat.Application.Models.Conversations.Responses;
using Chat.Domain.Entities;
using Chat.Domain.ErrorMessages;
using Chat.Domain.Repositories;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Conversations.Commands.CreateConversation;

public class CreateConversationCommandHandler(
    IUserContext userContext,
    IConversationNotificationService notificationService,
    IItemService itemService,
    IUserRepository userRepository,
    IConversationRepository conversationRepository)
    : ICommandHandler<CreateConversationCommand, CreateConversationResponse>
{
    public async Task<CreateConversationResponse> Handle(
        CreateConversationCommand command,
        CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var customer = await userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(customer);

        var item = await itemService.GetByIdAsync(command.Request.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        if (item.UserId == customer.Id)
        {
            throw new ConflictException(ConversationErrorMessages.MyselfConversation);
        }

        var existingConversation = await conversationRepository.FirstOrDefaultAsync(
            conversation => 
                conversation.ItemId == item.Id && 
                conversation.MembersIds.Contains(customer.Id),
            cancellationToken);

        if (existingConversation is not null)
        {
            throw new ConflictException(ConversationErrorMessages.AlreadyExists);
        }
        
        if (item.IsActive == false)
        {
            throw new ConflictException(ConversationErrorMessages.CreateOnInactiveItem);
        }

        var seller = await userRepository.GetByIdAsync(item.UserId, cancellationToken);
        NotFoundException.ThrowIfNull(seller);

        var conversation = new Conversation
        {
            ItemId = item.Id,
            MembersIds = [customer.Id, seller.Id]
        };

        await conversationRepository.AddAsync(conversation, cancellationToken);

        await notificationService.CreateConversationAsync(conversation, cancellationToken);

        return new CreateConversationResponse(
            conversation.Id,
            item,
            [customer, seller]);
    }
}