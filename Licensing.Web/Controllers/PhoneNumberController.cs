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
    public class PhoneNumberController : Controller
    {
        LicensingContext _context;

        public PhoneNumberController()
        {
            _context = new LicensingContext();
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            //confirm the preloaded Phone Number
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            phoneNumberManager.Confirm(phoneNumberManager.GetPhoneNumber(id));

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get address to edit
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            PhoneNumber phoneNumber = phoneNumberManager.GetPhoneNumber(id);

            return View("EditPhoneNumber", new PhoneNumberVM(phoneNumber));
        }

        [HttpPost]
        public ActionResult Edit(PhoneNumberVM phoneNumberVM)
        {
            if (ModelState.IsValid)
            {
                PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
                phoneNumberManager.SetPhoneNumber(phoneNumberVM.PhoneNumber);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("EditPhoneNumber", phoneNumberVM);
            }
        }

        [HttpGet]
        public ActionResult CreatePrimary(int id)
        {
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            PhoneNumber phoneNumber = new PhoneNumber();
            phoneNumber.LicenseId = id;
            phoneNumber.PhoneNumberType = phoneNumberManager.GetPhoneNumberType("Primary");

            return View("EditPhoneNumber", new PhoneNumberVM(phoneNumber));
        }

        [HttpGet]
        public ActionResult CreateHome(int id)
        {
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            PhoneNumber phoneNumber = new PhoneNumber();
            phoneNumber.LicenseId = id;
            phoneNumber.PhoneNumberType = phoneNumberManager.GetPhoneNumberType("Home");

            return View("EditPhoneNumber", new PhoneNumberVM(phoneNumber));
        }

        [HttpGet]
        public ActionResult CreateFax(int id)
        {
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            PhoneNumber phoneNumber = new PhoneNumber();
            phoneNumber.LicenseId = id;
            phoneNumber.PhoneNumberType = phoneNumberManager.GetPhoneNumberType("Fax");

            return View("EditPhoneNumber", new PhoneNumberVM(phoneNumber));
        }
    }
}