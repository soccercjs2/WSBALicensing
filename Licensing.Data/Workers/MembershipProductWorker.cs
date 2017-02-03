using Licensing.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class MembershipProductWorker
    {
        private LicensingContext _context;

        public MembershipProductWorker(LicensingContext context)
        {
            _context = context;
        }
    }
}
