using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniBankingProject.Database.Models;
using MiniBankingProject.Domain.Features.Validation;
using MiniBankingProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.Domain.Features.User
{
    public class UserService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public TblUser LoginUser(string moblieno, string pin)
        {
           
            var model = _db.TblUsers
            .AsNoTracking()
            .FirstOrDefault(x => x.MobileNo == moblieno
            && x.Pin == pin && x.DeleteFlag == false);

            return model;
        }

        public List<TblUser> GetUsers()
        {
            var model = _db.TblUsers.AsNoTracking().ToList();

            return model;
        }

        public TblUser GetUser(int id)
        {
            var item = _db.TblUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == id);
            return item;
        }

        public TblUser CreateUser(TblUser user)
        {
            _db.TblUsers.Add(user);
            _db.SaveChanges();
            return user;
        }

        public TblUser UpdateUser(int id, TblUser user)
        {
            var item = _db.TblUsers.FirstOrDefault(x => x.UserId == id);
            if (item is null) {return null;}
            
            item.FullName = user.FullName;
            item.UpdatedDate = DateTime.Now;

            //item.MobileNo = user.MobileNo;
            //item.Balance = user.Balance;
            //item.Pin = user.Pin;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return user;
        }

        #region Old Code
        //public TblUser ChangePin(int id,string oldpin,string newpin)
        //{
        //    var item = _db.TblUsers
        //        .FirstOrDefault(x => x.UserId == id);
        //    if (item is null) {return null;}
        //    if (item.Pin != oldpin)
        //    {
        //        throw new Exception("Old Pin is not correct.");
        //    }
        //    item.Pin = newpin;
        //    item.UpdatedDate = DateTime.Now;

        //    //if (!string.IsNullOrEmpty(user.FullName))
        //    //{
        //    //    item.FullName = user.FullName;
        //    //}
        //    //if (!string.IsNullOrEmpty(user.MobileNo))
        //    //{
        //    //    item.MobileNo = user.MobileNo;
        //    //}
        //    //if (user.Balance > 0)
        //    //{
        //    //    item.Balance = user.Balance;
        //    //} 

        //    _db.Entry(item).State = EntityState.Modified;
        //    _db.SaveChanges();
        //    return item;    
        //}
        #endregion

        public ServiceResult<TblUser> ChangePin(int id, string oldpin, string newpin)
        {
            var item = _db.TblUsers.FirstOrDefault(x => x.UserId == id);

            if (item is null)
            {
                return new ServiceResult<TblUser>
                {
                    Success = false,
                    Message = "User not found.",
                    Data = null
                };
            }


            if (item.Pin != oldpin)
            {
                //OldPIN is incorrect
                return new ServiceResult<TblUser>
                {
                    Success = false,
                    Message = "Old PIN is incorrect.",
                    Data = null
                };
            }
            else if (item.Pin == newpin)
            {
                //OldPIN and NewPIN are the same
                return new ServiceResult<TblUser>
                {
                    Success = false,
                    Message = "Old PIN and New PIN cannot be the same.",
                    Data = null
                };
            }

            item.Pin = newpin;
            item.UpdatedDate = DateTime.Now;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return new ServiceResult<TblUser>
            {
                Success = true,
                Message = "PIN changed successfully.",
                Data = item
            };
        }


        public bool? DeleteUser(int id)
        {
            var item = _db.TblUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == id);
            if (item is null)
            {
                return null;
            }
            _db.Entry(item).State = EntityState.Deleted;
            var result = _db.SaveChanges();

            return result > 0;
        }

        // Transfer Money
        public TransferRequestModel Transfer(TransferRequestModel transaction)
        {
            var Fromuser = _db.TblUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.MobileNo == transaction.FromMobileNo);

            var Touser = _db.TblUsers
               .AsNoTracking()
               .FirstOrDefault(x => x.MobileNo == transaction.ToMobileNo);

            if (Fromuser == null || Touser == null)
            {
                throw new Exception("One or both users not found.");
            }

            Fromuser.Balance -= transaction.TransferedAmount;
            Touser.Balance += transaction.TransferedAmount;

            _db.Entry(Fromuser).State = EntityState.Modified;
            _db.Entry(Touser).State = EntityState.Modified;

            //Add User to Transaction
            _db.Entry(Fromuser).Property(x => x.CreatedDate).IsModified = false;

            TblTransaction transaction1 = new TblTransaction
            {
                FromMobileNo = transaction.FromMobileNo,
                ToMobileNo = transaction.ToMobileNo,
                TransferedAmount = transaction.TransferedAmount,
                Dates = DateTime.Now,
                Notes = transaction.Notes,
                UserId = Fromuser.UserId // Ensure this is set correctly
            };

            _db.TblTransactions.Add(transaction1);
            _db.SaveChanges();

            return transaction;
        }

        public List<TransferRequestModel> TransactionsHistroy(int id)
        {
            var model = _db.TblTransactions
                .AsNoTracking()
                .Where(x => x.UserId == id)
                .ToList();

            var result = new List<TransferRequestModel>();

            foreach (var item in model)
            {
                result.Add(new TransferRequestModel
                {
                    TransactionId = item.TransactionId,
                    FromMobileNo = item.FromMobileNo,
                    ToMobileNo = item.ToMobileNo,
                    TransferedAmount = item.TransferedAmount,
                    Dates = item.Dates,
                    Notes = item.Notes
                });
            }

            return result;
        }

        //public TblTransaction TransactionHistroy(int id)
        //{
        //    var item = _db.TblTransactions
        //        .AsNoTracking()
        //        .FirstOrDefault(x => x.TransactionId == id);
        //    return item;
        //}

    }

    // Login Request
    public class LoginRequest
    {
        [Required(ErrorMessage = "Mobile number is required.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Mobile number must be exactly 13 characters.")]
        [NumericOnly]  // Create custom validator ensures numbers only
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "PIN is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "PIN must be exactly 6 characters.")]
        [NumericOnly]  
        public string Pin { get; set; }
    }

    // Change Pin Request
    public class ChangePinRequest
    {
        [Required(ErrorMessage = "Old PIN is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Old PIN must be exactly 6 digits.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Old PIN must contain only numbers.")]
        public string OldPin { get; set; }

        [Required(ErrorMessage = "New PIN is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "New PIN must be exactly 6 digits.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "New PIN must contain only numbers.")]
        public string NewPin { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (OldPin == NewPin)
        //    {
        //        yield return new ValidationResult("Old PIN and New PIN cannot be the same.", new[] { nameof(NewPin) });
        //    }
        //}
    }

    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }


}

