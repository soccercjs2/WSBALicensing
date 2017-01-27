using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class AreaOfPracticeManager
    {
        private LicensingContext _context;
        private AreaOfPracticeWorker _areaOfPracticeWorker;

        public AreaOfPracticeManager(LicensingContext context, License license)
        {
            _context = context;
            _areaOfPracticeWorker = new AreaOfPracticeWorker(context, license);
        }

        public ICollection<AreaOfPractice> GetAreasOfPractice()
        {
            return _areaOfPracticeWorker.GetAreasOfPractice();
        }
    }
}
