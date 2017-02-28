using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.PracticeAreas;
using System;
using System.Collections.Generic;
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

        public ICollection<PracticeAreaOption> GetPracticeAreaOptions()
        {
            return _context.PracticeAreaOptions.OrderBy(o => o.Name).ToList();
        }

        public PracticeAreaOption GetPracticeAreaOption(int id)
        {
            return _context.PracticeAreaOptions.Find(id);
        }

        public void DeletePracticeArea(PracticeArea practiceArea)
        {
            _context.PracticeAreas.Remove(practiceArea);
            _context.SaveChanges();
        }
    }
}
