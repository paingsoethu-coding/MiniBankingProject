using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.Domain.Models;

public class BaseResponseModel
{
    public required string RespCode { get; set; }
    
    public required string RespDesp { get; set; }

    public EnumResType RespType { get; set; }

    public bool IsSuccess { get; set; }

    public bool IsError { get { return !IsSuccess; } }

    public static BaseResponseModel Success(string respCode, string respDesp)
    {
        return new BaseResponseModel
        {
            IsSuccess = true,
            RespCode = respCode,
            RespDesp = respDesp,
            RespType = EnumResType.Sucess
        };
    }

    public static BaseResponseModel ValidationError(string respCode, string respDesp)
    {
        return new BaseResponseModel
        {
            IsSuccess = false,
            RespCode = respCode,
            RespDesp = respDesp,
            RespType = EnumResType.ValidationError
        };
    }

    public static BaseResponseModel SystemError(string respCode, string respDesp)
    {
        return new BaseResponseModel
        {
            IsSuccess = false,
            RespCode = respCode,
            RespDesp = respDesp,
            RespType = EnumResType.SystemError
        };
    }
}

public enum EnumResType
{
    None,
    Sucess,
    Pending,
    ValidationError,
    SystemError
}

