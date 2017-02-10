﻿using Licensing.Business.Tools;
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

        public void SetFinancialResponsibility(License license, string company, string policyNumber, int coveredById)
        {
            CoveredByOption option = _financialResponsibilityWorker.GetOption(coveredById);

            license.FinancialResponsibility = new FinancialResponsibility();
            license.FinancialResponsibility.Company = company;
            license.FinancialResponsibility.PolicyNumber = policyNumber;
            license.FinancialResponsibility.CoveredByOption = option;
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

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("FinancialResponsibility", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("FinancialResponsibility", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Financial Responsibility",
                license.LicenseType.FinancialResponsibility,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                "_FinancialResponsibility",
                license.FinancialResponsibility
            );
        }
    }
}
