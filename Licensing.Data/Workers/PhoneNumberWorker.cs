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
        private License _license;

        public PhoneNumberWorker(LicensingContext context, License license)
        {
            _context = context;
            _license = license;
        }

        public PhoneNumber GetPhoneNumber(PhoneNumberType phoneNumberType)
        {
            if (_license.PhoneNumbers == null)
            {
                return null;
            }

            if (phoneNumberType == null)
            {
                return null;
            }

            return _license.PhoneNumbers.Where(pn => pn.PhoneNumberTypeId == phoneNumberType.PhoneNumberTypeId).FirstOrDefault();
        }
    }
}
