using Licensing.Data.Context;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class EthnicityWorker
    {
        private LicensingContext _context;

        public EthnicityWorker(LicensingContext context)
        {
            _context = context;
        }

        public void OptOut(License license)
        {
            license.EthnicityOptedOut = true;
            _context.SaveChanges();
        }
    }
}
