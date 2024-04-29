using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.Domain.Entities;

public class Conversation : Entity
{
    [BsonRepresentation(BsonType.String)]
    public Guid ItemId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public IEnumerable<Guid> MembersIds { get; set; } = [];
}