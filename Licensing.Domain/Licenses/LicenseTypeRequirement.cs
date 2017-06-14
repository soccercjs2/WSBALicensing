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
    public class LicenseTypeRequirement
    {
        public int LicenseTypeRequirementId { get; set; }

        [Display(Name = "Membership Type")]
        public RequirementType MembershipType { get; set; }
        [Display(Name = "Judicial Position")]
        public RequirementType JudicialPosition { get; set; }
        [Display(Name = "Practice Areas")]
        public RequirementType PracticeAreas { get; set; }
        [Display(Name = "Trust Account")]
        public RequirementType TrustAccount { get; set; }
        [Display(Name = "Professional Liability Insurance")]
        public RequirementType ProfessionalLiabilityInsurance { get; set; }
        [Display(Name = "Financial Responsibility")]
        public RequirementType FinancialResponsibility { get; set; }
        [Display(Name = "Pro Bono")]
        public RequirementType ProBono { get; set; }
        public RequirementType MCLE { get; set; }

        //contact information
        [Display(Name = "Primary Address")]
        public RequirementType PrimaryAddress { get; set; }
        [Display(Name = "Home Address")]
        public RequirementType HomeAddress { get; set; }
        [Display(Name = "Agent Of Service Address")]
        public RequirementType AgentOfServiceAddress { get; set; }
        [Display(Name = "Primary Email")]
        public RequirementType PrimaryEmail { get; set; }
        [Display(Name = "Primary Phone Number")]
        public RequirementType PrimaryPhoneNumber { get; set; }
        [Display(Name = "Home Phone Number")]
        public RequirementType HomePhoneNumber { get; set; }
        [Display(Name = "Fax Phone Number")]
        public RequirementType FaxPhoneNumber { get; set; }

        //practice information
        [Display(Name = "Areas Of Practice")]
        public RequirementType AreasOfPractice { get; set; }
        [Display(Name = "Firm Size")]
        public RequirementType FirmSize { get; set; }
        public RequirementType Languages { get; set; }

        //demographics
        public RequirementType Disability { get; set; }
        public RequirementType Ethnicity { get; set; }
        public RequirementType Gender { get; set; }
        [Display(Name = "Sexual Orientation")]
        public RequirementType SexualOrientation { get; set; }

        //payment information
        public RequirementType Donations { get; set; }
        public RequirementType Sections { get; set; }
        [Display(Name = "Bar News")]
        public RequirementType BarNews { get; set; }
        [Display(Name = "Hardship Exemption")]
        public RequirementType HardshipExemption { get; set; }
        [Display(Name = "Keller Deduction")]
        public RequirementType KellerDeduction { get; set; }
    }
}
