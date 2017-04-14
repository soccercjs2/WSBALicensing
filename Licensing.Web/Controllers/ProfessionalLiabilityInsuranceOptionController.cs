using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.ProfessionalLiabilityInsurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class ProfessionalLiabilityInsuranceOptionController : Controller
    {
        LicensingContext _context;

        public ProfessionalLiabilityInsuranceOptionController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Options()
        {
            ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
            ProfessionalLiabilityInsuranceOptionsVM professionalLiabilityInsuranceOptionsVM = new ProfessionalLiabilityInsuranceOptionsVM();
            professionalLiabilityInsuranceOptionsVM.Options = professionalLiabilityInsuranceManager.GetOptions();

            return View("~/Views/ProfessionalLiabilityInsurance/EditProfessionalLiabilityInsuranceOptions.cshtml", professionalLiabilityInsuranceOptionsVM);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
            ProfessionalLiabilityInsuranceOption option;

            if (id == null)
            {
                option = new ProfessionalLiabilityInsuranceOption();
            }
            else
            {
                option = professionalLiabilityInsuranceManager.GetOption((int)id);
            }

            return View("~/Views/ProfessionalLiabilityInsurance/EditProfessionalLiabilityInsuranceOption.cshtml", new ProfessionalLiabilityInsuranceOptionVM(option));
        }

        [HttpPost]
        public ActionResult Edit(ProfessionalLiabilityInsuranceOptionVM professionalLiabilityInsuranceOptionVM)
        {
            if (ModelState.IsValid)
            {
                //instantiate nullable bools
                bool? privatePractice;
                bool? currentlyInsured;
                bool? maintainCoverage;

                //convert private practice radio button values to nullable bool values
                if (professionalLiabilityInsuranceOptionVM.PrivatePractice == 1) { privatePractice = true; }
                else if (professionalLiabilityInsuranceOptionVM.PrivatePractice == 0) { privatePractice = false; }
                else { privatePractice = null; }

                //convert currently insured radio button values to nullable bool values
                if (professionalLiabilityInsuranceOptionVM.CurrentlyInsured == 1) { currentlyInsured = true; }
                else if (professionalLiabilityInsuranceOptionVM.CurrentlyInsured == 0) { currentlyInsured = false; }
                else { currentlyInsured = null; }

                //convert maintain coverage radio button values to nullable bool values
                if (professionalLiabilityInsuranceOptionVM.MaintainCoverage == 1) { maintainCoverage = true; }
                else if (professionalLiabilityInsuranceOptionVM.MaintainCoverage == 0) { maintainCoverage = false; }
                else { maintainCoverage = null; }

                //get option to edit
                ProfessionalLiabilityInsuranceManager professionalLiabilityInsuranceManager = new ProfessionalLiabilityInsuranceManager(_context);
                ProfessionalLiabilityInsuranceOption option = professionalLiabilityInsuranceManager.GetOption(professionalLiabilityInsuranceOptionVM.ProfessionalLiabilityInsuranceOptionId);

                //create new option if option doesn't already exist
                if (option == null) { option = new ProfessionalLiabilityInsuranceOption(); }

                //update option details from view
                option.Description = professionalLiabilityInsuranceOptionVM.Description;
                option.PrivatePractice = privatePractice;
                option.CurrentlyInsured = currentlyInsured;
                option.MaintainCoverage = maintainCoverage;

                //update option
                professionalLiabilityInsuranceManager.SetOption(option);

                return RedirectToAction("Options", "ProfessionalLiabilityInsuranceOption");
            }
            else
            {
                return View("~/Views/ProfessionalLiabilityInsurance/EditProfessionalLiabilityInsuranceOption.cshtml", professionalLiabilityInsuranceOptionVM);
            }
        }
    }
}