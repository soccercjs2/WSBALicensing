using Licensing.Data.Context;
using Licensing.Domain.FirmSizes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class FirmSizeWorker
    {
        private LicensingContext _context;

        public FirmSizeWorker(LicensingContext context)
        {
            _context = context;
        }

        public void DeleteFirmSize(FirmSize firmSize)
        {
            _context.FirmSizes.Remove(firmSize);
        }

        public ICollection<FirmSizeOption> GetOptions()
        {
            return _context.FirmSizeOptions.ToList();
        }

        public FirmSizeOption GetOption(int id)
        {
            return _context.FirmSizeOptions.Find(id);
        }

        public FirmSizeOption GetOption(string code)
        {
            ICollection<FirmSizeOption> options = _context.FirmSizeOptions.Where(c => c.AmsCode == code).ToList();

            foreach (FirmSizeOption option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<FirmSize> GetResponsesWithOption(FirmSizeOption option)
        {
            return _context.FirmSizes.Where(f => f.Option.FirmSizeOptionId == option.FirmSizeOptionId).ToList();
        }

        public void SetOption(FirmSizeOption option)
        {
            _context.Entry(option).State = option.FirmSizeOptionId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(FirmSizeOption option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
