using Licensing.Domain.PracticeAreas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class PracticeAreaOptionVM
    {
        public int PracticeAreaOptionId { get; set; }
        public string Name { get; set; }
        public bool PreSelected { get; set; }
        public bool Selected { get; set; }

        public PracticeAreaOptionVM() { }

        public PracticeAreaOptionVM(PracticeAreaOption option)
        {
            PracticeAreaOptionId = option.PracticeAreaOptionId;
            Name = option.Name;
        }
    }
}
