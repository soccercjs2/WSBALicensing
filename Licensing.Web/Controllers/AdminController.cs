using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class AdminController : Controller
    {
        LicensingContext _context;

        public AdminController()
        {
            _context = new LicensingContext();
        }

        public ActionResult Index()
        {
            AdminDashboardVM adminDashboardVM = new AdminDashboardVM();

            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicensePeriodManager licensePeriodManager = new LicensePeriodManager(_context);

            adminDashboardVM.LicenseTypes = licenseTypeManager.GetLicenseTypes();
            adminDashboardVM.LicensePeriods = licensePeriodManager.GetLicensePeriods();

            return View("Index", adminDashboardVM);
        }
    }
}