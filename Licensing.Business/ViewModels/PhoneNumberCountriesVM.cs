using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class PhoneNumberCountriesVM
    {
        public IList<PhoneNumberCountry> Codes { get; set; }
        public IList<PhoneNumberCountry> CodesToBeAdded { get; set; }
        public IList<PhoneNumberCountry> CodesToBeActivated { get; set; }
        public IList<PhoneNumberCountry> CodesToBeChanged { get; set; }
        public IList<PhoneNumberCountry> CodesToBeDeactivated { get; set; }
        public IList<PhoneNumberCountry> CodesToBeDeleted { get; set; }
    }
}
