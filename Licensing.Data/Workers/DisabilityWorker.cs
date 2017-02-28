using Licensing.Data.Context;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class DisabilityWorker
    {
        private LicensingContext _context;

        public DisabilityWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<DisabilityOption> GetOptions()
        {
            return _context.DisabilityOptions.ToList();
        }

        public DisabilityOption GetOption(int id)
        {
            return _context.DisabilityOptions.Find(id);
        }
    }
}
