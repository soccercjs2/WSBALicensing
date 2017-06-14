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
        LicensingContext _context;

        public HomeController()
        {
            _context = new LicensingContext();
        }

        public ActionResult Index()
        {
            if (Session["CurrentUser"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DashboardVM dashboardVM = new DashboardVM();

            //get current logged in user
            string currentUserBarNumber = Session["CurrentUser"].ToString();

            //create managers
            CustomerManager customerManager = new CustomerManager(_context);
            LicenseManager licenseManager = new LicenseManager(_context);
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicensePeriodManager licensePeriodManager = new LicensePeriodManager(_context);
            JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);
            PracticeAreaManager practiceAreaManager = new PracticeAreaManager(_context);
            TrustAccountManager trustAccountManager = new TrustAccountManager(_context);
            ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
            FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
            ProBonoManager proBonoManager = new ProBonoManager(_context);
            MCLEManager mcleManager = new MCLEManager(_context);
            AddressManager addressManager = new AddressManager(_context);
            EmailManager emailManager = new EmailManager(_context);
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);
            FirmSizeManager firmSizeManager = new FirmSizeManager(_context);
            LanguageManager languageManager = new LanguageManager(_context);
            DemographicManager demographicManager = new DemographicManager(_context);
            MembershipProductManager membershipProductManager = new MembershipProductManager(_context);
            SectionManager sectionManager = new SectionManager(_context);
            DonationManager donationManager = new DonationManager(_context);
            BarNewsManager barNewsManager = new BarNewsManager(_context);
            OrderManager orderManager = new OrderManager(_context);
            StatusManager statusManager = new StatusManager(_context);
            EmployerManager employerManager = new EmployerManager(_context);

            //get customer
            Customer customer = customerManager.GetCustomer(currentUserBarNumber);

            //get license
            LicensePeriod licensePeriod = licensePeriodManager.GetCurrentLicensePeriod();
            License license = licenseManager.GetLicense(customer, licensePeriod);

            //set license type for displaying dashboard controls
            dashboardVM.LicenseId = license.LicenseId;
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
            dashboardVM.MCLE = mcleManager.GetDashboardContainerVM(license);

            //set contact information for dashboard
            dashboardVM.AgentOfServiceAddressRequired = addressManager.AgentOfServiceAddressRequired(license);

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
            dashboardVM.Demographics = demographicManager.GetDashboardContainerVM(license);

            //set payment information for dashboard
            dashboardVM.MembershipProducts = membershipProductManager.GetDashboardContainerVM(license);
            dashboardVM.Sections = sectionManager.GetDashboardContainerVM(license);
            dashboardVM.Donations = donationManager.GetDashboardContainerVM(license);
            dashboardVM.BarNews = barNewsManager.GetDashboardContainerVM(license);

            //balance information
            decimal licensingBalance = membershipProductManager.GetBalanceDue(license);
            decimal sectionBalance = sectionManager.GetBalanceDue(license);
            decimal donationBalance = donationManager.GetBalanceDue(license);

            dashboardVM.BalanceDue = licensingBalance + sectionBalance + donationBalance;

            //bulk payment information
            dashboardVM.BulkPayment = employerManager.GetDashboardContainerVM(license, licensingBalance);

            return View(dashboardVM);
        }

        public ActionResult Save(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            AmsWriterManager amsWriterManager = new AmsWriterManager(_context);
            amsWriterManager.Save(license);

            return RedirectToAction("Index", "Home");
        }
    }
}