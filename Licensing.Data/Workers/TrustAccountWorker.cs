using Licensing.Data.Context;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public TrustAccountNumber GetTrustAccountNumber(int id)
        {
            return _context.TrustAccountNumbers.Find(id);
        }

        public void DeleteTrustAccountNumber(int trustAccountNumberId)
        {
            _context.TrustAccountNumbers.Remove(_context.TrustAccountNumbers.Find(trustAccountNumberId));
            _context.SaveChanges();
        }

        public void SetTrustAccountNumber(TrustAccountNumber trustAccountNumber)
        {
            _context.Entry(trustAccountNumber).State = trustAccountNumber.TrustAccountNumberId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
