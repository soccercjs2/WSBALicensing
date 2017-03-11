using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.PracticeAreas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class PracticeAreaWorker
    {
        private LicensingContext _context;

        public PracticeAreaWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<PracticeAreaOption> GetOptions()
        {
            return _context.PracticeAreaOptions.OrderBy(o => o.Name).ToList();
        }

        public PracticeAreaOption GetOption(int id)
        {
            return _context.PracticeAreaOptions.Find(id);
        }

        public PracticeAreaOption GetOption(string code)
        {
            ICollection<PracticeAreaOption> options = _context.PracticeAreaOptions.Where(c => c.AmsCode == code).ToList();

            foreach (PracticeAreaOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<PracticeArea> GetResponsesWithOption(PracticeAreaOption option)
        {
            return _context.PracticeAreas.Where(f => f.Option.PracticeAreaOptionId == option.PracticeAreaOptionId).ToList();
        }

        public void DeletePracticeArea(PracticeArea practiceArea)
        {
            _context.PracticeAreas.Remove(practiceArea);
            _context.SaveChanges();
        }

        public void SetOption(PracticeAreaOption option)
        {
            _context.Entry(option).State = option.PracticeAreaOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(PracticeAreaOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
