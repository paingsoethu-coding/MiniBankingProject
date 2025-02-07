using System;
using System.Collections.Generic;

namespace MiniBankingProject.Database2.Models;

public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public int TransactionNo { get; set; }

    public string TransactionType { get; set; } = null!;

    public string FromMobileNo { get; set; } = null!;

    public string ToMobileNo { get; set; } = null!;

    public decimal TransferedAmount { get; set; }

    public DateTime Dates { get; set; }

    public string Notes { get; set; } = null!;

    public bool DeleteFlag { get; set; }

    public virtual TblUser User { get; set; } = null!;
}
