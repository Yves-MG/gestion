using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Core.Services.Interfaces;

namespace Gestion.Core.Services.Implementations
{
    public class ServiceResult<T> : IServiceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public ServiceResult(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static IServiceResult<T> Failure(string message)
        {
            return new ServiceResult<T>(false, message, default(T)!);
        }

        public static ServiceResult<T> Success(string message, T data = default(T)!)
        {
            return new ServiceResult<T>(true, message, data);
        }

        //internal static ServiceResult<T> Success(T authResp)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
