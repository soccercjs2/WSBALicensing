using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.ContactInformation;

namespace Licensing.Data.Workers
{
    public class EmailWorker
    {
        private LicensingContext _context;

        public EmailWorker(LicensingContext context)
        {
            _context = context;
        }

        public void Confirm(Email email)
        {
            if (email != null)
            {
                email.Confirmed = true;
                _context.SaveChanges();
            }
        }
    }
}
