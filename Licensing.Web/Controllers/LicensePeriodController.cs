using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class LicensePeriodController : Controller
    {
        LicensingContext _context;

        public LicensePeriodController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Create()
        {
            LicensePeriod licensePeriod = new LicensePeriod();
            return View("EditLicensePeriod", new LicensePeriodVM(licensePeriod));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicensePeriodManager licensePeriodManager = new LicensePeriodManager(_context);

            return View("EditLicensePeriod", new LicensePeriodVM(licensePeriodManager.GetLicensePeriod(id)));
        }

        [HttpPost]
        public ActionResult Edit(LicensePeriodVM licensePeriodVM)
        {
            if (ModelState.IsValid)
            {
                LicensePeriodManager licensePeriodManager = new LicensePeriodManager(_context);
                licensePeriodManager.SetLicensePeriod(licensePeriodVM.LicensePeriod);

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View("EditLicensePeriod", licensePeriodVM);
            }
        }
    }
}