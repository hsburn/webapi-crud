using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace telstra.demo.Models
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        Guid Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        Guid Version { get; set; }
    }

    public abstract class Document : IDocument
    {
        public Guid Id { get; set; }
        public Guid Version { get; set; }
    }
}