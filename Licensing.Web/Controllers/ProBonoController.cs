using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.ProBonos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class ProBonoController : Controller
    {
        LicensingContext _context;

        public ProBonoController()
        {
            _context = new LicensingContext();
        }

        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            return View("EditProBono", new ProBonoVM(license));
        }

        public ActionResult ProvidesService(int id)
        {
            //get license
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            ProBonoManager proBonoManager = new ProBonoManager(_context);
            proBonoManager.SetProvidesService(license);

            return RedirectToAction("Edit", "ProBono", new { Id = id });
        }

        public ActionResult NotProvidesService(int id)
        {
            //get license
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            ProBonoManager proBonoManager = new ProBonoManager(_context);
            proBonoManager.SetNotProvidesService(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(ProBonoVM proBonoVM, string submit)
        {
            if (ModelState.IsValid)
            {
                switch (submit)
                {
                    case "ProvidesService":
                        proBonoVM = ProvidesService(proBonoVM);
                        ModelState.Clear();
                        return View("EditProBono", proBonoVM);
                    case "NotProvidesService":
                        proBonoVM = NotProvidesService(proBonoVM);
                        ModelState.Clear();
                        return View("EditProBono", proBonoVM);
                    case "Save":
                        Save(proBonoVM);
                        ModelState.Clear();
                        return RedirectToAction("Index", "Home");
                }

                return View("EditProBono", proBonoVM);
            }
            else
            {
                return View("EditProfessionalLiabilityInsurance", proBonoVM);
            }
        }

        private ProBonoVM ProvidesService(ProBonoVM proBonoVM)
        {
            if (proBonoVM.ProBono == null) { proBonoVM.ProBono = new ProBono(); }
            proBonoVM.ProBono.ProvidesService = true;
            proBonoVM.ProvidesServiceCssClass = "btn-success";
            proBonoVM.NotProvidesServiceCssClass = "btn-default";

            return proBonoVM;
        }

        private ProBonoVM NotProvidesService(ProBonoVM proBonoVM)
        {
            if (proBonoVM.ProBono == null) { proBonoVM.ProBono = new ProBono(); }
            proBonoVM.ProBono.ProvidesService = false;
            proBonoVM.ProvidesServiceCssClass = "btn-default";
            proBonoVM.NotProvidesServiceCssClass = "btn-danger";

            return proBonoVM;
        }

        private void Save(ProBonoVM proBonoVM)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(proBonoVM.LicenseId);

            ProBonoManager proBonoManager = new ProBonoManager(_context);

            if (!proBonoVM.ProBono.ProvidesService)
            {
                proBonoManager.SetNotProvidesService(license);
            }
            else
            {
                proBonoManager.SetProvidesService(license);
                proBonoManager.SetProBonoDetails(license, proBonoVM.ProBono.FreeServiceHours, proBonoVM.ProBono.LimitedFeeServiceHours, proBonoVM.ProBono.Anonymous);
            }
        }
    }
}