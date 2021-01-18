using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indeed_id.Models;

namespace indeed_id.Services
{
    public interface IWalletService
    {
        Task<bool> Withdraw(int userId, string currency, decimal amount);
        Task<bool> Deposit(int userId, string currency, decimal amount);
        Task<bool> Convert(int userId, string fromCurrency, string toCurrency, decimal amount);
        Task<object> Info(int userId);
    }
}
