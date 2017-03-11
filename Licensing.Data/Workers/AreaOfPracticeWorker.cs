using Licensing.Data.Context;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class AreaOfPracticeWorker
    {
        private LicensingContext _context;

        public AreaOfPracticeWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<AreaOfPracticeOption> GetAreaOfPracticeOptions()
        {
            return _context.AreaOfPracticeOptions.OrderBy(o => o.Name).ToList();
        }

        public AreaOfPracticeOption GetOption(int id)
        {
            return _context.AreaOfPracticeOptions.Find(id);
        }

        public AreaOfPracticeOption GetOption(string code)
        {
            ICollection<AreaOfPracticeOption> options = _context.AreaOfPracticeOptions.Where(c => c.AmsCode == code).ToList();

            foreach (AreaOfPracticeOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public void DeleteAreaOfPractice(AreaOfPractice areaOfPractice)
        {
            _context.AreasOfPractice.Remove(areaOfPractice);
            _context.SaveChanges();
        }

        public ICollection<AreaOfPractice> GetResponsesWithOption(AreaOfPracticeOption option)
        {
            return _context.AreasOfPractice.Where(f => f.Option.AreaOfPracticeOptionId == option.AreaOfPracticeOptionId).ToList();
        }

        public void SetOption(AreaOfPracticeOption option)
        {
            _context.Entry(option).State = option.AreaOfPracticeOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(AreaOfPracticeOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
