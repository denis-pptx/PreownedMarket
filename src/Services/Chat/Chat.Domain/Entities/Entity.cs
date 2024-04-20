using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.Domain.Entities;

public abstract class Entity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
}