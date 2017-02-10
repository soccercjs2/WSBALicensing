using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class JudicialCitationVM
    {
        public int LicenseId { get; set; }
        public int SelectedJudicialPositionOptionId { get; set; }
        public string Citation { get; set; }

        public JudicialCitationVM() { }

        public JudicialCitationVM(License license, int selectedJudicialPositionOptionId, string citation)
        {
            LicenseId = license.LicenseId;
            SelectedJudicialPositionOptionId = selectedJudicialPositionOptionId;
            Citation = citation;
        }
    }
}
