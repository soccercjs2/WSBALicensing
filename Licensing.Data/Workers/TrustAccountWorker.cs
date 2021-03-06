﻿using Licensing.Data.Context;
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
            TrustAccountNumber trustAccountNumber = _context.TrustAccountNumbers.Find(trustAccountNumberId);

            if (trustAccountNumber != null)
            {
                _context.TrustAccountNumbers.Remove(trustAccountNumber);
                _context.SaveChanges();
            }
        }

        public void DeleteTrustAccount(TrustAccount trustAccount)
        {
            _context.TrustAccounts.Remove(trustAccount);
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
