using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.Domain.Models;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public bool IsError { get { return !IsSuccess; } }
    public bool IsValidationError { get { return Type == EnumResType.ValidationError; } }
    public bool IsSystemError { get { return Type == EnumResType.SystemError; } }
    private EnumResType Type { get; set; }
    public T Data { get; set; } 
    public string Message { get; set; }

    public static Result<T> Success<T>(T data, string message = "Success")
    {
        return new Result<T>
        {
            IsSuccess = true,
            Type = EnumResType.Sucess,
            Data = data,
            Message = message

        };
    }

    public static Result<T> ValidationError(string message, T? data = default)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Type = EnumResType.ValidationError,
            Data = data,
            Message = message
        };
    }

    public static Result<T> SystemError<T>(string message, T? data = default)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Type = EnumResType.SystemError,
            Data = data,
            Message = message
        };
    }
}
