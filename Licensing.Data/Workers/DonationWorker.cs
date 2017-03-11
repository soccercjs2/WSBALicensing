using Licensing.Data.Context;
using Licensing.Domain.Donations;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Data.Workers
{
    public class DonationWorker
    {
        private LicensingContext _context;

        public DonationWorker(LicensingContext context)
        {
            _context = context;
        }

        public ICollection<DonationProduct> GetProducts()
        {
            return _context.DonationProducts.OrderBy(o => o.Name).ToList();
        }

        public DonationProduct GetProduct(int id)
        {
            return _context.DonationProducts.Find(id);
        }

        public DonationProduct GetProduct(string code)
        {
            ICollection<DonationProduct> options = _context.DonationProducts.Where(c => c.AmsCode == code).ToList();

            foreach (DonationProduct option in options)
            {
                if (option.AmsCode == code)
                {
                    return option;
                }
            }

            return null;
        }

        public ICollection<Donation> GetResponsesWithOption(DonationProduct option)
        {
            return _context.Donations.Where(f => f.Product.DonationProductId == option.DonationProductId).ToList();
        }

        public void SetOption(DonationProduct option)
        {
            _context.Entry(option).State = option.DonationProductId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteOption(DonationProduct option)
        {
            _context.Entry(option).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
