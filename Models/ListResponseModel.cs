using Framework.Exceptions;
using Framework.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Models
{

    public class BulkOperationModel : ResponseBase
    {
        [JsonIgnore]
        public int Count { get; set; }
        [JsonIgnore]
        public int Failed { get; set; }

        public BulkOperationModel(int count)
        {
            Count = count;
            Failed = 0;
        }

        public BulkOperationModel()
        {
        }

        public BulkOperationModel FinalizeResponse(int noOfOperations, string msg = "Bulk operation")
        {
            if (Errors.IsNullOrEmpty())
                SetSuccess($"{msg} successful");
            else if (Errors.Count < noOfOperations)
                SetParitalSuccess($"{msg} partially successful");
            else if (Errors.Count >= noOfOperations)
                SetInternalServerError($"{msg} failed");
            return this;
        }

        public BulkOperationModel FinalizeResponse(string msg = "Bulk operation")
        {
            if (Failed == 0)
                SetSuccess($"{msg} successful");
            else if (Failed < Count)
                SetParitalSuccess($"{msg} partially successful");
            else
                SetInternalServerError($"{msg} failed");
            return this;
        }

        public BulkOperationModel OperationFailed()
        {
            Failed++;
            return this;
        }

        public new BulkOperationModel SetByException(CustomException ex)
        {
            base.SetByException(ex);
            OperationFailed();
            return this;
        }
    }

    public class ListResponseModel<T> : ResponseModel<List<T>>
    {
        [JsonIgnore]
        public int Count { get; set; }
        [JsonIgnore]
        public int Failed { get; set; }

        public ListResponseModel(int count)
        {
            Count = count;
            Failed = 0;
        }

        public ListResponseModel()
        {
        }

        public ListResponseModel<T> FinalizeResponse(int noOfOperations, string msg = "Bulk operation")
        {
            if (Errors.IsNullOrEmpty())
                SetSuccess($"{msg} successful");
            else if (Errors.Count < noOfOperations)
                SetParitalSuccess($"{msg} partially successful");
            else if (Errors.Count >= noOfOperations)
                SetInternalServerError($"{msg} failed");
            return this;
        }

        public ListResponseModel<T> FinalizeResponse(string msg = "Bulk operation")
        {
            if (Failed == 0)
                SetSuccess($"{msg} successful");
            else if (Failed < Count)
                SetParitalSuccess($"{msg} partially successful");
            else
                SetInternalServerError($"{msg} failed");
            return this;
        }


        public new ListResponseModel<T> SetByException(CustomException ex)
        {
            base.SetByException(ex);
            OperationFailed();
            return this;
        }
        public ListResponseModel<T> OperationFailed()
        {
            Failed++;
            return this;
        }
    }
}
