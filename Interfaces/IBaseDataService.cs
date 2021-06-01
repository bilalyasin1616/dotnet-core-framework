using Framework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IBaseDataService<T>
    {
        Task AddAsync(T entity, bool saveChanges);
        void Update(T entity, bool saveChanges);
        T Get(int id, List<string> includes);
        List<T> Get(List<int> ids, List<string> includes);
        List<T> GetAll(List<string> includes);
        SearchResponse Search(List<FilterTermModel> filters, string sortBy, string sortOrder,
            int? lowerRange, int? upperRange, List<string> includes);
    }
}
