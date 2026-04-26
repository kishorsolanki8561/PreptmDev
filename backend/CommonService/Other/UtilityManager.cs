using CommonService.Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommonService.Other
{
    public class UtilityManager : DapperGenericRepo
    {
        public class ResponseWriter<T>
        {
            public static string WriteResponse(int ResponseCode, T DataOrError, string? message = null, long records = 0)
            {
                if (ResponseCode == StatusCodes.Status200OK)
                {
                    SuccessModel<T> response = new SuccessModel<T>();
                    response.Message = message == null ? "Ok" : message;
                    response.ResponseCode = ResponseCode;
                    response.IsSuccess = true;
                    response.Data = DataOrError;
                    response.TotalRecords = records;


                    return Newtonsoft.Json.JsonConvert.SerializeObject(response);
                }
                else if (ResponseCode == StatusCodes.Status404NotFound)
                {
                    SuccessModel<T> response = new SuccessModel<T>();
                    response.Message = message == null ? "Ok" : message;
                    response.IsSuccess = false;
                    response.ResponseCode = ResponseCode;
                    response.Data = DataOrError;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(response);
                }
                else if (ResponseCode == StatusCodes.Status400BadRequest)
                {
                    SuccessModel<T> response = new SuccessModel<T>();
                    response.Message = message == null ? "Ok" : message;
                    response.IsSuccess = false;
                    response.ResponseCode = ResponseCode;
                    response.Data = DataOrError;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(response);
                }
                else
                {
                    ErrorModel<T> response = new ErrorModel<T>();
                    response.Message = message == null ? "Error" : message;
                    response.ResponseCode = ResponseCode;
                    response.IsSuccess = false;
                    response.Errors = DataOrError;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(response);
                }
            }
        }



        public class InheritResponseModel
        {
            public bool IsSuccess { get; set; }
            public string? Message { get; set; }
            public int ResponseCode { get; set; }
            public long TotalRecords { get; set; }

        }

        public class ExceptionViewModel
        {
            public string? PropertyName { get; set; }
            public object? Error { get; set; }
        }

        public class SuccessModel<T> : InheritResponseModel
        {
            public T? Data { get; set; }
        }

        public class ErrorModel<T> : InheritResponseModel
        {
            public T? Errors { get; set; }
        }

        public class ServiceResponse<T>
        {
            public bool IsSuccess { get; set; } = true;
            public string? Message { get; set; }
            public int StatusCode { get; set; } //= IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError;
            public T Data { get; set; }
            public object OtherData
            {
                get;
                set;
            }
            public long TotalRecords { get; set; }
            [JsonIgnore]
            public string? Exception { get; set; }
        }

        public virtual ServiceResponse<T> SetResultStatus<T>(T objData, string Message, bool IsSuccess, string exception = "", string validationMessage = "", long records = 0,int statusCode = 0,object? otherData = null)
        {
            ServiceResponse<T> objReturn = new ServiceResponse<T>();
            objReturn.Message = Message;
            objReturn.IsSuccess = IsSuccess;
            objReturn.Data = objData;
            objReturn.OtherData = otherData;
            objReturn.Exception = exception;
            objReturn.TotalRecords = records;

            if(statusCode > 0)
            {
                objReturn.StatusCode = statusCode;
            }
            else
            {
                objReturn.StatusCode = IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError;
                if (objReturn.IsSuccess && objReturn.Data == null)
                {
                    objReturn.StatusCode = objReturn.Data != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;
                }
            }
            return objReturn;
        }



        public class PostValidator<T>
        {
            public static object IsValid(ref bool IsSuccess, T model)
            {
                List<ValidationResult> results = new List<ValidationResult>();
                ValidationContext context = new ValidationContext(model, null, null);
                if (Validator.TryValidateObject(model, context, results, true))
                {
                    IsSuccess = true;
                    return model;
                }
                else
                {
                    List<string> errors = results.Select(x => x.ErrorMessage).ToList();
                    return ResponseWriter<List<string>>.WriteResponse(StatusCodes.Status400BadRequest, errors);
                }
            }
        }

    }
}
