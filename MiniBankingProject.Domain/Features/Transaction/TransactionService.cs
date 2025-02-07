using Microsoft.EntityFrameworkCore;
using MiniBankingProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.Domain.Features.Transaction
{
    public class TransactionService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public List<TblTransaction> GetTransactions()
        {
            var model = _db.TblTransactions.AsNoTracking().ToList();

            return model;
        }

        public TblTransaction? GetTransaction(int id)
        {
            var item = _db.TblTransactions
                .AsNoTracking()
                .FirstOrDefault(x => x.TransactionId == id);
            return item;
        }

        public TblTransaction CreateTransaction(TblTransaction transaction)
        {
            _db.TblTransactions.Add(transaction);
            _db.SaveChanges();
            return transaction;
        }

        public TblTransaction? UpdateTransaction(int id, TblTransaction transaction)
        {
            var item = _db.TblTransactions
                .AsNoTracking()
                .FirstOrDefault(x => x.TransactionId == id);
            if (item is null)
            {
                return null;
            }

            item.TransactionNo = transaction.TransactionNo;
            item.TransactionType = transaction.TransactionType;
            item.FromMobileNo = transaction.FromMobileNo;
            item.ToMobileNo = transaction.ToMobileNo;
            item.TransferedAmount = transaction.TransferedAmount;
            item.Dates = DateTime.Now;
            item.Notes = transaction.Notes;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return transaction;
        }

        public TblTransaction? PatchTransaction(int id, TblTransaction transaction)
        {
            var item = _db.TblTransactions
                .AsNoTracking()
                .FirstOrDefault(x => x.TransactionId == id);

            if (item is null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(transaction.TransactionNo.ToString()))
            {
                item.TransactionNo = transaction.TransactionNo;
            }
            if (!string.IsNullOrEmpty(transaction.TransactionType))
            {
                item.TransactionType = transaction.TransactionType;
            }
            if (!string.IsNullOrEmpty(transaction.FromMobileNo))
            {
                item.FromMobileNo = transaction.FromMobileNo;
            }
            if (!string.IsNullOrEmpty(transaction.ToMobileNo))
            {
                item.ToMobileNo = transaction.ToMobileNo;
            }
            if (transaction.TransferedAmount > 0)
            {
                item.TransferedAmount = transaction.TransferedAmount;
            }
            if (!string.IsNullOrEmpty(transaction.Notes))
            {
                item.Notes = transaction.Notes;
            }

            item.Dates = DateTime.Now;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return transaction;
        }

        public bool? DeleteTransaction(int id)
        {
            var item = _db.TblTransactions
                .AsNoTracking()
                .FirstOrDefault(x => x.TransactionId == id);
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
