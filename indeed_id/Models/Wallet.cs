using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indeed_id.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
