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
            LicensePeriodManager licensePeriodManager = new LicensePeriodManager(context);
            JudicialPositionManager judicialPositionManager = new JudicialPositionManager(context);
            PracticeAreaManager practiceAreaManager = new PracticeAreaManager(context);
            TrustAccountManager trustAccountManager = new TrustAccountManager(context);
            ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(context);
            FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(context);
            ProBonoManager proBonoManager = new ProBonoManager(context);
            AddressManager addressManager = new AddressManager(context);
            EmailManager emailManager = new EmailManager(context);
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(context);
            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(context);
            FirmSizeManager firmSizeManager = new FirmSizeManager(context);
            LanguageManager languageManager = new LanguageManager(context);
            EthnicityManager ethnicityManager = new EthnicityManager(context);
            GenderManager genderManager = new GenderManager(context);
            DisabilityManager disabilityManager = new DisabilityManager(context);
            SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(context);
            MembershipProductManager membershipProductManager = new MembershipProductManager(context);
            SectionManager sectionManager = new SectionManager(context);
            DonationManager donationManager = new DonationManager(context);
            BarNewsManager barNewsManager = new BarNewsManager(context);
            StatusManager statusManager = new StatusManager(context);

            //get license
            LicensePeriod licensePeriod = licensePeriodManager.GetCurrentLicensePeriod();
            License license = licenseManager.GetLicense(customer, licensePeriod);

            //set license type for displaying dashboard controls
            dashboardVM.LicenseType = license.LicenseType;

            //set customer information for dashboard
            dashboardVM.Year = licensePeriod.EndDate.Year;
            dashboardVM.LicensingStatus = statusManager.GetLicensingStatus(license);

            //set licensing information for dashboard
            dashboardVM.MembershipType = licenseTypeManager.GetDashboardContainerVM(license);
            dashboardVM.JudicialPosition = judicialPositionManager.GetDashboardContainerVM(license);
            dashboardVM.PracticeAreas = practiceAreaManager.GetDashboardContainerVM(license);
            dashboardVM.TrustAccount = trustAccountManager.GetDashboardContainerVM(license);
            dashboardVM.ProfessionalLiabilityInsurance = professionalLiabilityInsuranceManager.GetDashboardContainerVM(license);
            dashboardVM.FinancialResponsibility = financialResponsibilityManager.GetDashboardContainerVM(license);
            dashboardVM.ProBono = proBonoManager.GetDashboardContainerVM(license);

            //set contact information for dashboard
            dashboardVM.PrimaryAddress = addressManager.GetDashboardContainerVM(license, "Primary");
            dashboardVM.HomeAddress = addressManager.GetDashboardContainerVM(license, "Home");
            dashboardVM.AgentOfServiceAddress = addressManager.GetDashboardContainerVM(license, "Agent of Service");
            dashboardVM.Emails = emailManager.GetDashboardContainerVM(license);
            dashboardVM.PhoneNumbers = phoneNumberManager.GetDashboardContainerVM(license);

            //set practice information for dashboard
            dashboardVM.AreasOfPractice = areaOfPracticeManager.GetDashboardContainerVM(license);
            dashboardVM.FirmSize = firmSizeManager.GetDashboardContainerVM(license);
            dashboardVM.Languages = languageManager.GetDashboardContainerVM(license);

            //set demographic information for dashboard
            dashboardVM.Ethnicity = ethnicityManager.GetDashboardContainerVM(license);
            dashboardVM.Gender = genderManager.GetDashboardContainerVM(license);
            dashboardVM.Disability = disabilityManager.GetDashboardContainerVM(license);
            dashboardVM.SexualOrientation = sexualOrientationManager.GetDashboardContainerVM(license);

            //set payment information for dashboard
            dashboardVM.MembershipProducts = membershipProductManager.GetDashboardContainerVM(license);
            dashboardVM.Sections = sectionManager.GetDashboardContainerVM(license);
            dashboardVM.Donations = donationManager.GetDashboardContainerVM(license);
            dashboardVM.BarNews = barNewsManager.GetDashboardContainerVM(license);

            return View(dashboardVM);
        }
    }
}