using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace telstra.demo.Models
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        string InternalId { get; }
    }

    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public string InternalId => Id.ToString();
    }
}