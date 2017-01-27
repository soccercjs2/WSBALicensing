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
        private License _license;

        public AreaOfPracticeWorker(LicensingContext context, License license)
        {
            _context = context;
            _license = license;
        }

        public ICollection<AreaOfPractice> GetAreasOfPractice()
        {
            ICollection<AreaOfPractice> areasOfPractice = _license.AreasOfPractice;

            if (areasOfPractice.Count == 0) { return null; }
            else { return areasOfPractice; }
        }
    }
}
