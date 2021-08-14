using MovieApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Data
{
    public class AccountRepo
    {
        private List<Account> _accounts;

        public AccountRepo()
        {
            _accounts = new List<Account>()
            {
                new Account(){MemberNumber = "123", Pin = 123, Name = "Josh", Balance = 0.00m},
                new Account(){MemberNumber = "456", Pin = 456, Name = "Evan", Balance = 0.00m}
            };
        }

        public List<Account> GetAccounts()
        {
            return _accounts;
        }

        public Account GetAccount(string acctNumber, int pin)
        {
            return _accounts.FirstOrDefault(x => x.MemberNumber == acctNumber && x.Pin == pin);
        }

    }
}