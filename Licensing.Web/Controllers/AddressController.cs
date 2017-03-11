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
            
            return View("EditAddress", new AddressVM(address));
        }

        [HttpPost]
        public ActionResult Edit(AddressVM addressVM)
        {
            if (ModelState.IsValid)
            {
                AddressManager addressManager = new AddressManager(_context);
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

            return View("EditAddress", new AddressVM(address));
        }

        [HttpGet]
        public ActionResult CreateHome(int id)
        {
            AddressManager addressManager = new AddressManager(_context);
            Address address = new Address();
            address.LicenseId = id;
            address.AddressType = addressManager.GetAddressType("HOME");

            return View("EditAddress", new AddressVM(address));
        }

        [HttpGet]
        public ActionResult CreateAgentOfService(int id)
        {
            AddressManager addressManager = new AddressManager(_context);
            Address address = new Address();
            address.LicenseId = id;
            address.AddressType = addressManager.GetAddressType("AGENTOFSERVICE");

            return View("EditAddress", new AddressVM(address));
        }
    }
}