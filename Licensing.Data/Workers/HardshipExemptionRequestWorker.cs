using Licensing.Data.Context;
using Licensing.Domain.BarNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class HardshipExemptionRequestWorker
    {
        private LicensingContext _context;

        public HardshipExemptionRequestWorker(LicensingContext context)
        {
            _context = context;
        }
    }
}
