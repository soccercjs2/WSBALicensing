using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class SexualOrientationOptionController : Controller
    {
        LicensingContext _context;

        public SexualOrientationOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);
            ICollection<SexualOrientationOption> codes = sexualOrientationManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<SexualOrientationOption> amsCodes = sexualOrientationManager.GetAmsOptions();

            SexualOrientationOptionsVM sexualOrientationOptionsVM = new SexualOrientationOptionsVM();
            sexualOrientationOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            sexualOrientationOptionsVM.CodesToBeAdded = sexualOrientationManager.GetCodesToBeAdded(codes, amsCodes);
            sexualOrientationOptionsVM.CodesToBeActivated = sexualOrientationManager.GetCodesToBeActivated(codes, amsCodes);
            sexualOrientationOptionsVM.CodesToBeChanged = sexualOrientationManager.GetCodesToBeChanged(codes, amsCodes);
            sexualOrientationOptionsVM.CodesToBeDeactivated = sexualOrientationManager.GetCodesToBeDeactivated(codes, amsCodes);
            sexualOrientationOptionsVM.CodesToBeDeleted = sexualOrientationManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/SexualOrientation/EditSexualOrientationOptions.cshtml", sexualOrientationOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(SexualOrientationOptionsVM sexualOrientationOptionsVM)
        {
            if (ModelState.IsValid)
            {
                SexualOrientationManager sexualOrientationManager = new SexualOrientationManager(_context);

                if (sexualOrientationOptionsVM.CodesToBeAdded != null)
                {
                    foreach (SexualOrientationOption option in sexualOrientationOptionsVM.CodesToBeAdded)
                    {
                        sexualOrientationManager.SetOption(option);
                    }
                }

                if (sexualOrientationOptionsVM.CodesToBeActivated != null)
                {
                    foreach (SexualOrientationOption option in sexualOrientationOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        sexualOrientationManager.SetOption(option);
                    }
                }

                if (sexualOrientationOptionsVM.CodesToBeChanged != null)
                {
                    foreach (SexualOrientationOption option in sexualOrientationOptionsVM.CodesToBeChanged)
                    {
                        SexualOrientationOption codeToChange = sexualOrientationManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        sexualOrientationManager.SetOption(codeToChange);
                    }
                }

                if (sexualOrientationOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (SexualOrientationOption option in sexualOrientationOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        sexualOrientationManager.SetOption(option);
                    }
                }

                if (sexualOrientationOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (SexualOrientationOption option in sexualOrientationOptionsVM.CodesToBeDeleted)
                    {
                        sexualOrientationManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "SexualOrientationOption");
            }
            else
            {
                return View("~/Views/SexualOrientation/EditSexualOrientationOptions.cshtml", sexualOrientationOptionsVM);
            }
        }
    }
}