using Licensing.Domain.Ethnicities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class EthnicityOptionsVM
    {
        public IList<EthnicityOption> Codes { get; set; }
        public IList<EthnicityOption> CodesToBeAdded { get; set; }
        public IList<EthnicityOption> CodesToBeActivated { get; set; }
        public IList<EthnicityOption> CodesToBeChanged { get; set; }
        public IList<EthnicityOption> CodesToBeDeactivated { get; set; }
        public IList<EthnicityOption> CodesToBeDeleted { get; set; }
    }
}
