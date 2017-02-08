using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class SexualOrientationWorker
    {
        private LicensingContext _context;

        public SexualOrientationWorker(LicensingContext context)
        {
            _context = context;
        }
    }
}
