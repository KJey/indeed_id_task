using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indeed_id.Models;

namespace indeed_id.Engine
{
    public interface IWalletEngine
    {
        public Task<IEnumerable<Wallet>> List(int userId);
        public Task<Wallet> Get(int userId, string currency);
        public Task Update(Wallet wallet);
    }
}
