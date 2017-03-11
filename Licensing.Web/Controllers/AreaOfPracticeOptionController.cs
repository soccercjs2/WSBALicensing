using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.AreasOfPractice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class AreaOfPracticeOptionController : Controller
    {
        LicensingContext _context;

        public AreaOfPracticeOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);
            ICollection<AreaOfPracticeOption> codes = areaOfPracticeManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<AreaOfPracticeOption> amsCodes = areaOfPracticeManager.GetAmsOptions();

            AreaOfPracticeOptionsVM areaOfPracticeOptionsVM = new AreaOfPracticeOptionsVM();
            areaOfPracticeOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            areaOfPracticeOptionsVM.CodesToBeAdded = areaOfPracticeManager.GetCodesToBeAdded(codes, amsCodes);
            areaOfPracticeOptionsVM.CodesToBeActivated = areaOfPracticeManager.GetCodesToBeActivated(codes, amsCodes);
            areaOfPracticeOptionsVM.CodesToBeChanged = areaOfPracticeManager.GetCodesToBeChanged(codes, amsCodes);
            areaOfPracticeOptionsVM.CodesToBeDeactivated = areaOfPracticeManager.GetCodesToBeDeactivated(codes, amsCodes);
            areaOfPracticeOptionsVM.CodesToBeDeleted = areaOfPracticeManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/AreasOfPractice/EditAreaOfPracticeOptions.cshtml", areaOfPracticeOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(AreaOfPracticeOptionsVM areaOfPracticeOptionsVM)
        {
            if (ModelState.IsValid)
            {
                AreaOfPracticeManager areaOfPracticeManager = new AreaOfPracticeManager(_context);

                if (areaOfPracticeOptionsVM.CodesToBeAdded != null)
                {
                    foreach (AreaOfPracticeOption option in areaOfPracticeOptionsVM.CodesToBeAdded)
                    {
                        areaOfPracticeManager.SetOption(option);
                    }
                }

                if (areaOfPracticeOptionsVM.CodesToBeActivated != null)
                {
                    foreach (AreaOfPracticeOption option in areaOfPracticeOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        areaOfPracticeManager.SetOption(option);
                    }
                }

                if (areaOfPracticeOptionsVM.CodesToBeChanged != null)
                {
                    foreach (AreaOfPracticeOption option in areaOfPracticeOptionsVM.CodesToBeChanged)
                    {
                        AreaOfPracticeOption codeToChange = areaOfPracticeManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        areaOfPracticeManager.SetOption(codeToChange);
                    }
                }

                if (areaOfPracticeOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (AreaOfPracticeOption option in areaOfPracticeOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        areaOfPracticeManager.SetOption(option);
                    }
                }

                if (areaOfPracticeOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (AreaOfPracticeOption option in areaOfPracticeOptionsVM.CodesToBeDeleted)
                    {
                        areaOfPracticeManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "AreaOfPracticeOption");
            }
            else
            {
                return View("~/Views/AreasOfPractice/EditAreaOfPracticeOptions.cshtml", areaOfPracticeOptionsVM);
            }
        }
    }
}