using Licensing.Data.Context;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class TrustAccountWorker
    {
        private LicensingContext _context;

        public TrustAccountWorker(LicensingContext context)
        {
            _context = context;
        }

        public TrustAccount GetTrustAccount(int id)
        {
            return _context.TrustAccounts.Find(id);
        }
    }
}
