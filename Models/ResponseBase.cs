using Framework.Exceptions;
using Framework.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using StrList = System.Collections.Generic.List<string>;

namespace Framework.Models
{
    public enum ResponseCodes
    {
        Success = 200,
        Created = 201,
        NoContent = 204,
        PartialSuccess = 207,
        AlreadyExists = 208,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        InternalServerError = 500
    }

    public class ResponseBase
    {
        public ResponseCodes ResponseCode { get; set; }
        public string Msg { get; set; }
        public StrList Errors { get; set; }
        public StrList Warnings { get; set; }
        public ExceptionResponse Exception { get; set; }

        public ResponseBase()
        {
            Errors = new StrList();
            Warnings = new StrList();
        }

        public void Copy(ResponseBase response)
        {
            ResponseCode = response.ResponseCode;
            Msg = response.Msg;
            Errors = response.Errors;
            Warnings = response.Warnings;
            Exception = response.Exception;
        }

        public ResponseBase SetMsg(string msg)
        {
            Msg = msg;
            return this;
        }

        public ResponseBase AddError(string error)
        {
            Errors.Add(error);
            return this;
        }

        public ResponseBase AddError(StrList errors)
        {
            Errors = errors;
            return this;
        }

        public ResponseBase AddErrorsRange(StrList errors)
        {
            if (errors.NotNullOrEmpty())
                Errors.AddRange(errors);
            return this;
        }

        public ResponseBase AddWarning(string warning)
        {
            Warnings.Add(warning);
            return this;
        }
        public bool CheckSuccess()
        {
            return ResponseCode == ResponseCodes.Success || ResponseCode == ResponseCodes.Created;
        }

        public bool CheckGetSuccess()
        {
            return ResponseCode == ResponseCodes.Success || ResponseCode == ResponseCodes.PartialSuccess;
        }

        public ResponseBase SetSuccess(string msg)
        {
            ResponseCode = ResponseCodes.Success;
            Msg = msg;
            return this;
        }

        public ResponseBase SetNoContent(string msg, StrList warnings = null)
        {
            ResponseCode = ResponseCodes.NoContent;
            Msg = msg;
            if (warnings.NotNullOrEmpty())
                Warnings = warnings;
            return this;
        }

        public ResponseBase SetParitalSuccess(string operation, StrList errors = null, StrList warnings = null)
        {
            ResponseCode = ResponseCodes.PartialSuccess;
            Msg = operation;
            if (warnings.NotNullOrEmpty())
                Warnings = warnings;
            if (errors.NotNullOrEmpty())
                Errors = errors;
            return this;
        }

        public ResponseBase SetInternalServerError(string operation, Exception exception = null, StrList errors = null, StrList warnings = null)
        {
            ResponseCode = ResponseCodes.InternalServerError;
            Msg = operation;
            Exception = new ExceptionResponse(exception);
            if (errors.NotNullOrEmpty())
                Errors = errors;
            if (warnings.NotNullOrEmpty())
                Warnings = warnings;
            return this;
        }

        public ResponseBase SetBadRequest(string msg, StrList errors = null, StrList warnings = null)
        {
            ResponseCode = ResponseCodes.BadRequest;
            Msg = msg;
            if (warnings.NotNullOrEmpty())
                Warnings = warnings;
            if (errors.NotNullOrEmpty())
                Errors = errors;
            return this;
        }

        //Static init functions start here
        public static ResponseBase GetSuccess(string msg)
            => new ResponseBase().SetSuccess(msg);

        public static ResponseBase GetNoContent(string msg, StrList warnings = null)
            => new ResponseBase().SetNoContent(msg, warnings);

        public static ResponseBase GetParitalSuccess(string msg, StrList errors = null, StrList warnings = null)
            => new ResponseBase().SetParitalSuccess(msg, errors, warnings);

        public static ResponseBase GetInternalServerError(string msg, Exception exception = null, StrList errors = null, StrList warnings = null)
            => new ResponseBase().SetInternalServerError(msg, exception, errors, warnings);

        public static ResponseBase GetBadRequest(string msg, StrList errors = null, StrList warnings = null)
            => new ResponseBase().SetBadRequest(msg, errors, warnings);

        public ResponseBase SetByException(CustomException ex)
        {
            Errors.Add(ex.Message);
            if (ex.Errors.NotNullOrEmpty())
                Errors.AddRange(ex.Errors);
            if (ex.Warnings.NotNullOrEmpty())
                Warnings.AddRange(ex.Warnings);
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
