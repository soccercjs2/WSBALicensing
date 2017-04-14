using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.FinancialResponsibilities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class FinancialResponsibilityManager
    {
        private LicensingContext _context;
        private FinancialResponsibilityWorker _financialResponsibilityWorker;

        public FinancialResponsibilityManager(LicensingContext context)
        {
            _context = context;
            _financialResponsibilityWorker = new FinancialResponsibilityWorker(context);
        }

        public ICollection<CoveredByOption> GetOptions()
        {
            return _financialResponsibilityWorker.GetOptions();
        }

        public CoveredByOption GetOption(int id)
        {
            return _financialResponsibilityWorker.GetOption(id);
        }

        public CoveredByOption GetOption(string amsCode)
        {
            return _financialResponsibilityWorker.GetOption(amsCode);
        }

        public void SetFinancialResponsibility(License license, string company, string policyNumber, int coveredById)
        {
            CoveredByOption option = _financialResponsibilityWorker.GetOption(coveredById);

            license.FinancialResponsibility = new FinancialResponsibility();
            license.FinancialResponsibility.Company = company;
            license.FinancialResponsibility.PolicyNumber = policyNumber;
            license.FinancialResponsibility.Option = option;
            license.FinancialResponsibility.Confirmed = true;

            _context.SaveChanges();
        }

        public void Confirm(FinancialResponsibility financialResponsibility)
        {
            financialResponsibility.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return (license.FinancialResponsibility != null && license.FinancialResponsibility.Confirmed);
        }

        public IList<CoveredByOption> GetAmsOptions()
        {
            IList<CoveredByOption> options = new List<CoveredByOption>();
            var codes = WSBA.AMS.CodeTypesManager.GetCoveredByCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new CoveredByOption() { Name = code.Description, AmsCode = code.Code, Active = true });
            }

            return options;
        }

        public void SetOption(CoveredByOption option)
        {
            if (option.CoveredByOptionId == 0)
            {
                CoveredByOption existingCode = _financialResponsibilityWorker.GetOption(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _financialResponsibilityWorker.SetOption(option);
        }

        public void DeleteOption(CoveredByOption option)
        {
            _financialResponsibilityWorker.DeleteOption(option);
        }

        public IList<CoveredByOption> GetCodesToBeAdded(ICollection<CoveredByOption> codes, ICollection<CoveredByOption> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<CoveredByOption> GetCodesToBeActivated(ICollection<CoveredByOption> codes, ICollection<CoveredByOption> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<CoveredByOption> GetCodesToBeChanged(ICollection<CoveredByOption> codes, ICollection<CoveredByOption> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && c.Name != ac.Name)).ToList();
        }

        public IList<CoveredByOption> GetCodesToBeDeactivated(ICollection<CoveredByOption> codes, ICollection<CoveredByOption> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<CoveredByOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<CoveredByOption> codesToDeactivate = new List<CoveredByOption>();

            foreach (CoveredByOption option in codesToRemove)
            {
                ICollection<FinancialResponsibility> responsesWithOption = _financialResponsibilityWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<CoveredByOption> GetCodesToBeDeleted(ICollection<CoveredByOption> codes, ICollection<CoveredByOption> amsCodes)
        {
            IList<CoveredByOption> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<CoveredByOption> codesToDeleted = new List<CoveredByOption>();

            foreach (CoveredByOption option in codesToRemove)
            {
                ICollection<FinancialResponsibility> responsesWithOption = _financialResponsibilityWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("FinancialResponsibility", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Financial Responsibility",
                license.LicenseType.LicenseTypeRequirement.FinancialResponsibility,
                IsComplete(license),
                editRoute,
                null,
                false,
                "_FinancialResponsibility",
                license.FinancialResponsibility
            );
        }
    }
}
