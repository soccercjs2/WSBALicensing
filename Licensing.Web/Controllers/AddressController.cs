using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Domain.Addresses;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class AddressController : Controller
    {
        LicensingContext _context;

        public AddressController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {           
            //confirm the preloaded address
            AddressManager addressManager = new AddressManager(_context);
            addressManager.Confirm(addressManager.GetAddress(id));

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get address to edit
            AddressManager addressManager = new AddressManager(_context);
            Address address = addressManager.GetAddress(id);
            
            return View("EditAddress", new AddressVM(address, addressManager.GetAddressCountries(), addressManager.GetAddressStates(address.Country.AmsCode), address.Country.AddressCountryId));
        }

        [HttpPost]
        public ActionResult Edit(AddressVM addressVM)
        {
            if (ModelState.IsValid)
            {
                AddressManager addressManager = new AddressManager(_context);

                if (addressVM.AddressCountryIdStatesLoadedFor != addressVM.Address.AddressCountryId)
                {
                    AddressCountry country = addressManager.GetAddressCountry((int)addressVM.Address.AddressCountryId);

                    addressVM.Countries = addressManager.GetAddressCountries();
                    addressVM.States = addressManager.GetAddressStates(country.AmsCode);
                    addressVM.AddressCountryIdStatesLoadedFor = (int)addressVM.Address.AddressCountryId;

                    ModelState.Clear();

                    return View("EditAddress", addressVM);
                }

                addressManager.SetAddress(addressVM.Address);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditAddress", addressVM);
            }
        }

        [HttpGet]
        public ActionResult CreatePrimary(int id)
        {
            AddressManager addressManager = new AddressManager(_context);
            Address address = new Address();
            address.LicenseId = id;
            address.AddressType = addressManager.GetAddressType("OFFICE");

            AddressCountry defaultCountry = addressManager.GetAddressCountry("USA");
            address.AddressCountryId = defaultCountry.AddressCountryId;

            return View("EditAddress", new AddressVM(address, addressManager.GetAddressCountries(), addressManager.GetAddressStates(defaultCountry.AmsCode), defaultCountry.AddressCountryId));
        }

        [HttpGet]
        public ActionResult CreateHome(int id)
        {
            AddressManager addressManager = new AddressManager(_context);
            Address address = new Address();
            address.LicenseId = id;
            address.AddressType = addressManager.GetAddressType("HOME");

            AddressCountry defaultCountry = addressManager.GetAddressCountry("USA");
            address.AddressCountryId = defaultCountry.AddressCountryId;

            return View("EditAddress", new AddressVM(address, addressManager.GetAddressCountries(), addressManager.GetAddressStates(defaultCountry.AmsCode), defaultCountry.AddressCountryId));
        }

        [HttpGet]
        public ActionResult CreateAgentOfService(int id)
        {
            AddressManager addressManager = new AddressManager(_context);
            Address address = new Address();
            address.LicenseId = id;
            address.AddressType = addressManager.GetAddressType("AGENTOFSERVICE");

            AddressCountry defaultCountry = addressManager.GetAddressCountry("USA");
            address.AddressCountryId = defaultCountry.AddressCountryId;

            return View("EditAddress", new AddressVM(address, addressManager.GetAddressCountries(), addressManager.GetAddressStates(defaultCountry.AmsCode), defaultCountry.AddressCountryId));
        }
    }
}