using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class TrustAccountManager
    {
        private LicensingContext _context;
        private TrustAccountWorker _trustAccountWorker;

        public TrustAccountManager(LicensingContext context)
        {
            _context = context;
            _trustAccountWorker = new TrustAccountWorker(context);
        }

        public TrustAccount GetTrustAccount(int id)
        {
            return _trustAccountWorker.GetTrustAccount(id);
        }

        public TrustAccount GetTrustAccountByTrustAccountNumber(int id)
        {
            TrustAccountNumber trustAccountNumber = _trustAccountWorker.GetTrustAccountNumber(id);
            return _trustAccountWorker.GetTrustAccount(trustAccountNumber.TrustAccountId);
        }

        public TrustAccountNumber GetTrustAccountNumber(int id)
        {
            return _trustAccountWorker.GetTrustAccountNumber(id);
        }

        public TrustAccountNumber GetTrustAccountNumber(License license, int sequenceNumber)
        {
            if (license.TrustAccount == null) { return null; }
            if (license.TrustAccount.TrustAccountNumbers == null) { return null; }

            return license.TrustAccount.TrustAccountNumbers.Where(t => t.AmsSequenceNumber == sequenceNumber).FirstOrDefault();
        }

        public void SetTrustAccount(License license, bool handlesTrustAccount)
        {
            SetTrustAccount(license, handlesTrustAccount, 0);
        }

        public void SetTrustAccount(License license, bool handlesTrustAccount, int amsSequenceNumber)
        {
            if (license.TrustAccount == null) { license.TrustAccount = new TrustAccount(); }

            if (amsSequenceNumber > 0) { license.TrustAccount.AmsSequenceNumber = amsSequenceNumber; }

            license.TrustAccount.HandlesTrustAccount = handlesTrustAccount;

            _context.SaveChanges();
        }

        public void SetHandlesTrustAccount(License license)
        {
            if (license.TrustAccount != null)
            {
                license.TrustAccount.HandlesTrustAccount = true;
            }
            else
            {
                license.TrustAccount = new TrustAccount();
                license.TrustAccount.HandlesTrustAccount = true;
            }

            if (license.TrustAccount.TrustAccountNumbers != null && license.TrustAccount.TrustAccountNumbers.Count > 0)
            {
                license.TrustAccount.Confirmed = true;
            }

            _context.SaveChanges();
        }

        public void SetDoesNotHandleTrustAccount(License license)
        {
            if (license.TrustAccount != null)
            {
                license.TrustAccount.HandlesTrustAccount = false;
            }
            else
            {
                license.TrustAccount = new TrustAccount();
                license.TrustAccount.HandlesTrustAccount = false;
            }

            license.TrustAccount.Confirmed = true;

            _context.SaveChanges();
        }

        public void AddTrustAccountNumber(License license, TrustAccountNumber trustAccountNumber)
        {
            //add trust account number
            license.TrustAccount.TrustAccountNumbers.Add(trustAccountNumber);

            //save changes
            _context.SaveChanges();
        }

        public void AddTrustAccountNumber(License license, string bank, string branch, string accountNumber)
        {
            TrustAccountNumber trustAccountNumber = new TrustAccountNumber();
            trustAccountNumber.Bank = bank;
            trustAccountNumber.Branch = branch;
            trustAccountNumber.AccountNumber = accountNumber;

            if (license.TrustAccount.TrustAccountNumbers == null)
            {
                license.TrustAccount.TrustAccountNumbers = new List<TrustAccountNumber>();
            }

            license.TrustAccount.TrustAccountNumbers.Add(trustAccountNumber);
        }

        public void DeleteTrustAccount(License license)
        {
            _trustAccountWorker.DeleteTrustAccount(license.TrustAccount);
        }

        public void DeleteTrustAccountNumber(int trustAccountNumberId)
        {
            _trustAccountWorker.DeleteTrustAccountNumber(trustAccountNumberId);
        }

        public void SetTrustAccountNumber(TrustAccountNumber trustAccountNumber)
        {
            _trustAccountWorker.SetTrustAccountNumber(trustAccountNumber);
        }

        public void Confirm(License license)
        {
            license.TrustAccount.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            if (license.TrustAccount == null || !license.TrustAccount.Confirmed) { return false; }
            if (license.TrustAccount.HandlesTrustAccount && (license.TrustAccount.TrustAccountNumbers == null || license.TrustAccount.TrustAccountNumbers.Count == 0)) { return false; }

            return true;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("TrustAccount", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Trust Account",
                license.LicenseType.LicenseTypeRequirement.TrustAccount,
                IsComplete(license),
                editRoute,
                null,
                false,
                "_TrustAccount",
                license.TrustAccount
            );
        }
    }
}
