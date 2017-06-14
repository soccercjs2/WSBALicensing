using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class SectionProductVM
    {
        public int SectionProductId { get; set; }
        public string AmsCode { get; set; }
        public int AmsProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool PreSelected { get; set; }
        public bool Selected { get; set; }
        public bool Paid { get; set; }

        public SectionProductVM() { }

        public SectionProductVM(SectionProduct product)
        {
            SectionProductId = product.SectionProductId;
            AmsCode = product.AmsCode;
            AmsProductId = product.AmsProductId;
            Name = product.Name;
            Price = product.Price;
        }

        public SectionProductVM(SectionProduct product, decimal price)
        {
            SectionProductId = product.SectionProductId;
            AmsCode = product.AmsCode;
            AmsProductId = product.AmsProductId;
            Name = product.Name;
            Price = price;
        }
    }
}
