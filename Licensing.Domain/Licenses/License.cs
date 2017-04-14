using Licensing.Domain.Addresses;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.BarNews;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Customers;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Donations;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.FinancialResponsibilities;
using Licensing.Domain.FirmSizes;
using Licensing.Domain.Genders;
using Licensing.Domain.Judicial;
using Licensing.Domain.Languages;
using Licensing.Domain.PracticeAreas;
using Licensing.Domain.ProBonos;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using Licensing.Domain.Sections;
using Licensing.Domain.SexualOrientations;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class License
    {
        public int LicenseId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }

        //license details
        [ForeignKey("LicensePeriodId")]
        public virtual LicensePeriod LicensePeriod { get; set; }
        public int? LicensePeriodId { get; set; }

        [ForeignKey("LicenseTypeId")]
        public virtual LicenseType LicenseType { get; set; }
        public int? LicenseTypeId { get; set; }

        [ForeignKey("PreviousLicenseTypeId")]
        public virtual LicenseType PreviousLicenseType { get; set; }
        public int? PreviousLicenseTypeId { get; set; }

        //licensing information
        [ForeignKey("FinancialResponsibilityId")]
        public virtual FinancialResponsibility FinancialResponsibility { get; set; }
        public int? FinancialResponsibilityId { get; set; }

        [ForeignKey("JudicialPositionId")]
        public virtual JudicialPosition JudicialPosition { get; set; }
        public int? JudicialPositionId { get; set; }

        public virtual ICollection<PracticeArea> PracticeAreas { get; set; }
        public bool PracticeAreasConfirmed { get; set; }

        [ForeignKey("ProBonoId")]
        public virtual ProBono ProBono { get; set; }
        public int? ProBonoId { get; set; }

        [ForeignKey("ProfessionalLiabilityInsuranceId")]
        public virtual ProfessionalLiabilityInsurance ProfessionalLiabilityInsurance { get; set; }
        public int? ProfessionalLiabilityInsuranceId { get; set; }

        [ForeignKey("TrustAccountId")]
        public virtual TrustAccount TrustAccount { get; set; }
        public int? TrustAccountId { get; set; }

        //contact information
        public virtual ICollection<Address> Addresses { get; set; }

        [ForeignKey("EmailId")]
        public virtual Email Email { get; set; }
        public int? EmailId { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        //practice information
        public virtual ICollection<AreaOfPractice> AreasOfPractice { get; set; }
        public bool AreasOfPracticeConfirmed { get; set; }

        [ForeignKey("FirmSizeId")]
        public virtual FirmSize FirmSize { get; set; }
        public int? FirmSizeId { get; set; }

        public virtual ICollection<Language> Languages { get; set; }
        public bool LanguagesConfirmed { get; set; }

        //demographics
        [ForeignKey("DisabilityId")]
        public virtual Disability Disability { get; set; }
        public int? DisabilityId { get; set; }

        [ForeignKey("EthnicityId")]
        public virtual Ethnicity Ethnicity { get; set; }
        public int? EthnicityId { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }
        public int? GenderId { get; set; }

        [ForeignKey("SexualOrientationId")]
        public virtual SexualOrientation SexualOrientation { get; set; }
        public int? SexualOrientationId { get; set; }


        //payment
        public virtual ICollection<Donation> Donations { get; set; }
        public bool DonationsConfirmed { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
        public bool SectionsConfirmed { get; set; }

        [ForeignKey("BarNewsResponseId")]
        public virtual BarNewsResponse BarNewsResponse { get; set; }
        public int? BarNewsResponseId { get; set; }

        public bool KellerDeduction { get; set; }

        public DateTime? LastAmsUpdate { get; set; }
    }
}
