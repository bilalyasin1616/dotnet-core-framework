using Framework.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Framework.Interfaces
{
    public interface IBaseBusinessObject<T, S>
    {
        S ParseState(IHttpContextAccessor httpContextAccessor);
        T Get(int id, string includes);
        List<T> Get(List<int> ids, string includes);
        List<T> GetAll(string includes);
        T Add(T entity, bool saveChanges = true);
        T Update(T entity, bool saveChanges = true);
        void Delete(int id, bool saveChanges = true);
        void DeleteAll();
        BulkOperationModel DeleteList(List<int> ids, bool saveChanges = true);
        SearchResponse Search(List<FilterTermModel> filters, string sortBy, string sortOrder, string range, string includes);
    }
}