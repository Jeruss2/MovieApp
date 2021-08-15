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



            while (userInput != "9")
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
                        BrowseMovies();
                        ShowMovies();
                        break;
                    case "2":
                        ShowMovies();
                        RentMovie(userAccount);
                        break;
                    case "3":
                        ExtendRental(userAccount);
                        break;
                    case "4":
                        PurchaseRental(userAccount);
                        break;
                    case "5":
                        ShowPurchasedMovies(userAccount);
                        break;
                    case "6":
                        ReturnRental(userAccount);
                        break;
                    case "7":
                        AccountBalance(userAccount);
                        break;
                    case "8":
                        Logout();
                        break;
                    case "9":
                        Exit();
                        break;

                }
            }
        }

        public void AccountBalance(Account account)
        {
            Console.WriteLine();
            Console.WriteLine($"Account Balance: ${account.Balance}");
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
        //needs work - maybe a while statement to repeat until user is ready to move on
        public void BrowseMovies()
        {
            var input = "";


            Console.WriteLine();
            Console.WriteLine("Enter the title of the movie you would like to browse.");

            input = Console.ReadLine();

            _movieRepo.ShowMovieDetails(input);
            Console.WriteLine("Press any key to view list of movies");
            Console.ReadKey();
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

            rent.Account.Balance += rent.Movie.RentalCost;

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
                    rental.Account.Balance += rental.Movie.RentalCost;
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
                    rentals.Account.Balance += rentals.Movie.PurchaseCost;
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

        public void Logout()
        {
            Exit();

            var service = new MovieRentalService();

            Account account = service.Login();

            service.RentalLoop(account);
        }

        public void Exit()
        {
            Console.WriteLine();
            Console.WriteLine("Thank you for renting with us!");
            Console.WriteLine();
        }

        public void Menu()
        {
            Console.WriteLine("Rental Menu");
            Console.WriteLine();
            Console.WriteLine("1. Browse Movies");
            Console.WriteLine("2. Rent");
            Console.WriteLine("3. Extend Rental");
            Console.WriteLine("4. Purchase Rental");
            Console.WriteLine("5. Purchased Movies");
            Console.WriteLine("6. Return Rental");
            Console.WriteLine("7. Account Balance");
            Console.WriteLine("8. Logout");
            Console.WriteLine("9. Exit");
            Console.WriteLine();
        }

    }
}
