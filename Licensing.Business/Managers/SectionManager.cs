using Licensing.Business.Tools;
using Licensing.Business.ViewModels;
using Licensing.Data.Context;
using Licensing.Data.Workers;
using Licensing.Domain.Licenses;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.Managers
{
    public class SectionManager
    {
        private LicensingContext _context;
        private SectionWorker _sectionWorker;

        public SectionManager(LicensingContext context)
        {
            _context = context;
            _sectionWorker = new SectionWorker(context);
        }

        public ICollection<Section> GetSections(License license)
        {
            if (license.Sections == null || license.Sections.Count == 0) { return null; }
            else { return license.Sections; }
        }

        public ICollection<SectionProduct> GetProducts()
        {
            return _sectionWorker.GetProducts();
        }

        public SectionProduct GetProduct(int id)
        {
            return _sectionWorker.GetProduct(id);
        }

        public SectionProduct GetProduct(string code)
        {
            return _sectionWorker.GetProduct(code);
        }

        public void SetSection(License license, int sectionProductId)
        {
            Section matchedSection = null;

            if (license.Sections != null)
            {
                matchedSection = license.Sections.Where(s => s.Product.SectionProductId == sectionProductId).FirstOrDefault();
            }

            if (matchedSection == null)
            {
                Section section = new Section();
                section.Product = GetProduct(sectionProductId);

                if (license.Sections == null)
                {
                    license.Sections = new List<Section>();
                }

                license.Sections.Add(section);

                _context.SaveChanges();
            }
        }

        public void DeleteSection(License license, int sectionProductId)
        {
            if (license.Sections != null)
            {
                Section section = license.Sections.Where(a => a.Product.SectionProductId == sectionProductId).FirstOrDefault();
                _sectionWorker.DeleteSection(section);

                _context.SaveChanges();
            }
        }

        public void Confirm(License license)
        {
            license.SectionsConfirmed = true;
            _context.SaveChanges();
        }

        public bool IsComplete(License license)
        {
            return GetBalanceDue(license) <= 0;
        }

        public IList<SectionProduct> GetAmsOptions()
        {
            IList<SectionProduct> options = new List<SectionProduct>();
            var codes = WSBA.AMS.CodeTypesManager.GetSectionProductCodeList().OrderBy(c => c.Description);

            foreach (var code in codes)
            {
                options.Add(new SectionProduct() { Name = code.Description, AmsCode = code.Code, Price = code.Price, AmsProductId = code.ProductId, Active = true });
            }

            return options;
        }

        public void SetOption(SectionProduct option)
        {
            if (option.SectionProductId == 0)
            {
                SectionProduct existingCode = _sectionWorker.GetProduct(option.AmsCode);

                if (existingCode != null)
                {
                    existingCode.Active = true;
                    existingCode.Name = option.Name;
                    option = existingCode;
                }
            }

            _sectionWorker.SetOption(option);
        }

        public void DeleteOption(SectionProduct option)
        {
            _sectionWorker.DeleteOption(option);
        }

        public IList<SectionProduct> GetCodesToBeAdded(ICollection<SectionProduct> codes, ICollection<SectionProduct> amsCodes)
        {
            return amsCodes.Where(ac => !codes.Any(c => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<SectionProduct> GetCodesToBeActivated(ICollection<SectionProduct> codes, ICollection<SectionProduct> amsCodes)
        {
            //get inactive codes
            codes = codes.Where(c => !c.Active).ToList();
            return codes.Where(c => amsCodes.Any(ac => c.AmsCode == ac.AmsCode)).ToList();
        }

        public IList<SectionProduct> GetCodesToBeChanged(ICollection<SectionProduct> codes, ICollection<SectionProduct> amsCodes)
        {
            return amsCodes.Where(ac => codes.Any(c => c.AmsCode == ac.AmsCode && (c.Name != ac.Name || c.Price != ac.Price))).ToList();
        }

        public IList<SectionProduct> GetCodesToBeDeactivated(ICollection<SectionProduct> codes, ICollection<SectionProduct> amsCodes)
        {
            //get active codes
            codes = codes.Where(c => c.Active).ToList();

            IList<SectionProduct> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<SectionProduct> codesToDeactivate = new List<SectionProduct>();

            foreach (SectionProduct option in codesToRemove)
            {
                ICollection<Section> responsesWithOption = _sectionWorker.GetResponsesWithOption(option);
                if (responsesWithOption != null && responsesWithOption.Count > 0)
                {
                    codesToDeactivate.Add(option);
                }
            }

            return codesToDeactivate;
        }

        public IList<SectionProduct> GetCodesToBeDeleted(ICollection<SectionProduct> codes, ICollection<SectionProduct> amsCodes)
        {
            IList<SectionProduct> codesToRemove = codes.Where(c => !amsCodes.Any(ac => ac.AmsCode == c.AmsCode)).ToList();
            IList<SectionProduct> codesToDeleted = new List<SectionProduct>();

            foreach (SectionProduct option in codesToRemove)
            {
                ICollection<Section> responsesWithOption = _sectionWorker.GetResponsesWithOption(option);
                if (responsesWithOption == null || responsesWithOption.Count == 0)
                {
                    codesToDeleted.Add(option);
                }
            }

            return codesToDeleted;
        }

        public decimal GetBalanceDue(License license)
        {
            decimal balance = 0;

            if (license.Sections != null)
            {
                foreach (var section in license.Sections)
                {
                    if (license.SectionOrder != null)
                    {
                        var transactions =
                            (license.SectionOrder != null) ?
                            license.SectionOrder.Transactions.Where(p => p.AmsCode == section.Product.AmsCode).ToList() :
                            null;

                        if (transactions == null) { balance += section.Product.Price; }
                        else
                        {
                            decimal transactionTotal = 0;
                            foreach (var transaction in transactions)
                            {
                                transactionTotal += transaction.Amount;
                            }

                            balance += section.Product.Price + transactionTotal;
                        }
                    }
                    else { balance += section.Product.Price; }
                }
            }

            return Math.Max(balance, 0);
        }

        public ICollection<SectionProductVM> GetSectionProductsWithBalance(License license, bool onlyProductsWithBalance)
        {
            if (license.Sections == null) { return null; }

            var sectionProductVMs = license.Sections.Select(s =>
                new SectionProductVM(s.Product, s.Product.Price + GetPaymentTotal(license, s.Product)));

            if (onlyProductsWithBalance)
            {
                sectionProductVMs = sectionProductVMs.Where(spvm => spvm.Price > 0);
            }

            return sectionProductVMs.OrderByDescending(spvm => spvm.Price).ToList();
        }

        public ICollection<SectionProductVM> GetSectionProductsWithPayment(License license)
        {
            if (license.Sections == null) { return null; }

            return license.Sections.Select(s =>
                new SectionProductVM(s.Product, -GetPaymentTotal(license, s.Product)))
                .Where(spvm => spvm.Price > 0)
                .OrderByDescending(spvm => spvm.Price).ToList();
        }

        public decimal GetPaymentTotal(License license, SectionProduct sectionProduct)
        {
            var transactions = 
                (license.SectionOrder != null) ?
                license.SectionOrder.Transactions.Where(p => p.AmsCode == sectionProduct.AmsCode).ToList() :
                null;

            if (transactions == null) { return 0; }
            else
            {
                decimal amountPaid = 0;
                foreach (var transaction in transactions)
                {
                    amountPaid += transaction.Amount;
                }

                return amountPaid;
            }
        }

        public DashboardContainerVM GetDashboardContainerVM(License license)
        {
            RouteContainer editRoute = new RouteContainer("Sections", "Edit", license.LicenseId);

            return new DashboardContainerVM(
                "Sections",
                license.LicenseType.LicenseTypeRequirement.Sections,
                IsComplete(license),
                editRoute,
                null,
                true,
                "_Sections",
                GetSections(license)
            );
        }
    }
}
