using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business;
using MovieApp.Data;
using MovieApp.Models;

namespace RentalServicesWebApp.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult AllMovies()
        {

            string connection = System.IO.File.ReadAllText("C:\\Users\\jerus\\source\\repos\\MovieApp\\MovieApp\\configs.txt");

           
            var movie = new MovieRepo(connection);
            
            List<Movie> movies = movie.FetchAllMovies();


            return View("AllMovies", movies);
        }
    }
}
