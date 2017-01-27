using Licensing.Domain.Enums;
using System;
using System.Collections.Generic;
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
        
        public virtual ICollection<LicenseTypeProduct> LicenseTypeProducts { get; set; }
        public virtual ICollection<LicenseTypeSection> LicenseTypeSections { get; set; }

        //licensing information
        public RequirementType MembershipType { get; set; }
        public RequirementType JudicialPosition { get; set; }
        public RequirementType TrustAccount { get; set; }
        public RequirementType ProfessionalLiabilityInsurance { get; set; }
        public RequirementType FinancialResponsibility { get; set; }
        public RequirementType ProBono { get; set; }

        //contact information
        public RequirementType PrimaryAddress { get; set; }
        public RequirementType HomeAddress { get; set; }
        public RequirementType AgentOfServiceAddress { get; set; }
        public RequirementType PrimaryEmail { get; set; }
        public RequirementType HomeEmail { get; set; }
        public RequirementType PrimaryPhoneNumber { get; set; }
        public RequirementType HomePhoneNumber { get; set; }
        public RequirementType FaxPhoneNumber { get; set; }

        //practice information
        public RequirementType AreasOfPractice { get; set; }
        public RequirementType FirmSize { get; set; }
        public RequirementType Languages { get; set; }

        //demographics
        public RequirementType Disability { get; set; }
        public RequirementType Ethnicity { get; set; }
        public RequirementType Gender { get; set; }
        public RequirementType SexualOrientation { get; set; }

        //payment information
        public RequirementType Donations { get; set; }
        public RequirementType Sections { get; set; }
        public RequirementType BarNews { get; set; }
    }
}
