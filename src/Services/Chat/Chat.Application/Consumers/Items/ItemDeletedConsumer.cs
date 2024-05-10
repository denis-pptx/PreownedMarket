using Chat.Domain.Repositories;
using MassTransit;
using Shared.Events.Items;

namespace Chat.Application.Consumers.Items;

public class ItemDeletedConsumer(
    IConversationRepository _conversationRepository,
    IMessageRepository _messageRepository)
    : IConsumer<ItemDeletedEvent>
{
    public async Task Consume(ConsumeContext<ItemDeletedEvent> context)
    {
        var conversationsIds = await _conversationRepository
            .GetConversationsIdsByItemIdAsync(context.Message.ItemId);

        foreach (var conversationId in conversationsIds)
        {
            await _messageRepository.DeleteByConversationIdAsync(conversationId);
        }

        await _conversationRepository.DeleteByItemIdAsync(context.Message.ItemId);
    }
}