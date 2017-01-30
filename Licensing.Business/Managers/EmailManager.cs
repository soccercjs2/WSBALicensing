using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class EmailManager
    {
        private LicensingContext _context;
        private EmailWorker _emailWorker;

        public EmailManager(LicensingContext context)
        {
            _context = context;
            _emailWorker = new EmailWorker(context);
        }

        public Email GetPrimaryEmail(License license)
        {
            //get primary email type
            EmailType primaryEmailType = _context.EmailTypes.Where(et => et.Name == "Primary").FirstOrDefault();

            //return email
            return _emailWorker.GetEmail(license, primaryEmailType);
        }

        public Email GetHomeEmail(License license)
        {
            //get home email type
            EmailType homeEmailType = _context.EmailTypes.Where(et => et.Name == "Home").FirstOrDefault();

            //return email
            return _emailWorker.GetEmail(license, homeEmailType);
        }
    }
}
