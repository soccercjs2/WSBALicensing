using Licensing.Web.Models;
using Licensing.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DashboardVM dashboardVM = new DashboardVM();

            dashboardVM.Name = "Charlie Brown";
            dashboardVM.BarNumber = "555";

            dashboardVM.MembershipType = new DashboardContainerVM() { Title = "Membership Type", RequirementType = RequirementType.Required, Complete = true, PartialViewName = "_MembershipType", PartialViewData = "Active Attorney" };
            dashboardVM.TrustAccount = new DashboardContainerVM() { Title = "Trust Account", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_TrustAccount", PartialViewData = "Manages Trust Accounts" };
            dashboardVM.ProfessionalLiabilityInsurance = new DashboardContainerVM() { Title = "Professional Liability Insurance", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_ProfessionalLiabilityInsurance", PartialViewData = "Option 1" };
            dashboardVM.ProBono = new DashboardContainerVM() { Title = "Pro Bono", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_ProBono", PartialViewData = "Did Pro Bono Stuff" };

            dashboardVM.PrimaryAddress = new DashboardContainerVM() { Title = "Primary Address", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_Address", PartialViewData = "1325 4th Ave Seattle WA 98101" };
            dashboardVM.HomeAddress = new DashboardContainerVM() { Title = "Home Address", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_Address", PartialViewData = "6421 152nd St. SE Snohomish WA 98296" };
            dashboardVM.AgentOfServiceAddress = new DashboardContainerVM() { Title = "Agent of Service Address", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_Address", PartialViewData = "1325 4th Ave Seattle WA 98101" };
            dashboardVM.Emails = new DashboardContainerVM() { Title = "Emails", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_Emails", PartialViewData = "Primary: collins@wsba.org" };
            dashboardVM.PhoneNumbers = new DashboardContainerVM() { Title = "Phone Numbers", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_PhoneNumbers", PartialViewData = "Primary: (555) 123-4567" };

            dashboardVM.AreasOfPractice = new DashboardContainerVM() { Title = "Areas of Practice", RequirementType = RequirementType.Optional, Complete = false, PartialViewName = "_AreasOfPractice", PartialViewData = "Family Law, Business Law" };
            dashboardVM.FirmSize = new DashboardContainerVM() { Title = "Firm Size", RequirementType = RequirementType.Optional, Complete = false, PartialViewName = "_FirmSize", PartialViewData = "Solo" };
            dashboardVM.Languages = new DashboardContainerVM() { Title = "Languages", RequirementType = RequirementType.Optional, Complete = false, PartialViewName = "_Languages", PartialViewData = "English, Spanish" };

            dashboardVM.Ethnicity = new DashboardContainerVM() { Title = "Ethnicity/Race", RequirementType = RequirementType.Optional, Complete = true };
            dashboardVM.Gender = new DashboardContainerVM() { Title = "Gender", RequirementType = RequirementType.Optional, Complete = true };
            dashboardVM.Disability = new DashboardContainerVM() { Title = "Disability", RequirementType = RequirementType.Optional, Complete = false };
            dashboardVM.SexualOrientation = new DashboardContainerVM() { Title = "SexualOrientation", RequirementType = RequirementType.Optional, Complete = false };

            dashboardVM.Sections = new DashboardContainerVM() { Title = "Sections", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_Sections", PartialViewData = "Family Law, Business Law" };
            dashboardVM.Donations = new DashboardContainerVM() { Title = "Donations", RequirementType = RequirementType.Required, Complete = false, PartialViewName = "_Donations", PartialViewData = "BFD: 50, C4EJ: 50" };

            return View(dashboardVM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}