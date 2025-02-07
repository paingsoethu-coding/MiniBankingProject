using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBankingProject.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiniBankingProject.Api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        //[NonAction] ဘာလို့ဆိုရင် ဒီကောင်မှာက public ပေးလို့မရဘူး protected or nonaction တစ်ခုခုပေးမှကိုရမယ်
        protected IActionResult Execute(object model)
        {
            JObject jobj = JObject.Parse(JsonConvert.SerializeObject(model));
            if (jobj.ContainsKey("Response"))
            {
                BaseResponseModel baseResponseModel =
                    JsonConvert.DeserializeObject<BaseResponseModel>(
                        jobj["Response"]!.ToString()!)!;

                if (baseResponseModel.RespType == EnumResType.Pending)
                    return StatusCode(201,model);

                if (baseResponseModel.RespType == EnumResType.ValidationError)
                    return BadRequest(model);
                if (baseResponseModel.RespType == EnumResType.SystemError)
                    return StatusCode(500, model);

                return Ok(model);
            }

            return StatusCode(500, "Invalid Response Model. " +
                "Please add BaseResponseModel to your ResponseModel.");
        }

        protected IActionResult Execute<T>(Result<T> model)
        {
            if (model.IsValidationError)
                return BadRequest(model);

            if (model.IsSystemError)
                return StatusCode(500, model);

            return Ok(model);
        
        }
    }
}
