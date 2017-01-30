using Licensing.Data.Context;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class AreaOfPracticeWorker
    {
        private LicensingContext _context;

        public AreaOfPracticeWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<AreaOfPractice> GetAreasOfPractice(License license)
        {
            if (license == null)
            {
                return null;
            }

            if (license.AreasOfPractice == null)
            {
                return null;
            }

            ICollection<AreaOfPractice> areasOfPractice = license.AreasOfPractice;

            if (areasOfPractice.Count == 0) { return null; }
            else { return areasOfPractice; }
        }
    }
}
