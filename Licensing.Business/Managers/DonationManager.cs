using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Donations;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class DonationManager
    {
        private LicensingContext _context;
        private DonationWorker _donationWorker;

        public DonationManager(LicensingContext context, License license)
        {
            _context = context;
            _donationWorker = new DonationWorker(context, license);
        }

        public ICollection<Donation> GetDonations()
        {
            return _donationWorker.GetDonations();
        }
    }
}
