using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indeed_id.Models;
using Microsoft.EntityFrameworkCore;

namespace indeed_id.Data
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public AppContext(DbContextOptions options) : base(options)
        {
            Seed();
        }

        void Seed()
        {
            if(Users.Any()) return;
            var rnd = new Random();
            for (int i = 1; i <= 5; i++)
            {
                Users.Add(new User {Id = i, Name = $"User{i}"});
            }

            var allCurrencies = new string[]
            {
                "EUR", "USD", "JPY", "BGN", "CZK", "DKK", "GBP", "HUF", "PLN", "RON", "SEK", "CHF", "ISK", "NOK", "HRK",
                "RUB", "TRY", "AUD", "BRL", "CAD", "CNY", "HKD", "IDR", "ILS", "INR", "KRW", "MXN", "MYR", "NZD", "PHP",
                "SGD", "THB", "ZAR"
            };
            SaveChanges();
            var walletId = 1;
            foreach (var user in Users)
            {
                var currencies = allCurrencies.Take(rnd.Next(2, 4));
                foreach (var c in currencies)
                {
                    Wallets.Add(new Wallet{Id = walletId, Amount = rnd.Next(0, 1000000), Currency = c, UserId = user.Id});
                    walletId++;
                }
            }
            SaveChanges();
        }
    }
}
