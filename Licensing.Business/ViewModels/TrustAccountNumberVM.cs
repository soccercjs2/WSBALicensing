using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class TrustAccountNumberVM
    {
        public int TrustAccountNumberId { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        [Display(Name = "IOLTA Account Number")]
        public string AccountNumber { get; set; }

        public TrustAccountNumberVM() { }

        public TrustAccountNumberVM(int id, string bank, string branch, string accountNumber)
        {
            TrustAccountNumberId = id;
            Bank = bank;
            Branch = branch;
            AccountNumber = accountNumber;
        }
    }
}
