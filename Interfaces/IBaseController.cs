using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Framework.Interfaces
{
    public interface IBaseController<T>
    {
        [HttpGet]
        ActionResult<ResponseBase> Get(int id, string includes);

        [HttpGet]
        ActionResult<ResponseBase> GetList(List<int> ids, string includes);

        [HttpGet]
        ActionResult<ResponseBase> GetAll(string includes);

        [HttpPost]
        ActionResult<ResponseBase> Add(T entity);

        [HttpPut]
        ActionResult<ResponseBase> Update(T entity);

        [HttpDelete]
        ActionResult<ResponseBase> Delete(int id);

        [HttpDelete]
        ActionResult<ResponseBase> DeleteList([FromBody] List<int> ids);

        [HttpDelete]
        ActionResult<ResponseBase> DeleteAll();

        [HttpPost]
        ActionResult<ResponseBase> Search(SearchRequest searchRequest);

    }
}
