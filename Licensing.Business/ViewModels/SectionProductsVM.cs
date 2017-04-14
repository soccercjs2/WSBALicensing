using Licensing.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class SectionProductsVM
    {
        public IList<SectionProduct> Codes { get; set; }
        public IList<SectionProduct> CodesToBeAdded { get; set; }
        public IList<SectionProduct> CodesToBeActivated { get; set; }
        public IList<SectionProduct> CodesToBeChanged { get; set; }
        public IList<SectionProduct> CodesToBeDeactivated { get; set; }
        public IList<SectionProduct> CodesToBeDeleted { get; set; }
    }
}
