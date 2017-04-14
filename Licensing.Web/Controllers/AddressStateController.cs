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
    public class AddressStateController : Controller
    {
        LicensingContext _context;

        public AddressStateController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            AddressManager addressManager = new AddressManager(_context);
            ICollection<AddressState> codes = addressManager.GetAddressStates().OrderBy(c => c.Name).ToList(); ;
            ICollection<AddressState> amsCodes = addressManager.GetAmsAddressStates();

            AddressStatesVM addressStatesVM = new AddressStatesVM();
            addressStatesVM.Codes = codes.Where(c => c.Active).ToList();
            addressStatesVM.CodesToBeAdded = addressManager.GetAddressStatesToBeAdded(codes, amsCodes);
            addressStatesVM.CodesToBeActivated = addressManager.GetAddressStatesToBeActivated(codes, amsCodes);
            addressStatesVM.CodesToBeChanged = addressManager.GetAddressStatesToBeChanged(codes, amsCodes);
            addressStatesVM.CodesToBeDeactivated = addressManager.GetAddressStatesToBeDeactivated(codes, amsCodes);
            addressStatesVM.CodesToBeDeleted = addressManager.GetAddressStatesToBeDeleted(codes, amsCodes);

            return View("~/Views/Address/EditAddressStates.cshtml", addressStatesVM);
        }

        [HttpPost]
        public ActionResult Edit(AddressStatesVM addressStatesVM)
        {
            if (ModelState.IsValid)
            {
                AddressManager addressManager = new AddressManager(_context);

                if (addressStatesVM.CodesToBeAdded != null)
                {
                    foreach (AddressState option in addressStatesVM.CodesToBeAdded)
                    {
                        addressManager.SetAddressState(option);
                    }
                }

                if (addressStatesVM.CodesToBeActivated != null)
                {
                    foreach (AddressState option in addressStatesVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        addressManager.SetAddressState(option);
                    }
                }

                if (addressStatesVM.CodesToBeChanged != null)
                {
                    foreach (AddressState option in addressStatesVM.CodesToBeChanged)
                    {
                        AddressState codeToChange = addressManager.GetAddressState(option.AmsCountryCode, option.AmsCode);
                        codeToChange.Name = option.Name;
                        addressManager.SetAddressState(codeToChange);
                    }
                }

                if (addressStatesVM.CodesToBeDeactivated != null)
                {
                    foreach (AddressState option in addressStatesVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        addressManager.SetAddressState(option);
                    }
                }

                if (addressStatesVM.CodesToBeDeleted != null)
                {
                    foreach (AddressState option in addressStatesVM.CodesToBeDeleted)
                    {
                        addressManager.DeleteAddressState(option);
                    }
                }

                return RedirectToAction("Edit", "AddressState");
            }
            else
            {
                return View("~/Views/Address/EditAddressStates.cshtml", addressStatesVM);
            }
        }
    }
}