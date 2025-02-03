using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.Domain.Models
{
    public class TransactionRequestModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string MobileNo { get; set; } = null!;

        public decimal Balance { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
