using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using telstra.demo.Attributes;
using telstra.demo.Models;

namespace telstra.demo.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> collection;

        public MongoRepository(DbConnection dbConnection)
        {
            var database = new MongoClient(dbConnection.ConnectionString).GetDatabase(dbConnection.Name);
            this.collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute) documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true) 
                .FirstOrDefault())?.CollectionName;
        }

        public Task<DeleteResult> DeleteById(string id)
        {
            var objectId = new Guid(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<TDocument>> List(Pagination pagination)
        {
            return await collection
                .Find(doc => true)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Limit(pagination.PageSize)
                .ToListAsync();
        }

        public Task<TDocument> GetById(string id)
        {
            var objectId = new Guid(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return collection.Find(filter).FirstOrDefaultAsync();
        }

        public Task Insert(TDocument document)
        {
            return collection.InsertOneAsync(document);
        }

        public Task Update(string id, string version, TDocument document)
        {
            document.Id = new Guid(id);
            document.Version = Guid.NewGuid();

            var filter = Builders<TDocument>.Filter.And(
                Builders<TDocument>.Filter.Eq(doc => doc.Id, new Guid(id)),
                Builders<TDocument>.Filter.Eq(doc => doc.Version, new Guid(version))
            );

            return collection.FindOneAndReplaceAsync(filter, document);
        }
    }
}