using Licensing.Business.Managers;
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
            //confirm the preloaded Trust Account
            AddressManager addressManager = new AddressManager(_context);
            addressManager.Confirm(addressManager.GetAddress(id));

            //return updated partial view
            return RedirectToAction("Index", "Home");
        }
    }
}