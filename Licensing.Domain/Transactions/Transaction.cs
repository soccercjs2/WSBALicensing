using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licensing.Domain.Transactions
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public int AmsTransactionId { get; set; }
        public string AmsCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
