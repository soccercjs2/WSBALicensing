using Licensing.Domain.Genders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class GenderOptionsVM
    {
        public IList<GenderOption> Codes { get; set; }
        public IList<GenderOption> CodesToBeAdded { get; set; }
        public IList<GenderOption> CodesToBeActivated { get; set; }
        public IList<GenderOption> CodesToBeChanged { get; set; }
        public IList<GenderOption> CodesToBeDeactivated { get; set; }
        public IList<GenderOption> CodesToBeDeleted { get; set; }
    }
}
