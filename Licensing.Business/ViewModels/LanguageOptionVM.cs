using Licensing.Domain.AreasOfPractice;
using Licensing.Domain.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LanguageOptionVM
    {
        public int LanguageOptionId { get; set; }
        public string Name { get; set; }
        public bool PreSelected { get; set; }
        public bool Selected { get; set; }

        public LanguageOptionVM() { }

        public LanguageOptionVM(LanguageOption option)
        {
            LanguageOptionId = option.LanguageOptionId;
            Name = option.Name;
        }
    }
}
