using indeed_id.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indeed_id.Engine;

namespace indeed_id.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletEngine _walletEngine;
        private readonly IUserEngine _userEngine;
        private readonly ICurrencyEngine _currencyEngine;

        public WalletService(IWalletEngine walletEngine, IUserEngine userEngine, ICurrencyEngine currencyEngine)
        {
            _walletEngine = walletEngine;
            _userEngine = userEngine;
            _currencyEngine = currencyEngine;
        }

        public async Task<bool> Convert(int userId, string fromCurrency, string toCurrency, decimal amount)
        {
            var fromWallet = await _walletEngine.Get(userId, fromCurrency);
            var toWallet = await _walletEngine.Get(userId, toCurrency);
            if (fromWallet == null || toWallet == null) throw new Exception("Один из кошельков не найден");

            if (fromWallet.Amount < amount) throw new Exception("Недостаточно средств в кошельке");

            decimal? fromCurrencyRate = 1;
            decimal? toCurrencyRate = 1;
            if (fromCurrency != "EUR")
                fromCurrencyRate = await _currencyEngine.GetRate(fromCurrency);
            if (toCurrency != "EUR")
                toCurrencyRate = await _currencyEngine.GetRate(toCurrency);

            if (fromCurrencyRate == null || toCurrencyRate == null) throw new Exception("Не найден обменный курс для одной из валют");

            fromWallet.Amount -= amount;
            toWallet.Amount += Math.Round(((amount/fromCurrencyRate.Value) * toCurrencyRate.Value),2);

            await _walletEngine.Update(fromWallet);
            await _walletEngine.Update(toWallet);

            return true;
        }

        public async Task<bool> Deposit(int userId, string currency, decimal amount)
        {
            var wallet = await _walletEngine.Get(userId, currency);

            if (wallet == null)
            {
                throw new Exception("Кошелек не найден");
            }

            wallet.Amount += amount;
            await _walletEngine.Update(wallet);

            return true;
        }

        public async Task<object> Info(int userId)
        {
            if(userId<=0) throw new ArgumentException("Пользователь с таким идентификатором не найден");
            var user = await _userEngine.Get(userId);
            var wallets = await _walletEngine.List(userId);

            if (user == null) throw new Exception("Пользователь не найден");

            return new
            {
                UserName = user.Name,
                Wallets = wallets
            };
        }

        public async Task<bool> Withdraw(int userId, string currency, decimal amount)
        {
            var wallet = await _walletEngine.Get(userId, currency);

            if (wallet == null)
            {
                throw new Exception("Кошелек не найден");
            }

            if (wallet.Amount < amount)
            {
                throw new Exception("Недостаточно средств");
            }

            wallet.Amount -= amount;
            await _walletEngine.Update(wallet);

            return true;
        }
    }
}
