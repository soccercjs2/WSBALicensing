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

        public EmailManager(LicensingContext context, License license)
        {
            _context = context;
            _emailWorker = new EmailWorker(context, license);
        }

        public Email GetPrimaryEmail()
        {
            //get primary email type
            EmailType primaryEmailType = _context.EmailTypes.Where(et => et.Name == "Primary").FirstOrDefault();

            //return email
            return _emailWorker.GetEmail(primaryEmailType);
        }

        public Email GetHomeEmail()
        {
            //get home email type
            EmailType homeEmailType = _context.EmailTypes.Where(et => et.Name == "Home").FirstOrDefault();

            //return email
            return _emailWorker.GetEmail(homeEmailType);
        }
    }
}
