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
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool PreSelected { get; set; }
        public bool Selected { get; set; }

        public SectionProductVM() { }

        public SectionProductVM(SectionProduct product)
        {
            SectionProductId = product.SectionProductId;
            Name = product.Name;
            Price = product.Price;
        }
    }
}
