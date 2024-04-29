using Chat.Domain.Repositories;
using Contracts.Users;
using MassTransit;

namespace Chat.Application.Consumers.Users;

public class UserDeletedConsumer(
    IUserRepository _userRepository,
    IItemRepository _itemRepository,
    IConversationRepository _conversationRepository,
    IMessageRepository _messageRepository) 
    : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var user = await _userRepository.GetByIdAsync(context.Message.UserId);

        if (user is null)
        {
            return;
        }

        await _userRepository.DeleteAsync(user, context.CancellationToken);

        await _itemRepository.DeleteByUserIdAsync(user.Id, context.CancellationToken);

        await _conversationRepository.DeleteByUserIdAsync(user.Id, context.CancellationToken);

        await _messageRepository.DeleteByUserIdAsync(user.Id, context.CancellationToken);
    }
}