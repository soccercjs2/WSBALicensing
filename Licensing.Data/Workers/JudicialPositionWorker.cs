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

        public void DeleteJudicialPosition(JudicialPosition judicialPosition)
        {
            _context.JudicialPositions.Remove(judicialPosition);
            _context.SaveChanges();
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
            ICollection<JudicialPositionOption> options = _context.JudicialPositionOptions.Where(c => c.AmsCode == code).ToList();

            foreach (JudicialPositionOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<JudicialPosition> GetResponsesWithOption(JudicialPositionOption option)
        {
            return _context.JudicialPositions.Where(f => f.Option.JudicialPositionOptionId == option.JudicialPositionOptionId).ToList();
        }

        public void SetCoveredByOption(JudicialPositionOption judicialPositionOption)
        {
            _context.Entry(judicialPositionOption).State = judicialPositionOption.JudicialPositionOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(JudicialPositionOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
