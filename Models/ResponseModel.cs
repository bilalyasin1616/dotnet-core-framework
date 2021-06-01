using Framework.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using StrList = System.Collections.Generic.List<string>;

namespace Framework.Models
{

    public class ResponseModel<T> : ResponseBase
    {
        public T Data { get; set; }

        public new ResponseModel<T> AddError(string error)
        {
            Errors.Add(error);
            return this;
        }

        public ResponseModel<T> AddErrors(StrList errors)
        {
            Errors = errors;
            return this;
        }

        public new ResponseModel<T> AddWarning(string warning)
        {
            Warnings.Add(warning);
            return this;
        }

        public ResponseModel<T> SetSuccess(string msg, T data)
        {
            _ = SetSuccess(msg);
            Data = data;
            return this;
        }

        public new ResponseModel<T> SetNoContent(string msg, StrList warnings = null)
        {
            _ = base.SetNoContent(msg, warnings);
            return this;
        }

        public ResponseModel<T> SetParitalSuccess(string msg, T data, StrList errors = null, StrList warnings = null)
        {
            _ = SetParitalSuccess(msg, errors, warnings);
            Data = data;
            return this;
        }

        public new ResponseModel<T> SetInternalServerError(string msg, Exception exception = null, StrList errors = null, StrList warnings = null)
        {
            _ = base.SetInternalServerError(msg, exception, errors, warnings);
            return this;
        }

        public new ResponseModel<T> SetBadRequest(string msg, StrList errors = null, StrList warnings = null)
        {
            _ = base.SetBadRequest(msg, errors, warnings);
            return this;
        }

        //Static functions
        public static ResponseModel<T> GetSuccess(string msg, T data)
            => new ResponseModel<T>().SetSuccess(msg, data);

        public new static ResponseModel<T> GetNoContent(string msg, StrList warnings = null)
            => new ResponseModel<T>().SetNoContent(msg, warnings);

        public static ResponseModel<T> GetParitalSuccess(string msg, T data, StrList errors = null, StrList warnings = null)
            => new ResponseModel<T>().SetParitalSuccess(msg, data, errors, warnings);

        public new static ResponseModel<T> GetInternalServerError(string msg, Exception exception = null, StrList errors = null, StrList warnings = null)
            => new ResponseModel<T>().SetInternalServerError(msg, exception, errors, warnings);

        public new static ResponseModel<T> GetBadRequest(string msg, StrList errors = null, StrList warnings = null)
            => new ResponseModel<T>().SetBadRequest(msg, errors, warnings);

        public new ResponseModel<T> SetByException(CustomException ex)
        {
            base.SetByException(ex);
            return this;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
