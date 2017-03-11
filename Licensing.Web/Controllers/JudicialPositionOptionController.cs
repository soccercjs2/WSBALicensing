using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Judicial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class JudicialPositionOptionController : Controller
    {
        LicensingContext _context;

        public JudicialPositionOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);
            ICollection<JudicialPositionOption> codes = judicialPositionManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<JudicialPositionOption> amsCodes = judicialPositionManager.GetAmsOptions();

            JudicialPositionOptionsVM judicialPositionOptionsVM = new JudicialPositionOptionsVM();
            judicialPositionOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            judicialPositionOptionsVM.CodesToBeAdded = judicialPositionManager.GetCodesToBeAdded(codes, amsCodes);
            judicialPositionOptionsVM.CodesToBeActivated = judicialPositionManager.GetCodesToBeActivated(codes, amsCodes);
            judicialPositionOptionsVM.CodesToBeChanged = judicialPositionManager.GetCodesToBeChanged(codes, amsCodes);
            judicialPositionOptionsVM.CodesToBeDeactivated = judicialPositionManager.GetCodesToBeDeactivated(codes, amsCodes);
            judicialPositionOptionsVM.CodesToBeDeleted = judicialPositionManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/JudicialPosition/EditJudicialPositionOptions.cshtml", judicialPositionOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(JudicialPositionOptionsVM judicialPositionOptionsVM)
        {
            if (ModelState.IsValid)
            {
                JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);

                if (judicialPositionOptionsVM.CodesToBeAdded != null)
                {
                    foreach (JudicialPositionOption option in judicialPositionOptionsVM.CodesToBeAdded)
                    {
                        judicialPositionManager.SetOption(option);
                    }
                }

                if (judicialPositionOptionsVM.CodesToBeActivated != null)
                {
                    foreach (JudicialPositionOption option in judicialPositionOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        judicialPositionManager.SetOption(option);
                    }
                }

                if (judicialPositionOptionsVM.CodesToBeChanged != null)
                {
                    foreach (JudicialPositionOption option in judicialPositionOptionsVM.CodesToBeChanged)
                    {
                        JudicialPositionOption codeToChange = judicialPositionManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        judicialPositionManager.SetOption(codeToChange);
                    }
                }

                if (judicialPositionOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (JudicialPositionOption option in judicialPositionOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        judicialPositionManager.SetOption(option);
                    }
                }

                if (judicialPositionOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (JudicialPositionOption option in judicialPositionOptionsVM.CodesToBeDeleted)
                    {
                        judicialPositionManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "JudicialPositionOption");
            }
            else
            {
                return View("~/Views/JudicialPosition/EditJudicialPositionOptions.cshtml", judicialPositionOptionsVM);
            }
        }
    }
}