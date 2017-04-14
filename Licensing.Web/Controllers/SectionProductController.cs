using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class SectionProductController : Controller
    {
        LicensingContext _context;

        public SectionProductController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            SectionManager sectionManager = new SectionManager(_context);
            ICollection<SectionProduct> codes = sectionManager.GetProducts().OrderBy(c => c.Name).ToList(); ;
            ICollection<SectionProduct> amsCodes = sectionManager.GetAmsOptions();

            SectionProductsVM sectionProductsVM = new SectionProductsVM();
            sectionProductsVM.Codes = codes.Where(c => c.Active).ToList();
            sectionProductsVM.CodesToBeAdded = sectionManager.GetCodesToBeAdded(codes, amsCodes);
            sectionProductsVM.CodesToBeActivated = sectionManager.GetCodesToBeActivated(codes, amsCodes);
            sectionProductsVM.CodesToBeChanged = sectionManager.GetCodesToBeChanged(codes, amsCodes);
            sectionProductsVM.CodesToBeDeactivated = sectionManager.GetCodesToBeDeactivated(codes, amsCodes);
            sectionProductsVM.CodesToBeDeleted = sectionManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/Sections/EditSectionProducts.cshtml", sectionProductsVM);
        }

        [HttpPost]
        public ActionResult Edit(SectionProductsVM sectionProductsVM)
        {
            if (ModelState.IsValid)
            {
                SectionManager sectionManager = new SectionManager(_context);

                if (sectionProductsVM.CodesToBeAdded != null)
                {
                    foreach (SectionProduct option in sectionProductsVM.CodesToBeAdded)
                    {
                        sectionManager.SetOption(option);
                    }
                }

                if (sectionProductsVM.CodesToBeActivated != null)
                {
                    foreach (SectionProduct option in sectionProductsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        sectionManager.SetOption(option);
                    }
                }

                if (sectionProductsVM.CodesToBeChanged != null)
                {
                    foreach (SectionProduct option in sectionProductsVM.CodesToBeChanged)
                    {
                        SectionProduct codeToChange = sectionManager.GetProduct(option.AmsCode);
                        codeToChange.Name = option.Name;
                        sectionManager.SetOption(codeToChange);
                    }
                }

                if (sectionProductsVM.CodesToBeDeactivated != null)
                {
                    foreach (SectionProduct option in sectionProductsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        sectionManager.SetOption(option);
                    }
                }

                if (sectionProductsVM.CodesToBeDeleted != null)
                {
                    foreach (SectionProduct option in sectionProductsVM.CodesToBeDeleted)
                    {
                        sectionManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "SectionProduct");
            }
            else
            {
                return View("~/Views/Sections/EditSectionProducts.cshtml", sectionProductsVM);
            }
        }
    }
}