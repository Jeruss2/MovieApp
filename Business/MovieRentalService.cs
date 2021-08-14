using MovieApp.Data;
using MovieApp.Models;
using System;
using System.Collections.Generic;

namespace MovieApp.Business
{
    class MovieRentalService
    {
        private AccountRepo _accountRepo;
        private MovieRepo _movieRepo;
        private RentalsRepo _rentalsRepo;


        public MovieRentalService()
        {
            _accountRepo = new AccountRepo();
            _movieRepo = new MovieRepo();
            _rentalsRepo = new RentalsRepo();
        }


        public Account Login()
        {
            Account account = null;


            int i = 0;

            while (account == null)
            {
                Console.WriteLine("Enter Member Number");

                var memNumber = Console.ReadLine();

                Console.WriteLine("Enter Pin");

                var acctPin = int.Parse(Console.ReadLine());

                account = _accountRepo.GetAccount(memNumber, acctPin);


                if (account != null)
                {
                    return account;
                }

                if (i >= 3)
                {
                    Console.WriteLine("You have reach the maximum number of login attempts");
                    break;
                }

                i++;

            }

            return account;
        }

        public void RentalLoop(Account userAccount)
        {
            var userInput = "";



            while (userInput != "6")
            {
                Console.WriteLine();
                Console.WriteLine($"Hi {userAccount.Name}!");
                Console.WriteLine();

                Menu();

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        ShowMovies();
                        RentMovie(userAccount);
                        break;
                    case "2":
                        ExtendRental(userAccount);
                        break;
                    case "3":
                        PurchaseRental(userAccount);
                        break;
                    case "4":
                        ShowPurchasedMovies(userAccount);
                        break;
                    case "5":
                        ReturnRental(userAccount);
                        break;
                    case "6":
                        Exit(userAccount);
                        break;

                }
            }
        }

        public void ShowMovies()
        {
            List<Movie> instockMovies = _movieRepo.InstockMovies();

            Console.WriteLine();
            Console.WriteLine("Movies Available:");

            foreach (var instockMovie in instockMovies)
            {
                Console.WriteLine(instockMovie.Title);
            }

            Console.WriteLine();

            List<Movie> movies = _movieRepo.GetMovies();

            Console.WriteLine("All Movies:");

            foreach (var movie in movies)
            {

                Console.WriteLine(movie.Title);

            }

            Console.WriteLine();

        }

        public void ShowPurchasedMovies(Account account)
        {
            List<Movie> purchased = _movieRepo.GetPurchaseList();

            Console.WriteLine();
            Console.WriteLine("Purchased Movies:");
            foreach (var movie in purchased)
            {
                Console.WriteLine($"Title: {movie.Title}");
            }
        }

        public void RentMovie(Account account)
        {
            var movieSelect = "";

            var rent = new Rentals();

            rent.Account = account;
            rent.RentalTypes = RentalTypes.Rent;
            rent.RentalDate = DateTime.Now;
            rent.DueDate = DateTime.Now.AddDays(7);

            Console.WriteLine("Enter the title of the movie you would like to rent");

            movieSelect = Console.ReadLine();


            rent.Movie = _movieRepo.GetMovie(movieSelect);

            _movieRepo.RemoveFromInstock(movieSelect);
            _rentalsRepo.AddRentals(rent);

            var myRentals = _rentalsRepo.GetAccountRental(account.MemberNumber);

            myRentals.Sort((x, y) => DateTime.Compare(x.DueDate, y.DueDate));

            Console.WriteLine();
            Console.WriteLine("Current Rentals:");

            foreach (var rental in myRentals)
            {
                Console.WriteLine();
                Console.WriteLine($"Movie: {rental.Movie.Title} Due Date: {rental.DueDate}");
                Console.WriteLine();
            }

        }

        public void ExtendRental(Account account)
        {
            var rentalExtension = _rentalsRepo.GetAccountRental(account.MemberNumber);


            Console.WriteLine();
            Console.WriteLine("Current Rentals:");

            foreach (var rentals in rentalExtension)
            {
                Console.WriteLine(rentals.Movie.Title);
            }

            Console.WriteLine();
            Console.WriteLine("Enter the title of the movie you would like to extend.");

            var extend = Console.ReadLine();



            Console.WriteLine();
            Console.WriteLine("Current Rentals:");
            Console.WriteLine();

            foreach (var rental in rentalExtension)
            {
                if (rental.Movie.Title.ToLowerInvariant() == extend.ToLowerInvariant())
                {
                    rental.DueDate = rental.DueDate.AddDays(7);
                }

                Console.WriteLine($"Title: {rental.Movie.Title} Due Date: {rental.DueDate}");
                Console.WriteLine();
            }

        }

        public void PurchaseRental(Account account)
        {
            var rentalPurchase = _rentalsRepo.GetAccountRental(account.MemberNumber);

            Console.WriteLine();
            Console.WriteLine("Current Rentals:");

            foreach (var rentals in rentalPurchase)
            {
                Console.WriteLine(rentals.Movie.Title);
                Console.WriteLine();

            }

            Console.WriteLine("Enter the title of the movie you would like to purchase.");

            var purchase = Console.ReadLine();

            foreach (var rentals in rentalPurchase)
            {
                if (rentals.Movie.Title.ToLowerInvariant() == purchase.ToLowerInvariant())
                {
                    rentals.DueDate = DateTime.MaxValue;
                    _movieRepo.AddPurchase(purchase);
                    _movieRepo.RemoveFromInstock(purchase);
                    _movieRepo.RemoveFromAllMovies(purchase);
                    _rentalsRepo.RemoveRentals(rentals);
                }
            }
        }

        public void ReturnRental(Account account)
        {
            var returnRentals = _rentalsRepo.GetAccountRental(account.MemberNumber);

            Console.WriteLine();
            Console.WriteLine("Current Rentals:");
            Console.WriteLine();

            foreach (var returnRental in returnRentals)
            {
                Console.WriteLine(returnRental.Movie.Title);
            }

            Console.WriteLine();
            Console.WriteLine("Enter the title of the movie you wish to return.");

            var returnedMovie = Console.ReadLine();


            foreach (var returnRental in returnRentals)
            {
                if (returnRental.Movie.Title == returnedMovie)
                {
                    returnRental.DueDate = new DateTime();
                    _movieRepo.AddInstockMovies(returnedMovie);
                    _movieRepo.AddToAllMovies(returnedMovie);
                    _rentalsRepo.RemoveRentals(returnRental);
                }
            }



        }

        public void Exit(Account account)
        {
            Console.WriteLine();
            Console.WriteLine("Thank you for renting with us!");
        }

        public void Menu()
        {
            Console.WriteLine("Rental Menu");
            Console.WriteLine();
            Console.WriteLine("1. Rent");
            Console.WriteLine("2. Extend Rental");
            Console.WriteLine("3. Purchase Rental");
            Console.WriteLine("4. Purchased Movies");
            Console.WriteLine("5. Return Rental");
            Console.WriteLine("6. Exit");
            Console.WriteLine();
        }

    }
}
