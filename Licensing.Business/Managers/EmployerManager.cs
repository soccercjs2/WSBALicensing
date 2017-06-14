using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Employers;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class EmployerManager
    {
        private LicensingContext _context;
        private EmployerWorker _employerWorker;

        public EmployerManager(LicensingContext context)
        {
            _context = context;
            _employerWorker = new EmployerWorker(context);
        }

        public void SetEmployer(License license, int masterCustomerId, string name)
        {
            if (license.Employer == null)
            {
                Employer employer = _employerWorker.GetEmployer(masterCustomerId);

                if (employer == null)
                {
                    employer = new Employer();
                    employer.AmsMasterCustomerId = masterCustomerId;
                }

                employer.Name = name;

                license.Employer = employer;
            }
            else
            {
                license.Employer.Name = name;
            }

            _context.SaveChanges();
        }

        public void DeleteEmployer(License license)
        {
            license.Employer = null;
            _context.SaveChanges();
        }

        public DashboardContainerVM GetDashboardContainerVM(License license, decimal licensingBalance)
        {
            RouteContainer editRoute = new RouteContainer("BulkPayment", "Edit", license.LicenseId);

            if (license.Employer != null)
            {
                return new DashboardContainerVM(
                    "Bulk Payment",
                    RequirementType.Optional,
                    licensingBalance == 0,
                    editRoute,
                    null,
                    true,
                    "_BulkPayment",
                    new EmployerVM() { Employer = license.Employer, LateFeeDate = license.LicensePeriod.LateFeeDate, LicensingBalance = licensingBalance }
                );
            }
            else
            {
                return null;
            }
        }
    }
}
