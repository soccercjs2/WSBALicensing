using Licensing.Data.Context;
using Licensing.Domain.Judicial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class JudicialPositionWorker
    {
        private LicensingContext _context;

        public JudicialPositionWorker(LicensingContext context)
        {
            _context = context;
        }

        public void Confirm(JudicialPosition judicialPosition)
        {
            judicialPosition.Confirmed = true;
            _context.SaveChanges();
        }
    }
}
