using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indeed_id.Models.Operations
{
    public class Move : IOwned
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
