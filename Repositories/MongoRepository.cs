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

        public Task DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<IEnumerable<TDocument>> List()
        {
            return await collection.Find(doc => true).ToListAsync();
        }

        public Task<TDocument> GetById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return collection.Find(filter).FirstOrDefaultAsync();
        }

        public Task Insert(TDocument document)
        {
            return collection.InsertOneAsync(document);
        }

        public Task Update(string id, TDocument document)
        {
            var objectId = new ObjectId(id);
            document.Id = objectId;

            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return collection.FindOneAndReplaceAsync(filter, document);
        }
    }
}