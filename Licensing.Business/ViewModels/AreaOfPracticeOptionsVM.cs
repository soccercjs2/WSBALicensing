using Licensing.Domain.AreasOfPractice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class AreaOfPracticeOptionsVM
    {
        public IList<AreaOfPracticeOption> Codes { get; set; }
        public IList<AreaOfPracticeOption> CodesToBeAdded { get; set; }
        public IList<AreaOfPracticeOption> CodesToBeActivated { get; set; }
        public IList<AreaOfPracticeOption> CodesToBeChanged { get; set; }
        public IList<AreaOfPracticeOption> CodesToBeDeactivated { get; set; }
        public IList<AreaOfPracticeOption> CodesToBeDeleted { get; set; }
    }
}
