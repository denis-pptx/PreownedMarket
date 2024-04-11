using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.Domain.Entities;

public abstract class Entity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
}