using AutoMapper;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;
using Chat.Infrastructure.Models;

namespace Chat.Infrastructure.Mappings;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, MessageNotificationModel>()
            .ConstructUsing(message => new MessageNotificationModel(
                message.Id,
                message.Text,
                message.CreatedAt,
                message.SenderId,
                message.ConversationId));
    }
}
