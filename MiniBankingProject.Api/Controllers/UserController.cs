using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBankingProject.Database.Models;
using MiniBankingProject.Domain.Features.User;
using MiniBankingProject.Domain.Models;

namespace MiniBankingProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserService _service;
        //private readonly string _connectionString = "Data Source= MSI\\SQLEXPRESS2022; Initial Catalog=MiniDigitalWallet; User ID=sa; Password=sasa; TrustServerCertificate=True";

        private readonly List<TransferRequestModel> lst = new List<TransferRequestModel>();

        public UserController()
        {
            _service = new UserService();
            
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var lst = _service.GetUsers();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var item = _service.GetUser(id);
            if (item is null) { return NotFound(); }
            return Ok(item);
        }

        [HttpPost("Register")]
        public IActionResult CreateUser(TblUser user)
        {
            var item = _service.CreateUser(user);
            return Ok(item);
        }

        [HttpPut("UpdateProfile/{id}")]
        public IActionResult UpdateUser(int id, TblUser user)
        {
            var item = _service.UpdateUser(id, user);
            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, TblUser user)
        {
            var item = _service.PatchUser(id, user);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var item = _service.DeleteUser(id);

            return Ok();
        }



        // POST: api/User/Transfer
        [HttpPost("Transfer")]
        public IActionResult Transfer(TransferRequestModel transaction)
        {
            var item = _service.Transfer(transaction);
            return Ok(item);
        }

        [HttpGet("TransactionsHistory")]
        public IActionResult TransactionsHistory()
        {
            var lst = _service.TransactionsHistroy();
            return Ok(lst);
        }

        [HttpGet("TransactionHistory/{id}")]
        public IActionResult TransactionHistroy(int id)
        {
            var item = _service.TransactionHistroy(id);
            if (item is null) { return NotFound(); }
            return Ok(item);
        }


    }
}
