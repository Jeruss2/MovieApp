using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Data
{
    public class MovieRepo
    {

        private List<Movie> _movies;
        private List<Movie> _instockMovies;
        private List<Movie> _purchasedMovies;
        public MovieRepo()
        {

            _movies = new List<Movie>()
            {
                new Movie(){Title = "Troy", Director = "Wolfgang Petersen", ReleaseYear = 2004, RentalCost = 3, PurchaseCost = 5, InStock = true},
                new Movie(){Title = "Patriot", Director = "Roland Emmerich", ReleaseYear = 2000, RentalCost = 3, PurchaseCost = 5, InStock = true},
                new Movie(){Title = "Avatar", Director = "James Cameron", ReleaseYear = 2009, RentalCost = 5, PurchaseCost = 10, InStock = true},
                new Movie(){Title = "Jungle Cruise", Director = "Jaume Collet-Serra", ReleaseYear = 2021, RentalCost = 10, PurchaseCost = 20, InStock = true},
                new Movie(){Title = "Black Widow", Director = "Cate Strickland", ReleaseYear = 2021, RentalCost = 10, PurchaseCost = 20, InStock = true}
            };
            _instockMovies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "Troy", Director = "Wolfgang Petersen", ReleaseYear = 2004, RentalCost = 3,
                    PurchaseCost = 5, InStock = true
                },
                new Movie()
                {
                    Title = "Patriot", Director = "Roland Emmerich", ReleaseYear = 2000, RentalCost = 3,
                    PurchaseCost = 5, InStock = true
                },
                new Movie()
                {
                    Title = "Avatar", Director = "James Cameron", ReleaseYear = 2009, RentalCost = 5, PurchaseCost = 10,
                    InStock = true
                },
                new Movie()
                {
                    Title = "Jungle Cruise", Director = "Jaume Collet-Serra", ReleaseYear = 2021, RentalCost = 10,
                    PurchaseCost = 20, InStock = true
                },
                new Movie()
                {
                    Title = "Black Widow", Director = "Cate Strickland", ReleaseYear = 2021, RentalCost = 10,
                    PurchaseCost = 20, InStock = true
                }
            };

            _purchasedMovies = new List<Movie>();
        }

        public List<Movie> GetMovies()
        {
            return _movies;
        }

        public Movie GetMovie(string movieTitle)
        {
            return _movies.FirstOrDefault(x => x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant());
        }

        public void ShowMovieDetails(string movieTitle)
        {
            foreach (var movie in _movies)
            {
                if (movie.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant())
                {
                    Console.WriteLine($"Title: {movie.Title}");
                    Console.WriteLine($"Director: {movie.Director}");
                    Console.WriteLine($"Release Year: {movie.ReleaseYear}");
                    Console.WriteLine($"Rental Cost: ${movie.RentalCost}");
                    Console.WriteLine($"Purchase Cost: ${movie.PurchaseCost}");
                }
            }


        }

        public void RemoveFromAllMovies(string movieTitle)
        {
            _movies.Remove(_movies.FirstOrDefault(x => x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant()));
        }

        public void AddInstockMovies(string movieTitle)
        {
            _instockMovies.Add(_instockMovies.FirstOrDefault(x => x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant()));
        }

        public void RemoveFromInstock(string movieTitle)
        {
            _instockMovies.Remove(_instockMovies.FirstOrDefault(x => x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant()));
        }

        //Figure out a way to add movies that inStock = true; to this list.
        public List<Movie> InstockMovies()
        {
            return _instockMovies;
        }

        public void AddToAllMovies(string movieTitle)
        {
            _movies.Add(_movies.FirstOrDefault(x => x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant()));
        }

        public List<Movie> GetPurchaseList()
        {
            return _purchasedMovies;
        }

        public void AddPurchase(string movie)
        {
            var purchaseMovie = GetMovie(movie);

            if (purchaseMovie.Title.Equals(movie, StringComparison.CurrentCultureIgnoreCase))
            {
                _purchasedMovies.Add(purchaseMovie);
                RemoveFromInstock(movie);
                RemoveFromAllMovies(movie);
            }


        }

        public void AddNewMovies(Movie movie)
        {
            _movies.Add(movie);
            _instockMovies.Add(movie);
        }

    }
}
