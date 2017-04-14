using Licensing.Domain.Donations;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicenseTypeDonation
    {
        public int LicenseTypeDonationId { get; set; }
        public int LicenseTypeId { get; set; }

        [ForeignKey("DonationProductId")]
        public virtual DonationProduct Product { get; set; }
        public int? DonationProductId { get; set; }
    }
}
