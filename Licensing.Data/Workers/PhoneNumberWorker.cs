using Licensing.Data.Context;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class PhoneNumberWorker
    {
        private LicensingContext _context;

        public PhoneNumberWorker(LicensingContext context)
        {
            _context = context;
        }

        public PhoneNumber GetPhoneNumber(int phoneNumberId)
        {
            return _context.PhoneNumbers.Find(phoneNumberId);
        }
    }
}
