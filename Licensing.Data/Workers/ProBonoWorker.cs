using Licensing.Data.Context;
using Licensing.Domain.ProBonos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class ProBonoWorker
    {
        private LicensingContext _context;

        public ProBonoWorker(LicensingContext context)
        {
            _context = context;
        }

        public void DeleteProBono(ProBono proBono)
        {
            _context.ProBonos.Remove(proBono);
            _context.SaveChanges();
        }
    }
}
