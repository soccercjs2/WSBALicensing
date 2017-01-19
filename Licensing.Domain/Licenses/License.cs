using Licensing.Domain.Addresses;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Donations;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.FinancialResponsibilities;
using Licensing.Domain.FirmSizes;
using Licensing.Domain.Genders;
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
        public LicensingPeriod LicensingPeriod { get; set; }
        public int LicensingPeriodId { get; set; }

        public LicenseType LicenseType { get; set; }
        public int LicenseTypeId { get; set; }

        //licensing information
        public FinancialResponsibility FinancialResponsibility { get; set; }
        public int FinancialResponsibilityId { get; set; }

        public ProBono ProBono { get; set; }
        public int ProBonoId { get; set; }

        public ProfessionalLiabilityInsurance ProfessionalLiabilityInsurance { get; set; }
        public int ProfessionalLiabilityInsuranceId { get; set; }

        public TrustAccount TrustAccount { get; set; }
        public int TrustAccountId { get; set; }

        //contact information
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Email> Emails { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        //practice information
        public ICollection<AreaOfPractice> AreasOfPractice { get; set; }

        public FirmSize FirmSize { get; set; }
        public int FirmSizeId { get; set; }

        public ICollection<Language> Languages { get; set; }

        //demographics
        public Disability Disability { get; set; }
        public int DisabilityId { get; set; }

        public Ethnicity Ethnicity { get; set; }
        public int EthnicityId { get; set; }

        public Gender Gender { get; set; }
        public int GenderId { get; set; }

        public SexualOrientation SexualOrientation { get; set; }
        public int SexualOrientationId { get; set; }


        //payment
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Section> Sections { get; set; }
        public bool? BarNews { get; set; }
    }
}
