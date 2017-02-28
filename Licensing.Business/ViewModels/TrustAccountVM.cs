using Licensing.Domain.Licenses;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class TrustAccountVM
    {
        public int LicenseId { get; set; }
        public int? TrustAccountId { get; set; }
        public bool HandlesTrustAccount { get; set; }
        public bool Attested { get; set; }

        public TrustAccountNumber PendingTrustAccountNumber { get; set; }

        public ICollection<TrustAccountNumber> TrustAccountNumbers { get; set; }

        public TrustAccountVM() { }

        public TrustAccountVM(License license)
        {
            LicenseId = license.LicenseId;

            if (license.TrustAccount != null)
            {
                TrustAccountId = license.TrustAccount.TrustAccountId;
                HandlesTrustAccount = license.TrustAccount.HandlesTrustAccount;
                TrustAccountNumbers = license.TrustAccount.TrustAccountNumbers;

                TrustAccountNumber pendingTrustAccountNumber = new TrustAccountNumber();
                pendingTrustAccountNumber.TrustAccountId = license.TrustAccount.TrustAccountId;

                PendingTrustAccountNumber = pendingTrustAccountNumber;
            }
        }
    }
}
