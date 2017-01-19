using Licensing.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Licensing.Domain.Addresses;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Customers;
using Licensing.Domain.Donations;
using Licensing.Domain.FinancialResponsibilities;
using Licensing.Domain.FirmSizes;
using Licensing.Domain.Languages;
using Licensing.Domain.Licenses;
using Licensing.Domain.ProBonos;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using Licensing.Domain.Sections;
using Licensing.Domain.TrustAccounts;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Genders;
using Licensing.Domain.SexualOrientations;

namespace Licensing.Data.Initializer
{
    public class LicensingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LicensingContext>
    {
        protected override void Seed(LicensingContext context)
        {
            
        }
    }
}
