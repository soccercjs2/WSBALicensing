using Licensing.Business.Managers;
using Licensing.Data.Context;
using Licensing.Domain.Addresses;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Customers;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using Licensing.Web.Models;
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
            LicensingPeriod licensingPeriod = context.LicensingPeriods.Where(lp => lp.EndDate == new DateTime(2019, 5, 1)).FirstOrDefault();

            //get license
            License license = customer.Licenses.Where(l => l.LicensingPeriodId == licensingPeriod.LicensingPeriodId).FirstOrDefault();

            //set license type for displaying dashboard controls
            dashboardVM.LicenseType = license.LicenseType;

            //set customer information for dashboard
            dashboardVM.Name = customer.FirstName + " " + customer.LastName;
            dashboardVM.BarNumber = customer.BarNumber;

            //set licensing information for dashboard
            dashboardVM.MembershipType = new DashboardContainerVM("Membership Type", RequirementType.Required, true, false, "_MembershipType", license.LicenseType.Name);
            dashboardVM.JudicialPosition = new DashboardContainerVM("Judicial Position", RequirementType.Required, false, true, "_JudicialPosition", license.JudicialPosition);
            dashboardVM.TrustAccount = new DashboardContainerVM("Trust Account", RequirementType.Required, false, true, "_TrustAccount", license.TrustAccount);
            dashboardVM.ProfessionalLiabilityInsurance = new DashboardContainerVM("Professional Liability Insurance", RequirementType.Required, false, true, "_ProfessionalLiabilityInsurance", license.ProfessionalLiabilityInsurance);
            dashboardVM.FinancialResponsibility = new DashboardContainerVM("Financial Responsibility", RequirementType.Required, false, true, "_FinancialResponsibility", license.FinancialResponsibility);
            dashboardVM.ProBono = new DashboardContainerVM("Pro Bono", RequirementType.Required, false, true, "_ProBono", license.ProBono);

            //set contact information for dashboard

            //create Address Manager
            AddressManager addressManager = new AddressManager(context, license);

            //create Email Manager and EmailsVM
            EmailManager emailManager = new EmailManager(context, license);
            Email primaryEmail = emailManager.GetPrimaryEmail();
            Email homeEmail = emailManager.GetHomeEmail();

            EmailsVM emailsVM = null;
            if (primaryEmail != null || homeEmail != null)
            {
                emailsVM = new EmailsVM();
                emailsVM.PrimaryEmail = emailManager.GetPrimaryEmail();
                emailsVM.HomeEmail = emailManager.GetHomeEmail();
            }

            //create Phone Number Manager and PhoneNumbersVM
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(context, license);
            PhoneNumber primaryPhoneNumber = phoneNumberManager.GetPrimaryPhoneNumber();
            PhoneNumber homePhoneNumber = phoneNumberManager.GetHomePhoneNumber();
            PhoneNumber faxPhoneNumber = phoneNumberManager.GetFaxPhoneNumber();

            PhoneNumbersVM phoneNumbersVM = null;
            if (primaryPhoneNumber != null || homePhoneNumber != null || faxPhoneNumber != null)
            {
                phoneNumbersVM = new PhoneNumbersVM();
                phoneNumbersVM.PrimaryPhoneNumber = phoneNumberManager.GetFormattedPhoneNumber(primaryPhoneNumber);
                phoneNumbersVM.HomePhoneNumber = phoneNumberManager.GetFormattedPhoneNumber(homePhoneNumber);
                phoneNumbersVM.FaxPhoneNumber = phoneNumberManager.GetFormattedPhoneNumber(faxPhoneNumber);
            }

            dashboardVM.PrimaryAddress = new DashboardContainerVM("Primary Address", RequirementType.Required, false, true, "_Address", addressManager.GetPrimaryAddress());
            dashboardVM.HomeAddress = new DashboardContainerVM("Home Address", RequirementType.Required, false, true, "_Address", addressManager.GetHomeAddress());
            dashboardVM.AgentOfServiceAddress = new DashboardContainerVM("Agent of Service Address", RequirementType.Required, false, true, "_Address", addressManager.GetAgentOfServiceAddress());
            dashboardVM.Emails = new DashboardContainerVM("Emails", RequirementType.Required, false, true, "_Emails", emailsVM);
            dashboardVM.PhoneNumbers = new DashboardContainerVM("Phone Numbers", RequirementType.Required, false, true, "_PhoneNumbers", phoneNumbersVM);

            //set practice information for dashboard
            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(context, license);
            LanguageManager languageManager = new LanguageManager(context, license);

            dashboardVM.AreasOfPractice = new DashboardContainerVM("Areas of Practice", RequirementType.Optional, false, true, "_AreasOfPractice", areaOfPracticeManager.GetAreasOfPractice());
            dashboardVM.FirmSize = new DashboardContainerVM("Firm Size", RequirementType.Optional, false, true, "_FirmSize", license.FirmSize);
            dashboardVM.Languages = new DashboardContainerVM("Languages", RequirementType.Optional, false, true, "_Languages", languageManager.GetLanguages());

            //set demographic information for dashboard
            dashboardVM.Ethnicity = new DashboardContainerVM("Ethnicity/Race", RequirementType.Optional, false, false);
            dashboardVM.Gender = new DashboardContainerVM("Gender", RequirementType.Optional, false, false);
            dashboardVM.Disability = new DashboardContainerVM("Disability", RequirementType.Optional, false, false);
            dashboardVM.SexualOrientation = new DashboardContainerVM("Sexual Orientation", RequirementType.Optional, false, false);

            //set payment information for dashboard
            SectionManager sectionManager = new SectionManager(context, license);
            DonationManager donationManager = new DonationManager(context, license);

            dashboardVM.Sections = new DashboardContainerVM("Sections", RequirementType.Required, false, true, "_Sections", sectionManager.GetSections());
            dashboardVM.Donations = new DashboardContainerVM("Donations", RequirementType.Required, false, true, "_Donations", donationManager.GetDonations());
            dashboardVM.BarNews = new DashboardContainerVM("Bar News", RequirementType.Required, false, true, "_BarNews", license.BarNewsResponse);

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