using Licensing.Domain.Disabilities;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Genders;
using Licensing.Domain.Licenses;
using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class DemographicsVM
    {
        public int LicenseId { get; set; }
        public int SelectedDisabilityOptionId { get; set; }
        public int SelectedGenderOptionId { get; set; }
        public int SelectedEthnicityOptionId { get; set; }
        public int SelectedSexualOrientationOptionId { get; set; }
        public ICollection<DisabilityOption> DisabilityOptions { get; set; }
        public ICollection<GenderOption> GenderOptions { get; set; }
        public ICollection<EthnicityOption> EthnicityOptions { get; set; }
        public ICollection<SexualOrientationOption> SexualOrientationOptions { get; set; }
        public bool HasEnteredDemogracphics { get; set; }
        public bool HasUnenteredDemogracphics { get; set; }
        public string DemographicsResponded { get; set; }
        public string DemographicsNotResponded { get; set; }

        public DemographicsVM() { }
        public DemographicsVM(
            License license, 
            ICollection<DisabilityOption> disabilityOptions, 
            ICollection<GenderOption> genderOption, 
            ICollection<EthnicityOption> ethnicityOption,
            ICollection<SexualOrientationOption> sexualOrientationOption)
        {
            LicenseId = license.LicenseId;
            DisabilityOptions = disabilityOptions;
            GenderOptions = genderOption;
            EthnicityOptions = ethnicityOption;
            SexualOrientationOptions = sexualOrientationOption;

            if (license.Disability != null || license.Gender != null || license.Ethnicity != null || license.SexualOrientation != null)
            {
                HasEnteredDemogracphics = true;
            }

            if (license.Disability == null || license.Gender == null || license.Ethnicity == null || license.SexualOrientation == null)
            {
                HasUnenteredDemogracphics = true;
            }

            DemographicsResponded = "";
            DemographicsNotResponded = "";

            if (license.Disability != null) { DemographicsResponded += "Disability"; }
            else { DemographicsNotResponded += "Disability"; }

            if (license.Gender != null) { DemographicsResponded += (DemographicsResponded.Length == 0) ? "Gender" : ", Gender"; }
            else { DemographicsNotResponded += (DemographicsNotResponded.Length == 0) ? "Gender" : ", Gender"; }

            if (license.Ethnicity != null) { DemographicsResponded += (DemographicsResponded.Length == 0) ? "Ethnicity/Race" : ", Ethnicity/Race"; }
            else { DemographicsNotResponded += (DemographicsNotResponded.Length == 0) ? "Ethnicity/Race" : ", Ethnicity/Race"; }

            if (license.SexualOrientation != null) { DemographicsResponded += (DemographicsResponded.Length == 0) ? "Sexual Orientation" : ", Sexual Orientation"; }
            else { DemographicsNotResponded += (DemographicsNotResponded.Length == 0) ? "Sexual Orientation" : ", Sexual Orientation"; }
        }
    }
}
