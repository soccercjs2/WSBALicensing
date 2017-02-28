using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Addresses
{
    public class Address : Preloadable
    {
        public int AddressId { get; set; }
        public int LicenseId { get; set; }

        public virtual AddressType AddressType { get; set; }
        public int AddressTypeId { get; set; }

        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string CongressionalDistrict { get; set; }
    }
}
