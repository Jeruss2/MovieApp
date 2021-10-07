using MovieApp.Data;
using MovieApp.Models;
using MovieApp___Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MovieApp.Business
{
    public class MovieRentalService
    {
        private AccountRepo _accountRepo;
        private MovieRepo _movieRepo;
        private RentalsRepo _rentalsRepo;



        public MovieRentalService(AccountRepo accountRepo, MovieRepo movieRepo, RentalsRepo rentalsRepo)
        {
            _accountRepo = accountRepo;
            _movieRepo = movieRepo;
            _rentalsRepo = rentalsRepo;

        }

        //Need to fix login to exit after number of tries

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

                //account = _accountRepo.GetAccount(memNumber, acctPin);

                account = _accountRepo.FetchOne(memNumber, acctPin);


                if (account != null)
                {
                    return account;
                }

                if (i > 2)
                {
                    Console.WriteLine("You have reach the maximum number of login attempts");
                    //break;
                    Exit();
                }

                i++;

            }

            return account;
        }

        public void RentalLoop(Account userAccount)
        {
            var userInput = "";

            if (userAccount.AccountTypes == /*AccountTypes.Admin*/  "Admin")
            {
                while (userInput != "6")
                {
                    Console.WriteLine();
                    Console.WriteLine($"Hi {userAccount.Name}!");
                    Console.WriteLine();

                    AdminMenu();

                    userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            CreateAccount();
                            break;
                        case "2":
                            DeleteAccount();
                            break;
                        case "3":
                            ViewAccounts();
                            break;
                        case "4":
                            EditAccount();
                            break;
                        case "5":
                            AddMovie();
                            break;
                        case "6":
                            continue;
                    }

                }

            }

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
                        //ShowMovies();
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
                        userInput = "9";
                        break;
                    case "9":
                        Exit();
                        break;
                }
            }


        }

        public void CreateAccount()
        {
            var memberAccount = new Account();

            Console.WriteLine("Create a new Member Account");
            Console.WriteLine();
            Console.WriteLine("Enter Account Number");
            memberAccount.MemberNumber = Console.ReadLine();

            Console.WriteLine("Enter Member Name");
            memberAccount.Name = Console.ReadLine();

            Console.WriteLine("Enter Account Type");

            memberAccount.AccountTypes = Console.ReadLine();

            // memberAccount.AccountTypes = accountType.ToLowerInvariant() == "admin" ? AccountTypes.Admin : AccountTypes.Member;

            Console.WriteLine("Enter Pin");

            memberAccount.Pin = int.Parse(Console.ReadLine());

            memberAccount.Balance = 0.00m;

            //_accountRepo.AddAccount(memberAccount);

            _accountRepo.Create(memberAccount);

        }

        public void DeleteAccount()
        {
            Console.WriteLine();
            Console.WriteLine("What is the Member Number you want to remove");

            var deleteAccount = Console.ReadLine();

            var allAccount = _accountRepo.FetchSingleAccount(deleteAccount);

            //_accountRepo.RemoveAccount(allAccount);

            _accountRepo.Delete(allAccount);

        }

        public void ViewAccounts()
        {
            //var accountsList = _accountRepo.GetAccounts();

            var accountsList = _accountRepo.FetchAllAccounts().GroupBy(x => x.MemberNumber).Select(y => y.First());


            foreach (var account in accountsList)
            {
                Console.WriteLine();
                Console.WriteLine($"Member Number: {account.MemberNumber}, Name on Account: {account.Name}, Pin: {account.Pin}, Account Type: {account.AccountTypes}, Balance: ${account.Balance}");
                Console.WriteLine();
            }
        }

        public void AccountBalance(Account account)
        {
            Console.WriteLine();
            Console.WriteLine($"Account Balance: ${account.Balance}");
        }


        //!! this works
        public void AddMovie()
        {
            var movie = new Movie();

            Console.WriteLine("Movie Title");
            movie.Title = Console.ReadLine();

            Console.WriteLine("Movie Director");
            movie.Director = Console.ReadLine();

            Console.WriteLine("Release Year");
            movie.ReleaseYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Rental Cost");
            movie.RentalCost = int.Parse(Console.ReadLine());

            Console.WriteLine("Purchase Cost");
            movie.PurchaseCost = int.Parse(Console.ReadLine());

            movie.InStock = true;

            //_movieRepo.AddNewMovies(movie);
            _movieRepo.CreateNewMovie(movie);
        }

        public void EditAccount()
        {
            Console.WriteLine("Enter the Member Number of the account you would like to edit?");

            var editAccount = Console.ReadLine();

            //var accountsList = _accountRepo.GetAccounts();

            var accountsList = _accountRepo.FetchSingleAccount(editAccount);

            //foreach (var account in accountsList.Where(account => editAccount == account.MemberNumber))
            //{
            Console.WriteLine("What do you want the member number to be?");
            accountsList.MemberNumber = Console.ReadLine();

            Console.WriteLine("What name do you want on the account?");
            accountsList.Name = Console.ReadLine();

            Console.WriteLine("What pin do you want for the account?");
            accountsList.Pin = int.Parse(Console.ReadLine());

            Console.WriteLine("What balance do you want on the account?");
            accountsList.Balance = decimal.Parse(Console.ReadLine());

            Console.WriteLine("What type of account should this be?");
            accountsList.AccountTypes = Console.ReadLine();

            //account.AccountTypes = accountType.ToLowerInvariant() == "Admin".ToLowerInvariant() ? AccountTypes.Admin : AccountTypes.Member;


            _accountRepo.Edit(accountsList);
            //}



        }

        public void ShowMovies()
        {

            List<Movie> instockMovies = _movieRepo.FetchInstockMovies();

            Console.WriteLine();
            Console.WriteLine("Movies Available:");

            foreach (var instockMovie in instockMovies.Select(x => x.Title).Distinct())
            {
                Console.WriteLine(instockMovie);
            }

            Console.WriteLine();


            List<Movie> movies = _movieRepo.FetchAllMovies();


            Console.WriteLine("All Movies:");

            foreach (var movie in movies.Select(x => x.Title).Distinct())
            {

                Console.WriteLine(movie);

            }

            Console.WriteLine();

        }
        //needs work - maybe a while statement to repeat until user is ready to move on
        public void BrowseMovies()
        {
            var input = "";

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter the title of the movie you would like to browse.");

                input = Console.ReadLine();

                Console.WriteLine();

                var movie = _movieRepo.GetMovieObj(input);

                if (string.IsNullOrWhiteSpace(movie.Title))
                {
                    Console.WriteLine("The movie you have selected is not on our list.");

                }
                else
                {
                    _movieRepo.DisplayMovieDetails(movie);
                    Console.WriteLine("Press any key to view list of movies");
                    Console.ReadKey();
                }


                if (input == "Exit")
                {
                    break;
                }
            }

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

            rent.RentalDate = DateTime.Now;
            rent.DueDate = DateTime.Now.AddDays(7);

            // do while - compare to list of movies

            var instock = _movieRepo.FetchinstockMovieList() ?? _movieRepo.FetchInstockMovies();


            var matches = "";


            while (true)
            {
                Console.WriteLine("Enter the title of the movie you would like to rent");

                movieSelect = Console.ReadLine();

                // matches = instock.Where(x => x.Title == movieSelect).ToString();

                if (instock.Any(x => x.Title.Equals(movieSelect, StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }

            }


            DataCache dc = new DataCache(_movieRepo);

            rent.Movie = dc.FetchMovie(movieSelect);

            account.Balance += rent.Movie.RentalCost;

            _rentalsRepo.AddRentals(rent);

            var myRentals = _rentalsRepo.FetchAccountRental(account.MemberNumber);

            //FetchAccountRental

            _movieRepo.RemoveFromInstock(rent.Movie);


            _accountRepo.Edit(rent.Account);


            Console.WriteLine();
            Console.WriteLine("Current Rentals:");

            foreach (var rental in myRentals.Where(x => x.Account.MemberNumber == account.MemberNumber).Distinct())
            {
                Console.WriteLine();
                Console.WriteLine($"Movie: {rental.Movie.Title} Due Date: {rental.DueDate}");
                Console.WriteLine();
            }


        }

        public void ExtendRental(Account account)
        {
            var rentalExtension = _rentalsRepo.GetAccountRental(account.MemberNumber);

            if (rentalExtension.Count == 0)
            {
                rentalExtension = _rentalsRepo.FetchAccountRental(account.MemberNumber);
            }


            Console.WriteLine();
            Console.WriteLine("Current Rentals:");

            foreach (var rentals in rentalExtension.Where(i => i.Account.MemberNumber == account.MemberNumber).Select(x => x.Movie.Title).Distinct().ToList())
            {
                Console.WriteLine(rentals);
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
                    _rentalsRepo.UpdateDueDate(extend, account.MemberNumber);
                }

                var title = rental.Movie.Title;
                var dueDate = rental.DueDate;

                Console.WriteLine($"Title: {title} Due Date: {dueDate}");
                Console.WriteLine();
            }



        }

        public void PurchaseRental(Account account)
        {
            //var rentalPurchase = _rentalsRepo.GetAccountRental(account.MemberNumber);

            var rentalPurchase = _rentalsRepo.FetchAccountRental(account.MemberNumber);


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
                    _movieRepo.InsertIntoPurchase(rentals.Movie);
                    _movieRepo.RemoveFromInstock(rentals.Movie);
                    _movieRepo.RemoveFromAllMovies(rentals.Movie);
                    _rentalsRepo.RemoveRentals(rentals);

                    var movie = _movieRepo.GetMovieObj(purchase);

                    _movieRepo.DeleteFromAllMovies(movie);
                }
            }
        }

        public void ReturnRental(Account account)
        {
            //var returnRentals = _rentalsRepo.FetchAccountRental(account.MemberNumber);

            var returnRentals = _rentalsRepo.FetchAccountRental(account.MemberNumber);


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

            var movie = _movieRepo.GetMovieObj(returnedMovie);


            foreach (var returnRental in returnRentals)
            {
                if (returnRental.Movie.Title == returnedMovie)
                {
                    returnRental.DueDate = new DateTime();
                    _movieRepo.InsertIntoInstockMovies(movie);
                    //_movieRepo.AddToAllMovies(returnedMovie);
                    _rentalsRepo.RemoveRentals(returnRental);

                }
            }

            var theMovie = returnRentals.Find(x => x.Movie.Title == returnedMovie);

            returnRentals.Remove(theMovie);

        }

        public void Logout()
        {
            Exit();

            RentalLoop(Login());
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

        public void AdminMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Delete Account");
            Console.WriteLine("3. View Accounts");
            Console.WriteLine("4. Edit Accounts");
            Console.WriteLine("5. Add Movies");
            Console.WriteLine("6. Continue to Rentals");
            Console.WriteLine();
        }


    }
}
