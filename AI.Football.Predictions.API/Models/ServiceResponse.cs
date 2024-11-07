using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.API.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public ServiceResponse() { }

        public ServiceResponse(T data, bool success = true, string message = "")
        {
            Data = data;
            Success = success;
            Message = message;
        }

        public static ServiceResponse<T> SuccessResponse(T data, string message = "") =>
            new ServiceResponse<T> { Data = data, Success = true, Message = message };

        public static ServiceResponse<T> ErrorResponse(string message) =>
            new ServiceResponse<T> { Success = false, Message = message };
    }

}