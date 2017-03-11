using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.PracticeAreas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class PracticeAreaOptionController : Controller
    {
        LicensingContext _context;

        public PracticeAreaOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            PracticeAreaManager practiceAreaManager = new PracticeAreaManager(_context);
            ICollection<PracticeAreaOption> codes = practiceAreaManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<PracticeAreaOption> amsCodes = practiceAreaManager.GetAmsOptions();

            PracticeAreaOptionsVM practiceAreaOptionsVM = new PracticeAreaOptionsVM();
            practiceAreaOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            practiceAreaOptionsVM.CodesToBeAdded = practiceAreaManager.GetCodesToBeAdded(codes, amsCodes);
            practiceAreaOptionsVM.CodesToBeActivated = practiceAreaManager.GetCodesToBeActivated(codes, amsCodes);
            practiceAreaOptionsVM.CodesToBeChanged = practiceAreaManager.GetCodesToBeChanged(codes, amsCodes);
            practiceAreaOptionsVM.CodesToBeDeactivated = practiceAreaManager.GetCodesToBeDeactivated(codes, amsCodes);
            practiceAreaOptionsVM.CodesToBeDeleted = practiceAreaManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/PracticeAreas/EditPracticeAreaOptions.cshtml", practiceAreaOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(PracticeAreaOptionsVM practiceAreaOptionsVM)
        {
            if (ModelState.IsValid)
            {
                PracticeAreaManager practiceAreaManager = new PracticeAreaManager(_context);

                if (practiceAreaOptionsVM.CodesToBeAdded != null)
                {
                    foreach (PracticeAreaOption option in practiceAreaOptionsVM.CodesToBeAdded)
                    {
                        practiceAreaManager.SetOption(option);
                    }
                }

                if (practiceAreaOptionsVM.CodesToBeActivated != null)
                {
                    foreach (PracticeAreaOption option in practiceAreaOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        practiceAreaManager.SetOption(option);
                    }
                }

                if (practiceAreaOptionsVM.CodesToBeChanged != null)
                {
                    foreach (PracticeAreaOption option in practiceAreaOptionsVM.CodesToBeChanged)
                    {
                        PracticeAreaOption codeToChange = practiceAreaManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        practiceAreaManager.SetOption(codeToChange);
                    }
                }

                if (practiceAreaOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (PracticeAreaOption option in practiceAreaOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        practiceAreaManager.SetOption(option);
                    }
                }

                if (practiceAreaOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (PracticeAreaOption option in practiceAreaOptionsVM.CodesToBeDeleted)
                    {
                        practiceAreaManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "PracticeAreaOption");
            }
            else
            {
                return View("~/Views/PracticeAreas/EditPracticeAreaOptions.cshtml", practiceAreaOptionsVM);
            }
        }
    }
}