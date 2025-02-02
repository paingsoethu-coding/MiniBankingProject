using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniBankingProject.Database.Models;
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

    }
}
