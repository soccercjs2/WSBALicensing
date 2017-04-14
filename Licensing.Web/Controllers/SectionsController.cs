using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class SectionsController : Controller
    {
        LicensingContext _context;

        public SectionsController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //get license who's Sections to confirm
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            //confirm the preloaded Sections
            SectionManager sectionManager = new SectionManager(_context);
            sectionManager.Confirm(license);

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            LicenseManager licenseManager = new LicenseManager(_context);
            License license = licenseManager.GetLicense(id);

            List<SectionProduct> sectionProducts = new List<SectionProduct>();

            foreach (LicenseTypeSection licenseTypeSection in license.LicenseType.LicenseTypeSections)
            {
                sectionProducts.Add(licenseTypeSection.Product);
            }

            return View("EditSections", new SectionVM(license, sectionProducts));
        }

        [HttpPost]
        public ActionResult Edit(SectionVM sectionVM)
        {
            if (ModelState.IsValid)
            {
                LicenseManager licenseManager = new LicenseManager(_context);
                License license = licenseManager.GetLicense(sectionVM.LicenseId);

                SectionManager sectionManager = new SectionManager(_context);

                foreach (var product in sectionVM.Products)
                {
                    if (product.Selected && !product.PreSelected)
                    {
                        sectionManager.AddSection(license, product.SectionProductId);
                    }
                    else if (product.PreSelected && !product.Selected)
                    {
                        sectionManager.DeleteSection(license, product.SectionProductId);
                    }
                }

                sectionManager.Confirm(license);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditSections", sectionVM);
            }
        }
    }
}