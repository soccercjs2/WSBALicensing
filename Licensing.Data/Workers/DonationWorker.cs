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

        public DonationWorker(LicensingContext context)
        {
            _context = context;
        }
    }
}
