using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.Domain.Entities;

public class Message : Entity
{
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid SenderId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid ConversationId { get; set; }
}