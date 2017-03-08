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
        public string HandlesCssClass { get; set; }
        public string NotHandlesCssClass { get; set; }

        public TrustAccountNumberVM PendingTrustAccountNumber { get; set; }

        public List<TrustAccountNumberVM> TrustAccountNumbers { get; set; }
        public List<TrustAccountNumberVM> TrustAccountNumbersToRemove { get; set; }

        public TrustAccountVM() { }

        public TrustAccountVM(License license)
        {
            LicenseId = license.LicenseId;
            TrustAccountNumbers = new List<TrustAccountNumberVM>();

            if (license.TrustAccount != null)
            {
                TrustAccountId = license.TrustAccount.TrustAccountId;
                HandlesTrustAccount = license.TrustAccount.HandlesTrustAccount;

                foreach (TrustAccountNumber trustAccountNumber in license.TrustAccount.TrustAccountNumbers)
                {
                    TrustAccountNumbers.Add(
                        new TrustAccountNumberVM(
                            trustAccountNumber.TrustAccountNumberId, 
                            trustAccountNumber.Bank, 
                            trustAccountNumber.Branch, 
                            trustAccountNumber.AccountNumber));
                }

                PendingTrustAccountNumber = new TrustAccountNumberVM();

                if (HandlesTrustAccount)
                {
                    HandlesCssClass = "btn-success";
                    NotHandlesCssClass = "btn-default";
                }
                else
                {
                    HandlesCssClass = "btn-default";
                    NotHandlesCssClass = "btn-danger";
                }
            }
            else
            {
                HandlesCssClass = "btn-success";
                NotHandlesCssClass = "btn-danger";
            }
        }
    }
}
