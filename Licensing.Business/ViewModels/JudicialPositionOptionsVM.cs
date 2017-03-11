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
        public IList<JudicialPositionOption> Codes { get; set; }
        public IList<JudicialPositionOption> CodesToBeAdded { get; set; }
        public IList<JudicialPositionOption> CodesToBeActivated { get; set; }
        public IList<JudicialPositionOption> CodesToBeChanged { get; set; }
        public IList<JudicialPositionOption> CodesToBeDeactivated { get; set; }
        public IList<JudicialPositionOption> CodesToBeDeleted { get; set; }
    }
}
