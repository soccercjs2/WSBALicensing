using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class LanguageOptionController : Controller
    {
        LicensingContext _context;

        public LanguageOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            LanguageManager languageManager = new LanguageManager(_context);
            ICollection<LanguageOption> codes = languageManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<LanguageOption> amsCodes = languageManager.GetAmsOptions();

            LanguageOptionsVM languageOptionsVM = new LanguageOptionsVM();
            languageOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            languageOptionsVM.CodesToBeAdded = languageManager.GetCodesToBeAdded(codes, amsCodes);
            languageOptionsVM.CodesToBeActivated = languageManager.GetCodesToBeActivated(codes, amsCodes);
            languageOptionsVM.CodesToBeChanged = languageManager.GetCodesToBeChanged(codes, amsCodes);
            languageOptionsVM.CodesToBeDeactivated = languageManager.GetCodesToBeDeactivated(codes, amsCodes);
            languageOptionsVM.CodesToBeDeleted = languageManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/Languages/EditLanguageOptions.cshtml", languageOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(LanguageOptionsVM languageOptionsVM)
        {
            if (ModelState.IsValid)
            {
                LanguageManager languageManager = new LanguageManager(_context);

                if (languageOptionsVM.CodesToBeAdded != null)
                {
                    foreach (LanguageOption option in languageOptionsVM.CodesToBeAdded)
                    {
                        languageManager.SetOption(option);
                    }
                }

                if (languageOptionsVM.CodesToBeActivated != null)
                {
                    foreach (LanguageOption option in languageOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        languageManager.SetOption(option);
                    }
                }

                if (languageOptionsVM.CodesToBeChanged != null)
                {
                    foreach (LanguageOption option in languageOptionsVM.CodesToBeChanged)
                    {
                        LanguageOption codeToChange = languageManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        languageManager.SetOption(codeToChange);
                    }
                }

                if (languageOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (LanguageOption option in languageOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        languageManager.SetOption(option);
                    }
                }

                if (languageOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (LanguageOption option in languageOptionsVM.CodesToBeDeleted)
                    {
                        languageManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "LanguageOption");
            }
            else
            {
                return View("~/Views/Languages/EditLanguageOptions.cshtml", languageOptionsVM);
            }
        }
    }
}