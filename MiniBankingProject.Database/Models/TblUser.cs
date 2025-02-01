using System;
using System.Collections.Generic;

namespace MiniBankingProject.Database.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public string Pin { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool DeleteFlag { get; set; }

    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
