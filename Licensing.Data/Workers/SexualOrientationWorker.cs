using Licensing.Data.Context;
using Licensing.Domain.Licenses;
using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class SexualOrientationWorker
    {
        private LicensingContext _context;

        public SexualOrientationWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<SexualOrientationOption> GetOptions()
        {
            return _context.SexualOrientationOptions.ToList();
        }

        public SexualOrientationOption GetOption(int id)
        {
            return _context.SexualOrientationOptions.Find(id);
        }

        public SexualOrientationOption GetOption(string code)
        {
            ICollection<SexualOrientationOption> options = _context.SexualOrientationOptions.Where(c => c.AmsCode == code).ToList();

            foreach (SexualOrientationOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<SexualOrientation> GetResponsesWithOption(SexualOrientationOption option)
        {
            return _context.SexualOrientations.Where(f => f.Option.SexualOrientationOptionId == option.SexualOrientationOptionId).ToList();
        }

        public void SetOption(SexualOrientationOption option)
        {
            _context.Entry(option).State = option.SexualOrientationOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(SexualOrientationOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
