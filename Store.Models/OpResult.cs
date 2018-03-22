using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model
{
    public class OpResult
    {
        public bool Success;
        public List<string> Errors = new List<string>();
        public List<string> Warnings = new List<string>();
        public Exception exception;

        public static OpResult FailureResult(List<string> errors)
        {
            return new OpResult
            {
                Success = false,
                Errors = errors
            };
        }

        public static OpResult FailureResult(string error)
        {
            return FailureResult(new List<string>() { error });
        }

        public static OpResult ExceptionResult(Exception e)
        {
            return new OpResult
            {
                Success = false,
                exception = e
            };
        }

    }
    public class OpResult<T>: OpResult
    {
        public T Result;

        public static OpResult<T> SuccessResult(T obj)
        {
            return new OpResult<T>
            {
                Success = true,
                Result = obj
            };
        }

        
    }
}
