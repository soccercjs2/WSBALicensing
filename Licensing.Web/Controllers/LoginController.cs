using Licensing.Business.Managers;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licensing.Web.Controllers
{
    public class LoginController : Controller
    {
        LicensingContext _context;

        public LoginController()
        {
            _context = new LicensingContext();
        }

        public ActionResult Index()
        {
            return View("Index", new LoginVM());
        }

        [HttpPost]
        public ActionResult Index(LoginVM loginVM)
        {
            Session["CurrentUser"] = loginVM.UserName;

            return RedirectToAction("Index", "Home");
        }
    }
}