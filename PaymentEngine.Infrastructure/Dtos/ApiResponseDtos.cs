using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PaymentEngine.Infrastructure.Dtos
{
    public class ApiResponseDtos
    {
        public ApiResponseCodes Code { get; set; }
        public int Result
        {
            get
            {
                return (int)Code;
            }
        }
        public string Message { get; set; }
        public static ApiResponseDtos<T> SuccessMessage<T>(T data, string message = null)
        {
            return new ApiResponseDtos<T>
            {

                Code = ApiResponseCodes.OK,
                Message = message,
                Payload = data
            };
        }
        public static ApiResponseDtos<T> ErrorMessage<T>(string error = null, ApiResponseCodes responseCodes = ApiResponseCodes.ERROR)
        {
            return new ApiResponseDtos<T>
            {
                Code = responseCodes,
                Message = error,

            };
        }
    }

    public class ApiResponseDtos<T> : ApiResponseDtos
    {
        public T Payload { get; set; } = default(T);
        public int TotalCount { get; set; }
        public List<string> ErrorList = new List<string>();
    }

    public enum ApiResponseCodes
    {
        NOT_FOUND = -3,
        INVALID_REQUEST = -2,
        [Description("Server error occured, please try again.")]
        ERROR = -1,
        [Description("FAIL")]
        FAIL = 2,
        [Description("SUCCESS")]
        OK = 1
    }

}
