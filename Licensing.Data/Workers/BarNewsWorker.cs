using Licensing.Data.Context;
using Licensing.Domain.BarNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class BarNewsWorker
    {
        private LicensingContext _context;

        public BarNewsWorker(LicensingContext context)
        {
            _context = context;
        }

        public void Confirm(BarNewsResponse barNews)
        {
            barNews.Confirmed = true;
            _context.SaveChanges();
        }
    }
}
