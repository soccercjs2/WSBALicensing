using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Genders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class GenderOptionController : Controller
    {
        LicensingContext _context;

        public GenderOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            GenderManager genderManager = new GenderManager(_context);
            ICollection<GenderOption> codes = genderManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<GenderOption> amsCodes = genderManager.GetAmsOptions();

            GenderOptionsVM genderOptionsVM = new GenderOptionsVM();
            genderOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            genderOptionsVM.CodesToBeAdded = genderManager.GetCodesToBeAdded(codes, amsCodes);
            genderOptionsVM.CodesToBeActivated = genderManager.GetCodesToBeActivated(codes, amsCodes);
            genderOptionsVM.CodesToBeChanged = genderManager.GetCodesToBeChanged(codes, amsCodes);
            genderOptionsVM.CodesToBeDeactivated = genderManager.GetCodesToBeDeactivated(codes, amsCodes);
            genderOptionsVM.CodesToBeDeleted = genderManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/Gender/EditGenderOptions.cshtml", genderOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(GenderOptionsVM genderOptionsVM)
        {
            if (ModelState.IsValid)
            {
                GenderManager genderManager = new GenderManager(_context);

                if (genderOptionsVM.CodesToBeAdded != null)
                {
                    foreach (GenderOption option in genderOptionsVM.CodesToBeAdded)
                    {
                        genderManager.SetOption(option);
                    }
                }

                if (genderOptionsVM.CodesToBeActivated != null)
                {
                    foreach (GenderOption option in genderOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        genderManager.SetOption(option);
                    }
                }

                if (genderOptionsVM.CodesToBeChanged != null)
                {
                    foreach (GenderOption option in genderOptionsVM.CodesToBeChanged)
                    {
                        GenderOption codeToChange = genderManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        genderManager.SetOption(codeToChange);
                    }
                }

                if (genderOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (GenderOption option in genderOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        genderManager.SetOption(option);
                    }
                }

                if (genderOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (GenderOption option in genderOptionsVM.CodesToBeDeleted)
                    {
                        genderManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "GenderOption");
            }
            else
            {
                return View("~/Views/Gender/EditGenderOptions.cshtml", genderOptionsVM);
            }
        }
    }
}