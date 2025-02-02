using MiniBankingProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankingProject.Domain.Models
{
    public class TransferRequestModel
    {
        public string FromMobileNo { get; set; } = null!;

        public string ToMobileNo { get; set; } = null!;

        public decimal TransferedAmount { get; set; }

        public DateTime Dates { get; set; }

        public string Notes { get; set; } = null!;
        
    }
}
