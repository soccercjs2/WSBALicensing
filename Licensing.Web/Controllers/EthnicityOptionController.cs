using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Ethnicities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class EthnicityOptionController : Controller
    {
        LicensingContext _context;

        public EthnicityOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            EthnicityManager ethnicityManager = new EthnicityManager(_context);
            ICollection<EthnicityOption> codes = ethnicityManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<EthnicityOption> amsCodes = ethnicityManager.GetAmsOptions();

            EthnicityOptionsVM ethnicityOptionsVM = new EthnicityOptionsVM();
            ethnicityOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            ethnicityOptionsVM.CodesToBeAdded = ethnicityManager.GetCodesToBeAdded(codes, amsCodes);
            ethnicityOptionsVM.CodesToBeActivated = ethnicityManager.GetCodesToBeActivated(codes, amsCodes);
            ethnicityOptionsVM.CodesToBeChanged = ethnicityManager.GetCodesToBeChanged(codes, amsCodes);
            ethnicityOptionsVM.CodesToBeDeactivated = ethnicityManager.GetCodesToBeDeactivated(codes, amsCodes);
            ethnicityOptionsVM.CodesToBeDeleted = ethnicityManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/Ethnicity/EditEthnicityOptions.cshtml", ethnicityOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(EthnicityOptionsVM ethnicityOptionsVM)
        {
            if (ModelState.IsValid)
            {
                EthnicityManager ethnicityManager = new EthnicityManager(_context);

                if (ethnicityOptionsVM.CodesToBeAdded != null)
                {
                    foreach (EthnicityOption option in ethnicityOptionsVM.CodesToBeAdded)
                    {
                        ethnicityManager.SetOption(option);
                    }
                }

                if (ethnicityOptionsVM.CodesToBeActivated != null)
                {
                    foreach (EthnicityOption option in ethnicityOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        ethnicityManager.SetOption(option);
                    }
                }

                if (ethnicityOptionsVM.CodesToBeChanged != null)
                {
                    foreach (EthnicityOption option in ethnicityOptionsVM.CodesToBeChanged)
                    {
                        EthnicityOption codeToChange = ethnicityManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        ethnicityManager.SetOption(codeToChange);
                    }
                }

                if (ethnicityOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (EthnicityOption option in ethnicityOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        ethnicityManager.SetOption(option);
                    }
                }

                if (ethnicityOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (EthnicityOption option in ethnicityOptionsVM.CodesToBeDeleted)
                    {
                        ethnicityManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "EthnicityOption");
            }
            else
            {
                return View("~/Views/Ethnicity/EditEthnicityOptions.cshtml", ethnicityOptionsVM);
            }
        }
    }
}