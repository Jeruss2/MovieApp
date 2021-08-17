using System;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using MovieApp.Business;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new MovieRentalService();

            Account account = service.Login();

            service.RentalLoop(account);

        }
    }
}
