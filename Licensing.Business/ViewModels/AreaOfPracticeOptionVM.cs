using Licensing.Domain.AreasOfPractice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class AreaOfPracticeOptionVM
    {
        public int AreaOfPracticeOptionId { get; set; }
        public string Name { get; set; }
        public bool PreSelected { get; set; }
        public bool Selected { get; set; }

        public AreaOfPracticeOptionVM() { }

        public AreaOfPracticeOptionVM(AreaOfPracticeOption option)
        {
            AreaOfPracticeOptionId = option.AreaOfPracticeOptionId;
            Name = option.Name;
        }
    }
}
