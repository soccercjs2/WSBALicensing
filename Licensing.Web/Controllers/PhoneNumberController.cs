using Licensing.Business.Managers;
using Licensing.Data.Context;
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
            //confirm the preloaded Trust Account
            PhoneNumberManager phoneNumberManager = new PhoneNumberManager(_context);
            phoneNumberManager.Confirm(phoneNumberManager.GetPhoneNumber(id));

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}