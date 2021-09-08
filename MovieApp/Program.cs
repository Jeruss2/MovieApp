using System;
using MovieApp.Business;
using MovieApp.Data;
using MovieApp.Models;
using System.IO;
using MovieApp___Business;

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



            //var troy = movie.FetchMovie("Troy");

            //Console.WriteLine(troy.Title);
            //Console.WriteLine(troy.Director);






        }
    }
}
