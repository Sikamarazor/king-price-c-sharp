using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kingPriceApi.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessMessage(T data, string message = "")
        => new()
        { Success = true, Message = message, Data = data };

        public static ApiResponse<T> Error(string message)
        => new()
        { Success = false, Message = message };
    }
}