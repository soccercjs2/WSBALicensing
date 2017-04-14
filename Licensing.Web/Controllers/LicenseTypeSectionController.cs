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
    public class LicenseTypeSectionController : Controller
    {
        LicensingContext _context;

        public LicenseTypeSectionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
            LicenseTypeSectionManager licenseTypeSectionManager = new LicenseTypeSectionManager(_context);
            LicenseType licenseType = licenseTypeManager.GetLicenseType(id);

            LicenseTypeSectionsVM licenseTypeSectionsVM = new LicenseTypeSectionsVM(
                licenseType, licenseType.LicenseTypeSections, licenseTypeSectionManager.GetExcludedSections(licenseType));

            return View("~/Views/Sections/EditLicenseTypeSections.cshtml", licenseTypeSectionsVM);
        }

        [HttpPost]
        public ActionResult Edit(LicenseTypeSectionsVM licenseTypeSectionsVM)
        {
            if (ModelState.IsValid)
            {
                LicenseTypeManager licenseTypeManager = new LicenseTypeManager(_context);
                LicenseType licenseType = licenseTypeManager.GetLicenseType(licenseTypeSectionsVM.LicenseTypeId);

                LicenseTypeSectionManager licenseTypeSectionManager = new LicenseTypeSectionManager(_context);

                if (licenseTypeSectionsVM.ExcludedSections != null)
                {
                    foreach (LicenseTypeSectionVM sectionVM in licenseTypeSectionsVM.ExcludedSections)
                    {
                        if (sectionVM.Flag)
                        {
                            licenseTypeSectionManager.AddLicenseTypeSection(licenseType, sectionVM.LicenseTypeSection);
                        }
                    }
                }

                if (licenseTypeSectionsVM.IncludedSections != null)
                {
                    foreach (LicenseTypeSectionVM sectionVM in licenseTypeSectionsVM.IncludedSections)
                    {
                        if (sectionVM.Flag)
                        {
                            licenseTypeSectionManager.DeleteLicenseTypeSection(licenseType, sectionVM.LicenseTypeSection);
                        }
                    }
                }

                return RedirectToAction("Edit", "LicenseTypeSection", new { id = licenseType.LicenseTypeId });
            }
            else
            {
                return View("~/Views/Sections/EditLicenseTypeSections.cshtml", licenseTypeSectionsVM);
            }
        }
    }
}