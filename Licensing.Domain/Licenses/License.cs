using Licensing.Domain.Addresses;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.BarNews;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Donations;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.FinancialResponsibilities;
using Licensing.Domain.FirmSizes;
using Licensing.Domain.Genders;
using Licensing.Domain.Judicial;
using Licensing.Domain.Languages;
using Licensing.Domain.ProBonos;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using Licensing.Domain.Sections;
using Licensing.Domain.SexualOrientations;
using Licensing.Domain.TrustAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Licenses
{
    public class License
    {
        public int LicenseId { get; set; }
        public int CustomerId { get; set; }

        //license details
        public virtual LicensingPeriod LicensingPeriod { get; set; }
        public int LicensingPeriodId { get; set; }

        public virtual LicenseType LicenseType { get; set; }
        public int LicenseTypeId { get; set; }

        //licensing information
        public virtual FinancialResponsibility FinancialResponsibility { get; set; }
        public int? FinancialResponsibilityId { get; set; }

        public virtual JudicialPosition JudicialPosition { get; set; }
        public int? JudicialPositionId { get; set; }

        public virtual ProBono ProBono { get; set; }
        public int? ProBonoId { get; set; }

        public virtual ProfessionalLiabilityInsurance ProfessionalLiabilityInsurance { get; set; }
        public int? ProfessionalLiabilityInsuranceId { get; set; }

        public virtual TrustAccount TrustAccount { get; set; }
        public int? TrustAccountId { get; set; }

        //contact information
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        //practice information
        public virtual ICollection<AreaOfPractice> AreasOfPractice { get; set; }

        public virtual FirmSize FirmSize { get; set; }
        public int? FirmSizeId { get; set; }

        public virtual ICollection<Language> Languages { get; set; }

        //demographics
        public virtual Disability Disability { get; set; }
        public int? DisabilityId { get; set; }

        public virtual Ethnicity Ethnicity { get; set; }
        public int? EthnicityId { get; set; }

        public virtual Gender Gender { get; set; }
        public int? GenderId { get; set; }

        public virtual SexualOrientation SexualOrientation { get; set; }
        public int? SexualOrientationId { get; set; }


        //payment
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Section> Sections { get; set; }

        public virtual BarNewsResponse BarNewsResponse { get; set; }
        public int? BarNewsResponseId { get; set; }
    }
}
