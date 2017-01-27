using Licensing.Data.Context;
using Licensing.Domain.Donations;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class DonationWorker
    {
        private LicensingContext _context;
        private License _license;

        public DonationWorker(LicensingContext context, License license)
        {
            _context = context;
            _license = license;
        }

        public ICollection<Donation> GetDonations()
        {
            ICollection<Donation> donations = _license.Donations;

            if (donations.Count == 0) { return null; }
            else { return donations; }
        }
    }
}
