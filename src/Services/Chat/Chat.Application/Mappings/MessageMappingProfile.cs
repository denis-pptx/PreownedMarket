using AutoMapper;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;

namespace Chat.Application.Mappings;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, MessageResponse>()
            .ConstructUsing(x => new MessageResponse(
                x.Id, 
                x.Text, 
                x.CreatedAt, 
                x.SenderId, 
                x.ConversationId));
    }
}