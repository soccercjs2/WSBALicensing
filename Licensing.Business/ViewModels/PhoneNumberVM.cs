using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class PhoneNumberVM
    {
        public PhoneNumber PhoneNumber { get; set; }

        public PhoneNumberVM() { }
        public PhoneNumberVM(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
