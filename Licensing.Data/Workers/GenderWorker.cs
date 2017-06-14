using Licensing.Data.Context;
using Licensing.Domain.Genders;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class GenderWorker
    {
        private LicensingContext _context;

        public GenderWorker(LicensingContext context)
        {
            _context = context;
        }

        public void DeleteGender(Gender gender)
        {
            _context.Genders.Remove(gender);
        }

        public ICollection<GenderOption> GetOptions()
        {
            return _context.GenderOptions.ToList();
        }

        public GenderOption GetOption(int id)
        {
            return _context.GenderOptions.Find(id);
        }

        public GenderOption GetOption(string code)
        {
            ICollection<GenderOption> options = _context.GenderOptions.Where(c => c.AmsCode == code).ToList();

            foreach (GenderOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<Gender> GetResponsesWithOption(GenderOption option)
        {
            return _context.Genders.Where(f => f.Option.GenderOptionId == option.GenderOptionId).ToList();
        }

        public void SetOption(GenderOption option)
        {
            _context.Entry(option).State = option.GenderOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(GenderOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
