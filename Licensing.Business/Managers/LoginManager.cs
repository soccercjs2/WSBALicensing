using Licensing.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Licensing.Business.Managers
{
    public class LoginManager
    {
        private LicensingContext _context;
        
        public LoginManager(LicensingContext context)
        {
            _context = context;
        }
    }
}
