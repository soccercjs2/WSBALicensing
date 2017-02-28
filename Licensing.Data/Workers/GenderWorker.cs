using Licensing.Data.Context;
using Licensing.Domain.Genders;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class GenderWorker
    {
        private LicensingContext _context;

        public GenderWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<GenderOption> GetOptions()
        {
            return _context.GenderOptions.ToList();
        }

        public GenderOption GetOption(int id)
        {
            return _context.GenderOptions.Find(id);
        }
    }
}
