using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.Domain.Entities;

public class Item : Entity
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = string.Empty;
}