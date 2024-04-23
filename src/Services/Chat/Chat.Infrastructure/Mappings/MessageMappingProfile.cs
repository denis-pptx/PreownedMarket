using AutoMapper;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;
using Chat.Infrastructure.Models.Messages;

namespace Chat.Infrastructure.Mappings;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, MessageNotification>()
            .ConstructUsing(message => new MessageNotification(
                message.Id,
                message.Text,
                message.CreatedAt,
                message.SenderId,
                message.ConversationId));
    }
}
