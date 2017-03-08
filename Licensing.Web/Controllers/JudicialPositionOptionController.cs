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
            ICollection<JudicialPositionOption> codes = judicialPositionManager.GetOptions();

            JudicialPositionOptionsVM judicialPositionOptionVM = new JudicialPositionOptionsVM();
            judicialPositionOptionVM.ActiveCodes = codes.Where(c => c.Active).ToList();
            judicialPositionOptionVM.InactiveCodes = codes.Where(c => !c.Active).ToList();
            judicialPositionOptionVM.PersonifyCodes = judicialPositionManager.GetAmsJudicialPositionOptions();

            return View("~/Views/JudicialPosition/EditJudicialPositionOptions.cshtml", judicialPositionOptionVM);
        }

        [HttpPost]
        public ActionResult Edit(JudicialPositionOptionsVM judicialPositionOptionVM)
        {
            if (ModelState.IsValid)
            {
                JudicialPositionManager judicialPositionManager = new JudicialPositionManager(_context);

                if (judicialPositionOptionVM.ActiveCodes != null)
                {
                    foreach (JudicialPositionOption option in judicialPositionOptionVM.ActiveCodes)
                    {
                        judicialPositionManager.SetJudicialPositionOption(option);
                    }
                }

                if (judicialPositionOptionVM.InactiveCodes != null)
                {
                    foreach (JudicialPositionOption option in judicialPositionOptionVM.InactiveCodes)
                    {
                        judicialPositionManager.SetJudicialPositionOption(option);
                    }
                }

                if (judicialPositionOptionVM.PersonifyCodes != null)
                {
                    ICollection<JudicialPositionOption> optionsToInclude = judicialPositionOptionVM.PersonifyCodes.Where(o => o.Active).ToList();

                    foreach (JudicialPositionOption option in optionsToInclude)
                    {
                        judicialPositionManager.SetJudicialPositionOption(option);
                    }
                }

                return RedirectToAction("Edit", "JudicialPositionOption");
            }
            else
            {
                return View("~/Views/JudicialPosition/EditJudicialPositionOptions.cshtml", judicialPositionOptionVM);
            }
        }
    }
}