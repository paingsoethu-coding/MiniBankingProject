using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniBankingProject.Database.Models;
using MiniBankingProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.Domain.Features.User
{
    public class UserService
    {
        private readonly AppDbContext _db = new AppDbContext();

        

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
            var item = _db.TblUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == id);
            if (item is null)
            {
                return null;
            }
            
            item.FullName = user.FullName;
            item.MobileNo = user.MobileNo;
            item.Balance = user.Balance;
            item.Pin = user.Pin;
            item.UpdatedDate = DateTime.Now;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return user;
        }

        public TblUser PatchUser(int id, TblUser user)
        {
            var item = _db.TblUsers
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == id);
            if (item is null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(user.FullName))
            {
                item.FullName = user.FullName;
            }
            if (!string.IsNullOrEmpty(user.MobileNo))
            {
                item.MobileNo = user.MobileNo;
            }
            if (user.Balance > 0)
            {
                item.Balance = user.Balance;
            }
            if (!string.IsNullOrEmpty(user.Pin))
            {
                item.Pin = user.Pin;
            }
            
            item.UpdatedDate = DateTime.Now;
            
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return user;
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


        public List<TblTransaction> TransactionsHistroy()
        {
            var model = _db.TblTransactions.AsNoTracking().ToList();

            return model;
        }

        public TblTransaction TransactionHistroy(int id)
        {
            var item = _db.TblTransactions
                .AsNoTracking()
                .FirstOrDefault(x => x.TransactionId == id);
            return item;
        }

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

    }
}

