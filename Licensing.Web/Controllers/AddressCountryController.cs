using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class AddressCountryController : Controller
    {
        LicensingContext _context;

        public AddressCountryController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            AddressManager addressManager = new AddressManager(_context);
            ICollection<AddressCountry> codes = addressManager.GetAddressCountries().OrderBy(c => c.Name).ToList(); ;
            ICollection<AddressCountry> amsCodes = addressManager.GetAmsAddressCountries();

            AddressCountriesVM addressCountriesVM = new AddressCountriesVM();
            addressCountriesVM.Codes = codes.Where(c => c.Active).ToList();
            addressCountriesVM.CodesToBeAdded = addressManager.GetAddressCountriesToBeAdded(codes, amsCodes);
            addressCountriesVM.CodesToBeActivated = addressManager.GetAddressCountriesToBeActivated(codes, amsCodes);
            addressCountriesVM.CodesToBeChanged = addressManager.GetAddressCountriesToBeChanged(codes, amsCodes);
            addressCountriesVM.CodesToBeDeactivated = addressManager.GetAddressCountriesToBeDeactivated(codes, amsCodes);
            addressCountriesVM.CodesToBeDeleted = addressManager.GetAddressCountriesToBeDeleted(codes, amsCodes);

            return View("~/Views/Address/EditAddressCountries.cshtml", addressCountriesVM);
        }

        [HttpPost]
        public ActionResult Edit(AddressCountriesVM addressCountriesVM)
        {
            if (ModelState.IsValid)
            {
                AddressManager addressManager = new AddressManager(_context);

                if (addressCountriesVM.CodesToBeAdded != null)
                {
                    foreach (AddressCountry option in addressCountriesVM.CodesToBeAdded)
                    {
                        addressManager.SetAddressCountry(option);
                    }
                }

                if (addressCountriesVM.CodesToBeActivated != null)
                {
                    foreach (AddressCountry option in addressCountriesVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        addressManager.SetAddressCountry(option);
                    }
                }

                if (addressCountriesVM.CodesToBeChanged != null)
                {
                    foreach (AddressCountry option in addressCountriesVM.CodesToBeChanged)
                    {
                        AddressCountry codeToChange = addressManager.GetAddressCountry(option.AmsCode);
                        codeToChange.Name = option.Name;
                        addressManager.SetAddressCountry(codeToChange);
                    }
                }

                if (addressCountriesVM.CodesToBeDeactivated != null)
                {
                    foreach (AddressCountry option in addressCountriesVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        addressManager.SetAddressCountry(option);
                    }
                }

                if (addressCountriesVM.CodesToBeDeleted != null)
                {
                    foreach (AddressCountry option in addressCountriesVM.CodesToBeDeleted)
                    {
                        addressManager.DeleteAddressCountry(option);
                    }
                }

                return RedirectToAction("Edit", "AddressCountry");
            }
            else
            {
                return View("~/Views/Address/EditAddressCountries.cshtml", addressCountriesVM);
            }
        }
    }
}