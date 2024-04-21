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
            .ConstructUsing(x => new MessageNotificationModel(
                x.Id,
                x.Text,
                x.CreatedAt,
                x.SenderId,
                x.ConversationId));
    }
}
