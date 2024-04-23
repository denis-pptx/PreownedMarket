using AutoMapper;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;

namespace Chat.Application.Mappings;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, MessageResponse>()
            .ConstructUsing(message => new MessageResponse(
                message.Id, 
                message.Text, 
                message.CreatedAt, 
                message.SenderId,
                message.ConversationId));
    }
}