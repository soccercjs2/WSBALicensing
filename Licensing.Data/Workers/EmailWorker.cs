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
        private License _license;

        public EmailWorker(LicensingContext context, License license)
        {
            _context = context;
            _license = license;
        }

        public Email GetEmail(EmailType emailType)
        {
            if (_license.Emails == null)
            {
                return null;
            }

            if (emailType == null)
            {
                return null;
            }

            return _license.Emails.Where(e => e.EmailTypeId == emailType.EmailTypeId).FirstOrDefault();
        }
    }
}
