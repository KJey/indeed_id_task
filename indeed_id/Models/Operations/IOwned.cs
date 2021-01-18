using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indeed_id.Models.Operations
{
    public interface IOwned
    {
        public int UserId { get; set; }
    }
}
