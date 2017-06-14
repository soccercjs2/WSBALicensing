using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Licensing.Business.ViewModels
{
    public class PhoneNumberVM
    {
        public PhoneNumber PhoneNumber { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        public PhoneNumberVM() { }
        public PhoneNumberVM(PhoneNumber phoneNumber, ICollection<PhoneNumberCountry> countries)
        {
            PhoneNumber = phoneNumber;

            Countries = countries.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.PhoneNumberCountryId.ToString(),
                                      Text = x.Name + " (+" + x.InternationalCode + ")",
                                      Selected = (phoneNumber.PhoneNumberId == 0 && x.CountryCode == "USA")
                                  });
        }
    }
}
