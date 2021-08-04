using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using telstra.demo.Models;

namespace telstra.demo.Repositories
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> GetById(string id);
        Task<IEnumerable<TDocument>> List(Pagination pagination);
        Task Insert(TDocument document);
        Task Update(string id, string version, TDocument document);
        Task<DeleteResult> DeleteById(string id);
    }
}