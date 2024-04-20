using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.Domain.Entities;

public class Item : Entity
{
    public string Title { get; set; } = string.Empty;
    public string? FirstImagePath { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
}