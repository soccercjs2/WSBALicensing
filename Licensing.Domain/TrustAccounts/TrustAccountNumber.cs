using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.TrustAccounts
{
    public class TrustAccountNumber
    {
        public int TrustAccountNumberId { get; set; }
        public int TrustAccountId { get; set; }

        public string Bank { get; set; }
        public string Branch { get; set; }

        [Display(Name = "IOLTA Account Number")]
        public string AccountNumber { get; set; }
        public int AmsSequenceNumber { get; set; }
    }
}
