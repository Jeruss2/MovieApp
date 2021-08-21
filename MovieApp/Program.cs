using MovieApp.Business;
using MovieApp.Data;
using MovieApp.Models;
using System.IO;

namespace MovieApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connection = File.ReadAllText("configs.txt");

            var acc = new AccountRepo();
            var movie = new MovieRepo(connection);
            var rental = new RentalsRepo();

            var service = new MovieRentalService(acc, movie, rental);

            Account account = service.Login();

            service.RentalLoop(account);



            // var account = new AccountRepo();

            //var theList = account.FetchAllAccounts();

            //foreach (var account1 in theList)
            //{
            //    Console.WriteLine(account1.Name);
            //    Console.WriteLine(account1.Pin);
            //    Console.WriteLine(account1.Balance);
            //    Console.WriteLine(account1.AccountTypes);
            //    Console.WriteLine(account1.MemberNumber);
            //}

        }
    }
}
