using System.Collections.Generic;
using System.Threading.Tasks;
using telstra.demo.Models;

namespace telstra.demo.Repositories
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> GetById(string id);
        Task<IEnumerable<TDocument>> List();
        Task Insert(TDocument document);
        Task Update(string id, TDocument document);
        Task DeleteById(string id);
    }
}