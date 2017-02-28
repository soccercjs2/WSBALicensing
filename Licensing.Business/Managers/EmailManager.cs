using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class EmailManager
    {
        private LicensingContext _context;
        private EmailWorker _emailWorker;

        public EmailManager(LicensingContext context)
        {
            _context = context;
            _emailWorker = new EmailWorker(context);
        }

        public void Confirm(Email email)
        {
            email.Confirmed = true;
            _context.SaveChanges();
        }

        public void SetEmail(License license, Email email)
        {
            license.Email = email;
            license.Email.Confirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return (license.Email != null && license.Email.Confirmed);
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Email", "Edit", license.LicenseId);
            RouteContainer confirmRoute = new RouteContainer("Email", "Confirm", license.LicenseId);

            return new DashboardContainerVM(
                "Primary Email",
                license.LicenseType.PrimaryEmail,
                IsComplete(license),
                editRoute,
                confirmRoute,
                null,
                false,
                "_PrimaryEmail",
                license.Email
            );
        }
    }
}
