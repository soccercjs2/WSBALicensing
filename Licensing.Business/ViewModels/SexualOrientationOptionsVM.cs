using Licensing.Domain.SexualOrientations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class SexualOrientationOptionsVM
    {
        public IList<SexualOrientationOption> Codes { get; set; }
        public IList<SexualOrientationOption> CodesToBeAdded { get; set; }
        public IList<SexualOrientationOption> CodesToBeActivated { get; set; }
        public IList<SexualOrientationOption> CodesToBeChanged { get; set; }
        public IList<SexualOrientationOption> CodesToBeDeactivated { get; set; }
        public IList<SexualOrientationOption> CodesToBeDeleted { get; set; }
    }
}
