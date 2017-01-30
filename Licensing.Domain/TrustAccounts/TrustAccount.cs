using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.TrustAccounts
{
    public class TrustAccount : Preloadable
    {
        public int TrustAccountId { get; set; }
        public bool HandlesTrustAccount { get; set; }
        public virtual ICollection<TrustAccountNumber> TrustAccountNumbers { get; set; }
    }
}
