using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.Domain.Entities;

public class Message : Entity
{
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string SenderId { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string ConversationId { get; set; } = string.Empty;
}