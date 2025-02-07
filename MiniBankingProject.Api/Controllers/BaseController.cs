using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBankingProject.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiniBankingProject.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    public IActionResult Execute(object model) 
    {
        JObject jobj = JObject.Parse(JsonConvert.SerializeObject(model));
        if (jobj.ContainsKey("Response"))
        {
            BaseResponseModel baseResponseModel =
                JsonConvert.DeserializeObject<BaseResponseModel>(
                    jobj["Response"]!.ToString()!)!;

            if (baseResponseModel.RespType == EnumResType.ValidationError)
                return BadRequest(model);
            if (baseResponseModel.RespType == EnumResType.SystemError)
                return StatusCode(500, model);

            return Ok(model);
        }

        return StatusCode(500, "Invalid Response Model. " +
            "Please add BaseResponseModel to your ResponseModel.");
    }
}
