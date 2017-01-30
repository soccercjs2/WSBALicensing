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

        public Email GetEmail(License license, EmailType emailType)
        {
            if (license == null)
            {
                return null;
            }

            if (license.Emails == null)
            {
                return null;
            }

            if (emailType == null)
            {
                return null;
            }

            return license.Emails.Where(e => e.EmailTypeId == emailType.EmailTypeId).FirstOrDefault();
        }
    }
}
