using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WebApi
{
    public class ApiResult
    {
        public Object data { get; private set; }
        public List<string> errors { get; private set; }
        public string status { get; private set; }

        public static ApiResult Success(Object data = null)
        {
            return new ApiResult { status = ApiResultStatus.Success, data = data };
        }

        public static ApiResult Failure()
        {
            return new ApiResult { status = ApiResultStatus.Failure };
        }

        public static ApiResult Failure(List<string> errors)
        {
            return new ApiResult { status = ApiResultStatus.Failure, errors = errors };
        }

        public static ApiResult Failure(string error)
        {
            return new ApiResult { status = ApiResultStatus.Failure, errors = new List<string> { error } };
        }

    }

    public class ApiResultStatus
    {//Uisng this and not a boolean so as to leave flexibility for additional status like 'warnings'
        public const string Success = "success";
        public const string Failure = "fail";
    }
}