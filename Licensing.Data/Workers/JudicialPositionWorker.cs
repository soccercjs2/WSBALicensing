using Licensing.Data.Context;
using Licensing.Domain.Judicial;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public JudicialPositionOption GetOption(string code)
        {
            return _context.JudicialPositionOptions.Where(c => c.AmsCode == code).FirstOrDefault();
        }

        public void SetCoveredByOption(JudicialPositionOption judicialPositionOption)
        {
            _context.Entry(judicialPositionOption).State = judicialPositionOption.JudicialPositionOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
