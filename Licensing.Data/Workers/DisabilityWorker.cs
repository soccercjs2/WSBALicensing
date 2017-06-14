using Licensing.Data.Context;
using Licensing.Domain.Disabilities;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class DisabilityWorker
    {
        private LicensingContext _context;

        public DisabilityWorker(LicensingContext context)
        {
            _context = context;
        }

        public void DeleteDisability(Disability disability)
        {
            _context.Disabilities.Remove(disability);
        }

        public ICollection<DisabilityOption> GetOptions()
        {
            return _context.DisabilityOptions.ToList();
        }

        public DisabilityOption GetOption(int id)
        {
            return _context.DisabilityOptions.Find(id);
        }

        public DisabilityOption GetOption(string code)
        {
            ICollection<DisabilityOption> options = _context.DisabilityOptions.Where(c => c.AmsCode == code).ToList();

            foreach (DisabilityOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<Disability> GetResponsesWithOption(DisabilityOption option)
        {
            return _context.Disabilities.Where(f => f.Option.DisabilityOptionId == option.DisabilityOptionId).ToList();
        }

        public void SetOption(DisabilityOption option)
        {
            _context.Entry(option).State = option.DisabilityOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(DisabilityOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
