using Licensing.Business.Managers;
using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class PaymentController : Controller
    {
        LicensingContext _context;

        public PaymentController()
        {
            _context = new LicensingContext();
        }

        //[HttpGet]
        //public ActionResult Pay(int id)
        //{
        //    LicenseManager licenseManager = new LicenseManager(_context);
        //    MembershipProductManager membershipProductManager = new MembershipProductManager(_context);
        //    SectionManager sectionManager = new SectionManager(_context);
        //    DonationManager donationManager = new DonationManager(_context);
        //    BarNewsManager barNewsManager = new BarNewsManager(_context);

        //    License license = licenseManager.GetLicense(id);

        //    sectionManager.Confirm(license);
        //}
    }
}