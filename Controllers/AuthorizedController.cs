using Framework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public abstract class AuthorizedController : ControllerBase
    {
        protected ActionResult<ResponseBase> GetResponse<E>(E data)
        {
            return Ok(ResponseModel<E>.GetSuccess($"Returning record", data));
        }

        protected ActionResult<ResponseBase> GetListResponse(ResponseBase data)
        {
            if (data.ResponseCode == ResponseCodes.Success)
                return Ok(data);
            else if (data.ResponseCode == ResponseCodes.PartialSuccess)
                return StatusCode((int)ResponseCodes.PartialSuccess, data);
            else
                return StatusCode((int)ResponseCodes.InternalServerError, data);
        }

        protected ActionResult<ResponseBase> GetAddOrUpdateResponse<E>(E data)
        {
            
            return Ok(ResponseModel<E>.GetSuccess($"{typeof(E).Name} added/updated successfully", data));
        }

        protected ActionResult<ResponseBase> GetListResponse<E>(List<E> data)
        {
            if (data.Any())
                return Ok(ResponseModel<List<E>>.GetSuccess($"Returning record", data));
            else
                return NoContent();
        }

        protected ActionResult<ResponseBase> GetSearchResponse<T>(SearchResponse<T> data)
        {
            if (data.TotalRecords > 0)
                return Ok(ResponseModel<SearchResponse<T>>.GetSuccess($"Returning search result", data));
            else
                return NoContent();
        }

        protected ActionResult<ResponseBase> GetDeleteResponse(string entityName)
        {
            return Ok(ResponseBase.GetSuccess($"{entityName} deleted successfully"));
        }

        protected ActionResult<ResponseBase> GetDeleteListResponse(BulkOperationModel data)
        {
            if (data.ResponseCode == ResponseCodes.Success)
                return Ok(data);
            else if (data.ResponseCode == ResponseCodes.PartialSuccess)
                return StatusCode((int)ResponseCodes.PartialSuccess, data);
            else
                return StatusCode((int)ResponseCodes.InternalServerError, data);

        }
    }
}