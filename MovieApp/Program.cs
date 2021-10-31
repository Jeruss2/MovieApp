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

            var acc = new AccountRepo(connection);
            var movie = new MovieRepo(connection);
            var rental = new RentalsRepo(connection);

            var service = new MovieRentalService(acc, movie, rental);

            Account account = service.Login();

            service.RentalLoop(account);

        }
    }
}
