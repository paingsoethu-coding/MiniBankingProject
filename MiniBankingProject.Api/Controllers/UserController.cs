using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBankingProject.Database.Models;
using MiniBankingProject.Domain.Features.User;
using MiniBankingProject.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace MiniBankingProject.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    public readonly UserService _service;

    public UserController()
    {
        _service = new UserService();
    }


    [HttpPost("Login")]
    public IActionResult LoginUser([FromBody] LoginRequest request)
    {
        // Validate mobile number and pin
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lst = _service.LoginUser(request.MobileNo, request.Pin);

        if (lst is null)
        {
            return NotFound("You don`t have account.Please create an account.");
        }
        return Ok(lst);
    }

    [HttpPost("Register")]
    public IActionResult CreateUser(TblUser user)
    {
        var item = _service.CreateUser(user);
        return Ok(item);
    }



    [HttpGet("UserDetail/{id}")]
    public IActionResult GetUser(int id)
    {
        var item = _service.GetUser(id);
        if (item is null) { return NotFound(); }
        return Ok(item);
    }

    [HttpPut("UserProfileUpdate/{id}")]
    public IActionResult UpdateUser(int id, TblUser user)
    {
        var item = _service.UpdateUser(id, user);
        return Ok(item);
    }



    [HttpPatch("ChangePin/{id}")]
    public IActionResult ChangePin(int id, [FromBody] ChangePinRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _service.ChangePin(id, request.OldPin, request.NewPin);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var lst = _service.GetUsers();
        return Ok(lst);
    }

    #region old code
    //[HttpPut("UpdateProfile/{id}")]

    //[HttpPatch("ChangePin/{id}")]
    //public IActionResult ChangePin(int id, [FromBody] ChangePinRequest request)
    //{
    //    if (request == null || string.IsNullOrWhiteSpace(request.OldPin) || 
    //        string.IsNullOrWhiteSpace(request.NewPin))
    //    {
    //        return BadRequest("Both old and new PINs are required.");
    //    }

    //    // Ensure PINs are exactly 6 digits and numeric
    //    if (request.OldPin.Length != 6 || !request.OldPin.All(char.IsDigit) ||
    //        request.NewPin.Length != 6 || !request.NewPin.All(char.IsDigit))
    //    {
    //        return BadRequest("Both PINs must be exactly 6 digits and contain only numbers.");
    //    }

    //    var result = _service.ChangePin(id, request.OldPin, request.NewPin);

    //    if (result == null)
    //    {
    //        return NotFound("User not found or old PIN is incorrect.");
    //    }

    //    return Ok("PIN changed successfully.");
    //}

    //public IActionResult UpdateUsers(int id, TblUser user)
    //{
    //    var item = _service.UpdateUser(id, user);
    //    return Ok(item);
    //}

    //[HttpPatch("{id}")]
    //public IActionResult PatchUser(int id, TblUser user)
    //{
    //    var item = _service.PatchUser(id, user);
    //    return Ok(item);
    //}
    #endregion

    //POST: api/User/Transfer
    [HttpPost("Transfer")]
    public IActionResult Transfer(TransferRequestModel transaction)
    {
        var item = _service.Transfer(transaction);
        return Ok(item);
    }

    //SSLH
    //[HttpPost("TransferAsync")]
    //public async Task<IActionResult> TransferAsync([FromBody] TransferRequestModel transaction)
    //{
    //    try
    //    {
    //        var model = await _service.TransferAsync
    //            (transaction.TransactionType, transaction.FromMobileNo,
    //                transaction.ToMobileNo, transaction.TransferedAmount, transaction.Notes);

    //        //if (model.Response.IsSuccess)
    //        //    if (model.Response.RespType == EnumResType.ValidationError)
    //        //        return BadRequest(model);
    //        //if (model.Response.RespType == EnumResType.SystemError)
    //        //    return StatusCode(500, model);
    //        //return Ok(model);

    //        return Execute(model);
    //    }
    //    catch (ArgumentException ex) // should use ArgumentException 
    //    {

    //        return BadRequest(new { message = ex.Message });
    //    }
    //}



    [HttpGet("TransactionsHistory/{id}")]
    public IActionResult TransactionsHistory(int id)
    {
        var lst = _service.TransactionsHistroy(id);
        if (lst is null) { return NotFound(); }
        return Ok(lst);
    }


    [HttpPatch("Deposit/{id}")]
    public IActionResult Deposit(int id, decimal amount)
    {
        var item = _service.Deposit(id, amount);
        return Ok(item);
    }

    [HttpPatch("Withdraw/{id}")]
    public IActionResult Withdraw(int id, decimal amount)
    {
        var item = _service.Withdraw(id, amount);
        return Ok(item);
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var item = _service.DeleteUser(id);
        return Ok();
    }

}
