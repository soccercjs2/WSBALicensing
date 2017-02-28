using Licensing.Data.Context;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
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

        public AreaOfPracticeOption GetAreaOfPracticeOption(int id)
        {
            return _context.AreaOfPracticeOptions.Find(id);
        }

        public void DeleteAreaOfPractice(AreaOfPractice areaOfPractice)
        {
            _context.AreasOfPractice.Remove(areaOfPractice);
            _context.SaveChanges();
        }
    }
}
