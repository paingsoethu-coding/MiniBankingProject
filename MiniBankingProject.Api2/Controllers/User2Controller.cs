using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBankingProject.Domain.Features.Transaction;
using MiniBankingProject.Domain.Features.User;
using MiniBankingProject.Domain.Models;

namespace MiniBankingProject.Api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User2Controller : BaseController
    {
        public readonly UserService _service;
        //public readonly TransactionService _serviceTransaction;

        public User2Controller()
        {
            _service = new UserService();
            //_serviceTransaction = new TransactionService();

        }

        //[HttpPost("Login")]
        //public IActionResult LoginUser([FromBody] LoginRequest request)
        //{
        //    // Validate mobile number and pin
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var lst = _service.LoginUser(request.MobileNo, request.Pin);

        //    if (lst is null)
        //    {
        //        return NotFound("You don`t have account.Please create an account.");
        //    }
        //    return Ok(lst);
        //}

        // can`t open 2 of them why??
        //[HttpGet]
        //public IActionResult GetUsers()
        //{
        //    var lst = _service.GetUsers();
        //    return Ok(lst);
        //}

        //[HttpGet]
        //public IActionResult GetTransaction()
        //{
        //    var lst = _serviceTransaction.GetTransactions();
        //    return Ok(lst);
        //}

        //SSLH
        //[HttpPost("TransferAsync")]
        //public async Task<IActionResult> TransferAsync([FromBody] TransferRequestModel transaction)
        //{
        //    try
        //    {
        //        //var model = await _service.TransferAsync
        //        //    (transaction.FromMobileNo, transaction.ToMobileNo,
        //        //    transaction.TransferedAmount, transaction.Notes);
        //        var model2 = await _service.TransferAsync2
        //            (transaction.FromMobileNo, transaction.ToMobileNo,
        //            transaction.TransferedAmount, transaction.Notes);

        //        //await Task.WhenAll(model, model2); // await အကုန်ကိုဖြုတ်လိုက်ရင် နစ်ခုလုံးကို parallel run ပေးနေလိမ့်မယ်


        //        //return Ok(model);
        //        //return Execute(model);
        //        return Execute(model2);
        //    }
        //    catch (ArgumentException ex) // should use ArgumentException 
        //    {

        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        [HttpPost("TransferAsync")]
        public IActionResult TransferAsync([FromBody] TransferRequestModel transaction)
        {
            try
            {
                var model2 = _service.TransferAsync2(transaction.TransactionType,
                    transaction.FromMobileNo,transaction.ToMobileNo,transaction.TransferedAmount, transaction.Notes);

                //await Task.WhenAll(model, model2); // await အကုန်ကိုဖြုတ်လိုက်ရင် နစ်ခုလုံးကို parallel run ပေးနေလိမ့်မယ်


                //return Ok(model);
                //return Execute(model);
                return Execute(model2);
            }
            catch (ArgumentException ex) // should use ArgumentException 
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
