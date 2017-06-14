using Licensing.Domain.MCLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class McleVM
    {
        public MCLETranscript MCLETranscript { get; set; }
        public bool HasUnpaidMCLELateFee { get; set; }
        public bool HasUnpaidMCLEComityFee { get; set; }
    }
}
