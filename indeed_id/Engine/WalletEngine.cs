using indeed_id.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indeed_id.Data;
using Microsoft.EntityFrameworkCore;

namespace indeed_id.Engine
{
    public class WalletEngine : IWalletEngine
    {
        private readonly AppContext _appContext;

        public WalletEngine(AppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Wallet> Get(int userId, string currency)
        {
            return await _appContext.Wallets.FirstOrDefaultAsync(x => x.UserId == userId && x.Currency == currency);
        }

        public async Task<IEnumerable<Wallet>> List(int userId)
        {
            return await _appContext.Wallets.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task Update(Wallet wallet)
        {
            _appContext.Wallets.Update(wallet);
            await _appContext.SaveChangesAsync();
        }
    }
}
