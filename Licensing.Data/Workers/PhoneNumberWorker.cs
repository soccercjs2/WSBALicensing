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

        public PhoneNumber GetPhoneNumber(License license, PhoneNumberType phoneNumberType)
        {
            if (license == null)
            {
                return null;
            }

            if (license.PhoneNumbers == null)
            {
                return null;
            }

            if (phoneNumberType == null)
            {
                return null;
            }

            return license.PhoneNumbers.Where(pn => pn.PhoneNumberTypeId == phoneNumberType.PhoneNumberTypeId).FirstOrDefault();
        }
    }
}
