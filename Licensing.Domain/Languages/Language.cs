using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Languages
{
    public class Language
    {
        public int LanguageId { get; set; }
        public int LicenseId { get; set; }
        
        public LanguageOption Option { get; set; }
        public int LanguageOptionId { get; set; }
    }
}
