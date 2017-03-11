using Licensing.Domain.FirmSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class FirmSizeOptionsVM
    {
        public IList<FirmSizeOption> Codes { get; set; }
        public IList<FirmSizeOption> CodesToBeAdded { get; set; }
        public IList<FirmSizeOption> CodesToBeActivated { get; set; }
        public IList<FirmSizeOption> CodesToBeChanged { get; set; }
        public IList<FirmSizeOption> CodesToBeDeactivated { get; set; }
        public IList<FirmSizeOption> CodesToBeDeleted { get; set; }
    }
}
