using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class PhoneNumberTypesVM
    {
        public ICollection<PhoneNumberType> Types { get; set; }
        public string Name { get; set; }
    }
}
