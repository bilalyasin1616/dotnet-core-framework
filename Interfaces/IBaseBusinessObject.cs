using Framework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IBaseBusinessObject<T, S>
    {
        Task<T> AddAsync(T entity);
        Task DeleteAllAsync();
        Task DeleteAsync(int accountId);
        void DeleteList(List<int> accountIds);
        Task<List<T>> GetAllAsync(int after, int limit);
        Task<T> GetAsync(int accountId);
        Task<SearchResponse<T>> SearchAsync(SearchRequest sr);
        Task<T> UpdateAsync(T entity);
    }
}