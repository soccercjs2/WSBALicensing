using Licensing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class LicenseType
    {
        public int LicenseTypeId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Member Type Code")]
        public string AmsMemberType { get; set; }

        [ForeignKey("LicenseTypeRequirementId")]
        public virtual LicenseTypeRequirement LicenseTypeRequirement { get; set; }
        public int? LicenseTypeRequirementId { get; set; }

        public virtual ICollection<LicenseTypeProduct> LicenseTypeProducts { get; set; }
        public virtual ICollection<LicenseTypeSection> LicenseTypeSections { get; set; }
        public virtual ICollection<LicenseTypeDonation> LicenseTypeDonations { get; set; }

        public decimal DefaultDonationAmount { get; set; }
    }
}
