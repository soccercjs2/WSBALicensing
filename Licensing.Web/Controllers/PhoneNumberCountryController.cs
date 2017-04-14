using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.ContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class PhoneNumberCountryController : Controller
    {
        LicensingContext _context;

        public PhoneNumberCountryController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            ICollection<PhoneNumberCountry> codes = phoneNumberManager.GetCountries().OrderBy(c => c.Name).ToList(); ;
            ICollection<PhoneNumberCountry> amsCodes = phoneNumberManager.GetAmsPhoneNumberCountries();

            PhoneNumberCountriesVM phoneNumberCountriesVM = new PhoneNumberCountriesVM();
            phoneNumberCountriesVM.Codes = codes.Where(c => c.Active).ToList();
            phoneNumberCountriesVM.CodesToBeAdded = phoneNumberManager.GetCountriesToBeAdded(codes, amsCodes);
            phoneNumberCountriesVM.CodesToBeActivated = phoneNumberManager.GetCountriesToBeActivated(codes, amsCodes);
            phoneNumberCountriesVM.CodesToBeChanged = phoneNumberManager.GetCountriesToBeChanged(codes, amsCodes);
            phoneNumberCountriesVM.CodesToBeDeactivated = phoneNumberManager.GetCountriesToBeDeactivated(codes, amsCodes);
            phoneNumberCountriesVM.CodesToBeDeleted = phoneNumberManager.GetCountriesToBeDeleted(codes, amsCodes);

            return View("~/Views/PhoneNumber/EditPhoneNumberCountries.cshtml", phoneNumberCountriesVM);
        }

        [HttpPost]
        public ActionResult Edit(PhoneNumberCountriesVM phoneNumberCountriesVM)
        {
            if (ModelState.IsValid)
            {
                PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);

                if (phoneNumberCountriesVM.CodesToBeAdded != null)
                {
                    foreach (PhoneNumberCountry option in phoneNumberCountriesVM.CodesToBeAdded)
                    {
                        phoneNumberManager.SetCountry(option);
                    }
                }

                if (phoneNumberCountriesVM.CodesToBeActivated != null)
                {
                    foreach (PhoneNumberCountry option in phoneNumberCountriesVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        phoneNumberManager.SetCountry(option);
                    }
                }

                if (phoneNumberCountriesVM.CodesToBeChanged != null)
                {
                    foreach (PhoneNumberCountry option in phoneNumberCountriesVM.CodesToBeChanged)
                    {
                        PhoneNumberCountry codeToChange = phoneNumberManager.GetCountry(option.CountryCode);
                        codeToChange.Name = option.Name;
                        phoneNumberManager.SetCountry(option);
                    }
                }

                if (phoneNumberCountriesVM.CodesToBeDeactivated != null)
                {
                    foreach (PhoneNumberCountry option in phoneNumberCountriesVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        phoneNumberManager.SetCountry(option);
                    }
                }

                if (phoneNumberCountriesVM.CodesToBeDeleted != null)
                {
                    foreach (PhoneNumberCountry option in phoneNumberCountriesVM.CodesToBeDeleted)
                    {
                        phoneNumberManager.DeleteCountry(option);
                    }
                }

                return RedirectToAction("Edit", "PhoneNumberCountry");
            }
            else
            {
                return View("~/Views/PhoneNumber/EditPhoneNumberCountries.cshtml", phoneNumberCountriesVM);
            }
        }
    }
}