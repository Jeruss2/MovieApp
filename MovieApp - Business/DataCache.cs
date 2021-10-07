using MovieApp.Data;
using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp___Business
{
    public class DataCache
    {
        private List<Movie> _movies;
        private MovieRepo _movieRepo;
        private RentalsRepo _rentalsRepo;


        public DataCache(MovieRepo movieRepo/*, RentalsRepo rentalsRepo*/)
        {
            _movies = new List<Movie>();
            _movieRepo = movieRepo;
            //_rentalsRepo = rentalsRepo;
        }


        public Movie FetchMovie(string title)
        {
            var m = _movies.SingleOrDefault(m => m.Title == title);

            //return m;
            if (m != null)
            {
                return m;
            }

            var movieFromDb = _movieRepo.FetchMovie(title);


            _movies.Add(movieFromDb);

            return movieFromDb;
        }


        public void UpdateMovie(Movie movie)
        {
            if (movie == null)
            {
                throw new Exception("Attempted to updated null movie");
            }

            var m = FetchMovie(movie.Title);

            m.Title = movie.Title;
            m.RentalCost = movie.RentalCost;
            m.InStock = movie.InStock;

            //_movieRepo.Edit
        }


    }
}
