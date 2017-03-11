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
    public class AddressTypeController : Controller
    {
        LicensingContext _context;

        public AddressTypeController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            AddressManager addressManager = new AddressManager(_context);
            ICollection<AddressType> codes = addressManager.GetAddressTypes().OrderBy(c => c.Name).ToList(); ;
            ICollection<AddressType> amsCodes = addressManager.GetAmsOptions();

            AddressTypesVM addressTypesVM = new AddressTypesVM();
            addressTypesVM.Codes = codes.Where(c => c.Active).ToList();
            addressTypesVM.CodesToBeAdded = addressManager.GetCodesToBeAdded(codes, amsCodes);
            addressTypesVM.CodesToBeActivated = addressManager.GetCodesToBeActivated(codes, amsCodes);
            addressTypesVM.CodesToBeChanged = addressManager.GetCodesToBeChanged(codes, amsCodes);
            addressTypesVM.CodesToBeDeactivated = addressManager.GetCodesToBeDeactivated(codes, amsCodes);
            addressTypesVM.CodesToBeDeleted = addressManager.GetCodesToBeDeleted(codes, amsCodes);

            return View("~/Views/Address/EditAddressTypes.cshtml", addressTypesVM);
        }

        [HttpPost]
        public ActionResult Edit(AddressTypesVM addressTypesVM)
        {
            if (ModelState.IsValid)
            {
                AddressManager addressManager = new AddressManager(_context);

                if (addressTypesVM.CodesToBeAdded != null)
                {
                    foreach (AddressType option in addressTypesVM.CodesToBeAdded)
                    {
                        addressManager.SetOption(option);
                    }
                }

                if (addressTypesVM.CodesToBeActivated != null)
                {
                    foreach (AddressType option in addressTypesVM.CodesToBeActivated)
                    {
                        option.Active = true;
                        addressManager.SetOption(option);
                    }
                }

                if (addressTypesVM.CodesToBeChanged != null)
                {
                    foreach (AddressType option in addressTypesVM.CodesToBeChanged)
                    {
                        AddressType codeToChange = addressManager.GetAddressType(option.AmsCode);
                        codeToChange.Name = option.Name;
                        addressManager.SetOption(codeToChange);
                    }
                }

                if (addressTypesVM.CodesToBeDeactivated != null)
                {
                    foreach (AddressType option in addressTypesVM.CodesToBeDeactivated)
                    {
                        option.Active = false;
                        addressManager.SetOption(option);
                    }
                }

                if (addressTypesVM.CodesToBeDeleted != null)
                {
                    foreach (AddressType option in addressTypesVM.CodesToBeDeleted)
                    {
                        addressManager.DeleteOption(option);
                    }
                }

                return RedirectToAction("Edit", "AddressType");
            }
            else
            {
                return View("~/Views/Address/EditAddressTypes.cshtml", addressTypesVM);
            }
        }
    }
}