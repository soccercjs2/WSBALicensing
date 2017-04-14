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
    public class PhoneNumberTypeController : Controller
    {
        LicensingContext _context;

        public PhoneNumberTypeController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            PhoneNumberTypesVM phoneNumberTypesVM = new PhoneNumberTypesVM();
            phoneNumberTypesVM.Types = phoneNumberManager.GetPhoneNumberTypes();

            return View("~/Views/PhoneNumber/EditPhoneNumberTypes.cshtml", phoneNumberTypesVM);
        }

        [HttpPost]
        public ActionResult Edit(PhoneNumberTypesVM phoneNumberTypesVM)
        {
            if (ModelState.IsValid)
            {
                PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);

                PhoneNumberType type = new PhoneNumberType();
                type.Active = true;
                type.Name = phoneNumberTypesVM.Name;

                phoneNumberManager.SetType(type);

                ModelState.Clear();

                return RedirectToAction("Edit", "PhoneNumberType");
            }
            else
            {
                return View("~/Views/PhoneNumber/EditPhoneNumberTypes.cshtml", phoneNumberTypesVM);
            }
        }
    }
}