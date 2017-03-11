using Licensing.Domain.PracticeAreas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class PracticeAreaOptionsVM
    {
        public IList<PracticeAreaOption> Codes { get; set; }
        public IList<PracticeAreaOption> CodesToBeAdded { get; set; }
        public IList<PracticeAreaOption> CodesToBeActivated { get; set; }
        public IList<PracticeAreaOption> CodesToBeChanged { get; set; }
        public IList<PracticeAreaOption> CodesToBeDeactivated { get; set; }
        public IList<PracticeAreaOption> CodesToBeDeleted { get; set; }
    }
}
