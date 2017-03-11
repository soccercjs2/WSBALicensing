using Licensing.Data.Context;
using Licensing.Domain.Ethnicities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class EthnicityWorker
    {
        private LicensingContext _context;

        public EthnicityWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<EthnicityOption> GetOptions()
        {
            return _context.EthnicityOptions.ToList();
        }

        public EthnicityOption GetOption(int id)
        {
            return _context.EthnicityOptions.Find(id);
        }

        public EthnicityOption GetOption(string code)
        {
            ICollection<EthnicityOption> options = _context.EthnicityOptions.Where(c => c.AmsCode == code).ToList();

            foreach (EthnicityOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<Ethnicity> GetResponsesWithOption(EthnicityOption option)
        {
            return _context.Ethnicities.Where(f => f.Option.EthnicityOptionId == option.EthnicityOptionId).ToList();
        }

        public void SetOption(EthnicityOption option)
        {
            _context.Entry(option).State = option.EthnicityOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(EthnicityOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
