using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class LicenseTypeProductManager
    {
        private LicensingContext _context;
        private LicenseTypeProductWorker _licenseTypeProductWorker;

        public LicenseTypeProductManager(LicensingContext context)
        {
            _context = context;
            _licenseTypeProductWorker = new LicenseTypeProductWorker(context);
        }

        public ICollection<LicenseTypeProduct> GetExcludedProducts(LicenseType licenseType)
        {
            List<LicenseTypeProduct> excluded = new List<LicenseTypeProduct>();
            ICollection<LicenseProduct> licenseProducts = _licenseTypeProductWorker.GetProducts();

            foreach (LicenseProduct licenseProduct in licenseProducts)
            {
                if (licenseType.LicenseTypeProducts == null)
                {
                    excluded.Add(new LicenseTypeProduct() { LicenseTypeId = licenseType.LicenseTypeId, Product = licenseProduct });
                }

                bool foundMatch = false;

                foreach (LicenseTypeProduct licenseTypeProduct in licenseType.LicenseTypeProducts)
                {
                    foundMatch = licenseTypeProduct.Product.LicenseProductId == licenseProduct.LicenseProductId;
                }

                if (!foundMatch)
                {
                    excluded.Add(new LicenseTypeProduct() { LicenseTypeId = licenseType.LicenseTypeId, Product = licenseProduct });
                }
            }

            return excluded;
        }

        public void AddLicenseTypeProduct(LicenseType licenseType, LicenseTypeProduct licenseTypeProduct, bool primary, bool lateFee)
        {
            licenseTypeProduct.PrimaryProduct = primary;
            licenseTypeProduct.LateFeeProduct = lateFee;

            _licenseTypeProductWorker.AddLicenseTypeProduct(licenseTypeProduct);
        }

        public void DeleteLicenseTypeProduct(LicenseType licenseType, LicenseTypeProduct licenseTypeProduct)
        {
            _licenseTypeProductWorker.DeleteLicenseTypeProduct(licenseTypeProduct);
        }
    }
}
