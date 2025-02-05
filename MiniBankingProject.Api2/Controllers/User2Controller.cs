using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBankingProject.Domain.Features.Transaction;
using MiniBankingProject.Domain.Features.User;

namespace MiniBankingProject.Api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User2Controller : ControllerBase
    {
        public readonly UserService _service;
        public readonly TransactionService _serviceTransaction;

        public User2Controller()
        {
            _service = new UserService();
            _serviceTransaction = new TransactionService();
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

        // can`t open 2 of them why??
        //[HttpGet]
        //public IActionResult GetUsers()
        //{
        //    var lst = _service.GetUsers();
        //    return Ok(lst);
        //}

        [HttpGet]
        public IActionResult GetTransaction()
        {
            var lst = _serviceTransaction.GetTransactions();
            return Ok(lst);
        }
    }
}
