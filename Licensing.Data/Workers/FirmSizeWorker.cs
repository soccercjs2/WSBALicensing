using Licensing.Data.Context;
using Licensing.Domain.FirmSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class FirmSizeWorker
    {
        private LicensingContext _context;

        public FirmSizeWorker(LicensingContext context)
        {
            _context = context;
        }

        public void Confirm(FirmSize firmSize)
        {
            firmSize.Confirmed = true;
            _context.SaveChanges();
        }
    }
}
