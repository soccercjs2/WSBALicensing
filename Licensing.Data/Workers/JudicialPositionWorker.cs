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

        public ICollection<JudicialPositionOption> GetOptions()
        {
            return _context.JudicialPositionOptions.ToList();
        }

        public JudicialPositionOption GetOption(int id)
        {
            return _context.JudicialPositionOptions.Find(id);
        }
    }
}
