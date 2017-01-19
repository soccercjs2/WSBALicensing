using Licensing.Data.Context;
using Licensing.Domain.Customers;
using Licensing.Domain.Licenses;
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

            LicensingContext context = new LicensingContext();

            //get customer
            Customer customer = context.Customers.Where(c => c.BarNumber == "555").FirstOrDefault();

            //get current license period
            LicensingPeriod licensingPeriod = context.LicensingPeriods.FirstOrDefault();

            //get license
            License license = customer.Licenses.Where(l => l.LicensingPeriodId == licensingPeriod.LicensingPeriodId).FirstOrDefault();

            //dashboardVM.Name = "Charlie Brown";
            //dashboardVM.BarNumber = "555";

            dashboardVM.Name = customer.FirstName + " " + customer.LastName;
            dashboardVM.BarNumber = customer.BarNumber;

            dashboardVM.MembershipType = new DashboardContainerVM("Membership Type", RequirementType.Required, true, "_MembershipType", license.LicenseType.Name);
            dashboardVM.TrustAccount = new DashboardContainerVM("Trust Account", RequirementType.Required, false, "_TrustAccount", "XXX");
            dashboardVM.ProfessionalLiabilityInsurance = new DashboardContainerVM("Professional Liability Insurance", RequirementType.Required, false, "_ProfessionalLiabilityInsurance", "XXX");
            dashboardVM.ProBono = new DashboardContainerVM("Pro Bono", RequirementType.Required, false, "_ProBono", "Did Pro Bono Stuff");

            dashboardVM.PrimaryAddress = new DashboardContainerVM("Primary Address", RequirementType.Required, false, "_Address", "XXX");
            dashboardVM.HomeAddress = new DashboardContainerVM("Home Address", RequirementType.Required, false, "_Address", "XXX");
            dashboardVM.AgentOfServiceAddress = new DashboardContainerVM("Agent of Service Address", RequirementType.Required, false, "_Address", "XXX");
            dashboardVM.Emails = new DashboardContainerVM("Emails", RequirementType.Required, false, "_Emails", "XXX");
            dashboardVM.PhoneNumbers = new DashboardContainerVM("Phone Numbers", RequirementType.Required, false, "_PhoneNumbers", "XXX");

            dashboardVM.AreasOfPractice = new DashboardContainerVM("Areas of Practice", RequirementType.Optional, false, "_AreasOfPractice", "XXX");
            dashboardVM.FirmSize = new DashboardContainerVM("Firm Size", RequirementType.Optional, false, "_FirmSize", "XXX");
            dashboardVM.Languages = new DashboardContainerVM("Languages", RequirementType.Optional, false, "_Languages", "XXX");

            dashboardVM.Ethnicity = new DashboardContainerVM("Ethnicity/Race", RequirementType.Optional, false);
            dashboardVM.Gender = new DashboardContainerVM("Gender", RequirementType.Optional, false);
            dashboardVM.Disability = new DashboardContainerVM("Disability", RequirementType.Optional, false);
            dashboardVM.SexualOrientation = new DashboardContainerVM("Sexual Orientation", RequirementType.Optional, false);

            dashboardVM.Sections = new DashboardContainerVM("Sections", RequirementType.Required, false, "_Sections", "XXX");
            dashboardVM.Donations = new DashboardContainerVM("Donations", RequirementType.Required, false, "_Donations", "XXX");

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