using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.FirmSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class FirmSizeOptionController : Controller
    {
        LicensingContext _context;

        public FirmSizeOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            FirmSizeManager firmSizeManager = new FirmSizeManager(_context);
            ICollection<FirmSizeOption> codes = firmSizeManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<FirmSizeOption> amsCodes = firmSizeManager.GetAmsOptions();

            FirmSizeOptionsVM firmSizeOptionsVM = new FirmSizeOptionsVM();
            firmSizeOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            firmSizeOptionsVM.CodesToBeAdded = firmSizeManager.GetCodesToBeAdded(codes, amsCodes);
            firmSizeOptionsVM.CodesToBeActivated = firmSizeManager.GetCodesToBeActivated(codes, amsCodes);
            firmSizeOptionsVM.CodesToBeChanged = firmSizeManager.GetCodesToBeChanged(codes, amsCodes);
            firmSizeOptionsVM.CodesToBeDeactivated = firmSizeManager.GetCodesToBeDeactivated(codes, amsCodes);
            firmSizeOptionsVM.CodesToBeDeleted = firmSizeManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/FirmSize/EditFirmSizeOptions.cshtml", firmSizeOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(FirmSizeOptionsVM firmSizeOptionsVM)
        {
            if (ModelState.IsValid)
            {
                FirmSizeManager firmSizeManager = new FirmSizeManager(_context);

                if (firmSizeOptionsVM.CodesToBeAdded != null)
                {
                    foreach (FirmSizeOption option in firmSizeOptionsVM.CodesToBeAdded)
                    {
                        firmSizeManager.SetOption(option);
                    }
                }

                if (firmSizeOptionsVM.CodesToBeActivated != null)
                {
                    foreach (FirmSizeOption option in firmSizeOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        firmSizeManager.SetOption(option);
                    }
                }

                if (firmSizeOptionsVM.CodesToBeChanged != null)
                {
                    foreach (FirmSizeOption option in firmSizeOptionsVM.CodesToBeChanged)
                    {
                        FirmSizeOption codeToChange = firmSizeManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        firmSizeManager.SetOption(codeToChange);
                    }
                }

                if (firmSizeOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (FirmSizeOption option in firmSizeOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        firmSizeManager.SetOption(option);
                    }
                }

                if (firmSizeOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (FirmSizeOption option in firmSizeOptionsVM.CodesToBeDeleted)
                    {
                        firmSizeManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "FirmSizeOption");
            }
            else
            {
                return View("~/Views/FirmSize/EditFirmSizeOptions.cshtml", firmSizeOptionsVM);
            }
        }
    }
}