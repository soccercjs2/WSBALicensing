using Licensing.Business.Managers;
using Licensing.Data.Context;
using Licensing.Domain.Addresses;
using Licensing.Domain.ContactInformation;
using Licensing.Domain.Customers;
using Licensing.Domain.Enums;
using Licensing.Domain.Licenses;
using Licensing.Business.ViewModels;
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

            //create managers
            LicenseManager licenseManager = new LicenseManager(context);
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(context);
            JudicialPositionManager judicialPositionManager = new JudicialPositionManager(context);
            TrustAccountManager trustAccountManager = new TrustAccountManager(context);
            ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(context);
            FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(context);
            ProBonoManager proBonoManager = new ProBonoManager(context);
            AddressManager addressManager = new AddressManager(context);
            EmailManager emailManager = new EmailManager(context);
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(context);
            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(context);
            LanguageManager languageManager = new LanguageManager(context);
            SectionManager sectionManager = new SectionManager(context);
            DonationManager donationManager = new DonationManager(context);

            //get license
            License license = licenseManager.GetCurrentLicense(customer);

            //create view models
            EmailsVM emailsVM = new EmailsVM(emailManager.GetPrimaryEmail(license), emailManager.GetHomeEmail(license));
            PhoneNumbersVM phoneNumbersVM = new PhoneNumbersVM(
                phoneNumberManager.GetPrimaryPhoneNumber(license), 
                phoneNumberManager.GetHomePhoneNumber(license), 
                phoneNumberManager.GetFaxPhoneNumber(license)
            );

            //set license type for displaying dashboard controls
            dashboardVM.LicenseType = license.LicenseType;

            //set customer information for dashboard
            dashboardVM.Name = customer.FirstName + " " + customer.LastName;
            dashboardVM.BarNumber = customer.BarNumber;

            //set licensing information for dashboard
            dashboardVM.MembershipType = licenseTypeManager.GetDashboardContainerVM(license);
            dashboardVM.JudicialPosition = judicialPositionManager.GetDashboardContainerVM(license);
            dashboardVM.TrustAccount = trustAccountManager.GetDashboardContainerVM(license);
            dashboardVM.ProfessionalLiabilityInsurance = professionalLiabilityInsuranceManager.GetDashboardContainerVM(license);
            dashboardVM.FinancialResponsibility = financialResponsibilityManager.GetDashboardContainerVM(license);
            dashboardVM.ProBono = proBonoManager.GetDashboardContainerVM(license);

            //set contact information for dashboard
            dashboardVM.PrimaryAddress = new DashboardContainerVM("Primary Address", license.LicenseType.PrimaryAddress, false, null, null, null, "_Address", addressManager.GetPrimaryAddress(license));
            dashboardVM.HomeAddress = new DashboardContainerVM("Home Address", license.LicenseType.HomeAddress, false, null, null, null, "_Address", addressManager.GetHomeAddress(license));
            dashboardVM.AgentOfServiceAddress = new DashboardContainerVM("Agent of Service Address", license.LicenseType.AgentOfServiceAddress, false, null, null, null, "_Address", addressManager.GetAgentOfServiceAddress(license));
            dashboardVM.Emails = new DashboardContainerVM("Emails", license.LicenseType.PrimaryEmail, false, null, null, null, "_Emails", emailsVM);
            dashboardVM.PhoneNumbers = new DashboardContainerVM("Phone Numbers", license.LicenseType.PrimaryPhoneNumber, false, null, null, null, "_PhoneNumbers", phoneNumbersVM);

            //set practice information for dashboard
            dashboardVM.AreasOfPractice = new DashboardContainerVM("Areas of Practice", license.LicenseType.AreasOfPractice, false, null, null, null, "_AreasOfPractice", areaOfPracticeManager.GetAreasOfPractice(license));
            dashboardVM.FirmSize = new DashboardContainerVM("Firm Size", license.LicenseType.FirmSize, false, null, null, null, "_FirmSize", license.FirmSize);
            dashboardVM.Languages = new DashboardContainerVM("Languages", license.LicenseType.Languages, false, null, null, null, "_Languages", languageManager.GetLanguages(license));

            //set demographic information for dashboard
            dashboardVM.Ethnicity = new DashboardContainerVM("Ethnicity/Race", license.LicenseType.Ethnicity, false, null, null, null, null, null);
            dashboardVM.Gender = new DashboardContainerVM("Gender", license.LicenseType.Gender, false, null, null, null, null, null);
            dashboardVM.Disability = new DashboardContainerVM("Disability", license.LicenseType.Disability, false, null, null, null, null, null);
            dashboardVM.SexualOrientation = new DashboardContainerVM("Sexual Orientation", license.LicenseType.SexualOrientation, false, null, null, null, null, null);

            //set payment information for dashboard
            dashboardVM.Sections = new DashboardContainerVM("Sections", license.LicenseType.Sections, false, null, null, null, "_Sections", sectionManager.GetSections(license));
            dashboardVM.Donations = new DashboardContainerVM("Donations", license.LicenseType.Donations, false, null, null, null, "_Donations", donationManager.GetDonations(license));
            dashboardVM.BarNews = new DashboardContainerVM("Bar News", license.LicenseType.BarNews, false, null, null, null, "_BarNews", license.BarNewsResponse);

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