﻿using MiniBankingProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.Domain.Models
{
    public class TransferRequestModel
    {
        public string? TransactionId { get; set; }

        public int UserId { get; set; }

        public int TransactionNo { get; set; }

        public string TransactionType { get; set; } = null!;

        public string FromMobileNo { get; set; } = null!;

        public string ToMobileNo { get; set; } = null!;

        public decimal TransferedAmount { get; set; } = 0!;

        public DateTime Dates { get; set; }

        public string Notes { get; set; } = null!;

        public bool DeleteFlag { get; set; }

        //public virtual TblUser User { get; set; } = null!;

    }
}
