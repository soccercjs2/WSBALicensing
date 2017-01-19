using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Genders
{
    public class Gender
    {
        public int GenderId { get; set; }
        
        public GenderOption Option { get; set; }
        public int GenderOptionId { get; set; }
    }
}
