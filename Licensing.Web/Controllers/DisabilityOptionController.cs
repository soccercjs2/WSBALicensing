using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Disabilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class DisabilityOptionController : Controller
    {
        LicensingContext _context;

        public DisabilityOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            DisabilityManager disabilityManager = new DisabilityManager(_context);
            ICollection<DisabilityOption> codes = disabilityManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<DisabilityOption> amsCodes = disabilityManager.GetAmsOptions();

            DisabilityOptionsVM disabilityOptionsVM = new DisabilityOptionsVM();
            disabilityOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            disabilityOptionsVM.CodesToBeAdded = disabilityManager.GetCodesToBeAdded(codes, amsCodes);
            disabilityOptionsVM.CodesToBeActivated = disabilityManager.GetCodesToBeActivated(codes, amsCodes);
            disabilityOptionsVM.CodesToBeChanged = disabilityManager.GetCodesToBeChanged(codes, amsCodes);
            disabilityOptionsVM.CodesToBeDeactivated = disabilityManager.GetCodesToBeDeactivated(codes, amsCodes);
            disabilityOptionsVM.CodesToBeDeleted = disabilityManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/Disability/EditDisabilityOptions.cshtml", disabilityOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(DisabilityOptionsVM disabilityOptionsVM)
        {
            if (ModelState.IsValid)
            {
                DisabilityManager disabilityManager = new DisabilityManager(_context);

                if (disabilityOptionsVM.CodesToBeAdded != null)
                {
                    foreach (DisabilityOption option in disabilityOptionsVM.CodesToBeAdded)
                    {
                        disabilityManager.SetOption(option);
                    }
                }

                if (disabilityOptionsVM.CodesToBeActivated != null)
                {
                    foreach (DisabilityOption option in disabilityOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        disabilityManager.SetOption(option);
                    }
                }

                if (disabilityOptionsVM.CodesToBeChanged != null)
                {
                    foreach (DisabilityOption option in disabilityOptionsVM.CodesToBeChanged)
                    {
                        DisabilityOption codeToChange = disabilityManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        disabilityManager.SetOption(codeToChange);
                    }
                }

                if (disabilityOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (DisabilityOption option in disabilityOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        disabilityManager.SetOption(option);
                    }
                }

                if (disabilityOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (DisabilityOption option in disabilityOptionsVM.CodesToBeDeleted)
                    {
                        disabilityManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "DisabilityOption");
            }
            else
            {
                return View("~/Views/Disability/EditDisabilityOptions.cshtml", disabilityOptionsVM);
            }
        }
    }
}