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

        public ICollection<FirmSizeOption> GetOptions()
        {
            return _context.FirmSizeOptions.ToList();
        }

        public FirmSizeOption GetOption(int id)
        {
            return _context.FirmSizeOptions.Find(id);
        }
    }
}
