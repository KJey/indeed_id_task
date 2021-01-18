using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indeed_id.Models.Operations
{
    public class Convert : IOwned
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
    }
}
