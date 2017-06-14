using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using Licensing.Domain.MCLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class MCLEManager
    {
        private LicensingContext _context;
        private MCLEWorker _mcleWorker;

        public MCLEManager(LicensingContext context)
        {
            _context = context;
            _mcleWorker = new MCLEWorker(context);
        }

        public MCLETranscript GetMCLETranscript(License license)
        {
            return _mcleWorker.GetMCLETranscript(license.Customer.BarNumber, license.LicensePeriod.EndDate.Year);
        }

        public bool IsComplete(License license)
        {
            var mcleTranscript = _mcleWorker.GetMCLETranscript(license.Customer.BarNumber, license.LicensePeriod.EndDate.Year);
            bool hasUnpaidMCLELateFee = _mcleWorker.HasUnpaidMCLELateFee(license.Customer.BarNumber, license.LicensePeriod.EndDate.Year);
            bool hasUnpaidMCLEComityFee = _mcleWorker.HasUnpaidMCLEComityFee(license.Customer.BarNumber, license.LicensePeriod.EndDate.Year);

            if (hasUnpaidMCLELateFee || hasUnpaidMCLEComityFee) { return false; }

            return mcleTranscript == null || (mcleTranscript.TranscriptStatus == 6 && (mcleTranscript.CreditRequirementsFulfilled || mcleTranscript.CertifiedViaInboundComity || mcleTranscript.SubmissionType == 1));
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            var mcleTranscript = _mcleWorker.GetMCLETranscript(license.Customer.BarNumber, license.LicensePeriod.EndDate.Year);
            if (mcleTranscript == null) { mcleTranscript = new MCLETranscript(); }

            bool hasUnpaidMCLELateFee = _mcleWorker.HasUnpaidMCLELateFee(license.Customer.BarNumber, license.LicensePeriod.EndDate.Year);
            bool hasUnpaidMCLEComityFee = _mcleWorker.HasUnpaidMCLEComityFee(license.Customer.BarNumber, license.LicensePeriod.EndDate.Year);

            return new DashboardContainerVM(
                "MCLE",
                license.LicenseType.LicenseTypeRequirement.MCLE,
                IsComplete(license),
                null,
                null,
                false,
                "_MCLE",
                new McleVM() { MCLETranscript = mcleTranscript, HasUnpaidMCLELateFee = hasUnpaidMCLELateFee, HasUnpaidMCLEComityFee = hasUnpaidMCLEComityFee }
            );
        }
    }
}
