using Licensing.Domain.Judicial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class JudicialPositionOptionsVM
    {
        public IList<JudicialPositionOption> ActiveCodes { get; set; }
        public IList<JudicialPositionOption> InactiveCodes { get; set; }
        public IList<JudicialPositionOption> PersonifyCodes { get; set; }
    }
}
