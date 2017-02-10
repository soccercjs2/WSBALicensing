using Licensing.Business.Enums;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licensing.Business.ViewModels
{
    public class DashboardVM
    {
        public LicenseType LicenseType { get; set; }
        
        //customer info
        public string Name { get; set; }
        public string BarNumber { get; set; }
        public LicensingStatus LicensingStatus { get; set; }

        //license information
        public DashboardContainerVM MembershipType { get; set; }
        public DashboardContainerVM JudicialPosition { get; set; }
        public DashboardContainerVM TrustAccount { get; set; }
        public DashboardContainerVM ProfessionalLiabilityInsurance { get; set; }
        public DashboardContainerVM FinancialResponsibility { get; set; }
        public DashboardContainerVM ProBono { get; set; }

        //contact information
        public DashboardContainerVM PrimaryAddress { get; set; }
        public DashboardContainerVM HomeAddress { get; set; }
        public DashboardContainerVM AgentOfServiceAddress { get; set; }
        public DashboardContainerVM Emails { get; set; }
        public DashboardContainerVM PhoneNumbers { get; set; }

        //practice information and demographics
        public DashboardContainerVM AreasOfPractice { get; set; }
        public DashboardContainerVM FirmSize { get; set; }
        public DashboardContainerVM Languages { get; set; }

        public DashboardContainerVM Ethnicity { get; set; }
        public DashboardContainerVM Gender { get; set; }
        public DashboardContainerVM Disability { get; set; }
        public DashboardContainerVM SexualOrientation { get; set; }

        //payment information
        public DashboardContainerVM MembershipProducts { get; set; }
        public DashboardContainerVM Sections { get; set; }
        public DashboardContainerVM Donations { get; set; }
        public DashboardContainerVM BarNews { get; set; }
        public DashboardContainerVM Payment { get; set; }
    }
}