using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.FinancialResponsibilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class CoveredByOptionController : Controller
    {
        LicensingContext _context;

        public CoveredByOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);
            ICollection<CoveredByOption> codes = financialResponsibilityManager.GetOptions().OrderBy(c => c.Name).ToList(); ;
            ICollection<CoveredByOption> amsCodes = financialResponsibilityManager.GetAmsOptions();

            CoveredByOptionsVM coveredByOptionsVM = new CoveredByOptionsVM();
            coveredByOptionsVM.Codes = codes.Where(c => c.Active).ToList();
            coveredByOptionsVM.CodesToBeAdded = financialResponsibilityManager.GetCodesToBeAdded(codes, amsCodes);
            coveredByOptionsVM.CodesToBeActivated = financialResponsibilityManager.GetCodesToBeActivated(codes, amsCodes);
            coveredByOptionsVM.CodesToBeChanged = financialResponsibilityManager.GetCodesToBeChanged(codes, amsCodes);
            coveredByOptionsVM.CodesToBeDeactivated = financialResponsibilityManager.GetCodesToBeDeactivated(codes, amsCodes);
            coveredByOptionsVM.CodesToBeDeleted = financialResponsibilityManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/FinancialResponsibility/EditCoveredByOptions.cshtml", coveredByOptionsVM);
        }

        [HttpPost]
        public ActionResult Edit(CoveredByOptionsVM coveredByOptionsVM)
        {
            if (ModelState.IsValid)
            {
                FinancialResponsibilityManager financialResponsibilityManager = new FinancialResponsibilityManager(_context);

                if (coveredByOptionsVM.CodesToBeAdded != null)
                {
                    foreach (CoveredByOption option in coveredByOptionsVM.CodesToBeAdded)
                    {
                        financialResponsibilityManager.SetOption(option);
                    }
                }

                if (coveredByOptionsVM.CodesToBeActivated != null)
                {
                    foreach (CoveredByOption option in coveredByOptionsVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        financialResponsibilityManager.SetOption(option);
                    }
                }

                if (coveredByOptionsVM.CodesToBeChanged != null)
                {
                    foreach (CoveredByOption option in coveredByOptionsVM.CodesToBeChanged)
                    {
                        CoveredByOption codeToChange = financialResponsibilityManager.GetOption(option.AmsCode);
                        codeToChange.Name = option.Name;
                        financialResponsibilityManager.SetOption(codeToChange);
                    }
                }

                if (coveredByOptionsVM.CodesToBeDeactivated != null)
                {
                    foreach (CoveredByOption option in coveredByOptionsVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        financialResponsibilityManager.SetOption(option);
                    }
                }

                if (coveredByOptionsVM.CodesToBeDeleted != null)
                {
                    foreach (CoveredByOption option in coveredByOptionsVM.CodesToBeDeleted)
                    {
                        financialResponsibilityManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "CoveredByOption");
            }
            else
            {
                return View("~/Views/FinancialResponsibility/EditCoveredByOptions.cshtml", coveredByOptionsVM);
            }
        }
    }
}