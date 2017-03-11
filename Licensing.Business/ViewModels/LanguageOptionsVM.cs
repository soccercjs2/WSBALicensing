using Licensing.Domain.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Business.ViewModels
{
    public class LanguageOptionsVM
    {
        public IList<LanguageOption> Codes { get; set; }
        public IList<LanguageOption> CodesToBeAdded { get; set; }
        public IList<LanguageOption> CodesToBeActivated { get; set; }
        public IList<LanguageOption> CodesToBeChanged { get; set; }
        public IList<LanguageOption> CodesToBeDeactivated { get; set; }
        public IList<LanguageOption> CodesToBeDeleted { get; set; }
    }
}
