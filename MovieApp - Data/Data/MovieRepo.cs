using MovieApp.Models;
using MovieApp___Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MovieApp.Data
{
    public class MovieRepo
    {

        private List<Movie> _movies;
        private List<Movie> _instockMovies;
        private List<Movie> _purchasedMovies;
        private string _connectionString;


        public MovieRepo(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string was empty");
            }

            _connectionString = connectionString;
            _movies = new List<Movie>();
            _instockMovies = new List<Movie>();
            _purchasedMovies = new List<Movie>();
        }

        //public MovieRepo()
        //{

        //    _movies = new List<Movie>()
        //    {
        //        //new Movie(){Title = "Troy", Director = "Wolfgang Petersen", ReleaseYear = 2004, RentalCost = 3, PurchaseCost = 5, InStock = true},
        //        //new Movie(){Title = "Patriot", Director = "Roland Emmerich", ReleaseYear = 2000, RentalCost = 3, PurchaseCost = 5, InStock = true},
        //        //new Movie(){Title = "Avatar", Director = "James Cameron", ReleaseYear = 2009, RentalCost = 5, PurchaseCost = 10, InStock = true},
        //        //new Movie(){Title = "Jungle Cruise", Director = "Jaume Collet-Serra", ReleaseYear = 2021, RentalCost = 10, PurchaseCost = 20, InStock = true},
        //        //new Movie(){Title = "Black Widow", Director = "Cate Strickland", ReleaseYear = 2021, RentalCost = 10, PurchaseCost = 20, InStock = true}
        //    };
        //    _instockMovies = new List<Movie>()
        //    {
        //        new Movie()
        //        {
        //            Title = "Troy", Director = "Wolfgang Petersen", ReleaseYear = 2004, RentalCost = 3,
        //            PurchaseCost = 5, InStock = true
        //        },
        //        new Movie()
        //        {
        //            Title = "Patriot", Director = "Roland Emmerich", ReleaseYear = 2000, RentalCost = 3,
        //            PurchaseCost = 5, InStock = true
        //        },
        //        new Movie()
        //        {
        //            Title = "Avatar", Director = "James Cameron", ReleaseYear = 2009, RentalCost = 5, PurchaseCost = 10,
        //            InStock = true
        //        },
        //        new Movie()
        //        {
        //            Title = "Jungle Cruise", Director = "Jaume Collet-Serra", ReleaseYear = 2021, RentalCost = 10,
        //            PurchaseCost = 20, InStock = true
        //        },
        //        new Movie()
        //        {
        //            Title = "Black Widow", Director = "Cate Strickland", ReleaseYear = 2021, RentalCost = 10,
        //            PurchaseCost = 20, InStock = true
        //        }
        //    };

        //    _purchasedMovies = new List<Movie>();

        //    //_connectionString = @"Data Source=JERUSS2-DESKTOP\SQLEXPRESS01;Initial Catalog=Josh;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //}

        //public List<Movie> GetMovies()
        //{
        //    return _movies;
        //}

        public List<Movie> FetchAllMovies()
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "Select * from dbo.Movie";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Movie movie = new Movie();

                        movie.Title = reader.GetString(0);
                        movie.Director = reader.GetString(1);
                        movie.ReleaseYear = reader.GetInt32(2);
                        movie.RentalCost = reader.GetInt32(3);
                        movie.PurchaseCost = reader.GetInt32(4);
                        movie.InStock = reader.GetBoolean(5);


                        _movies.Add(movie);

                    }
                }
            }

            return _movies;

        }

        //public Movie GetMovie(string movieTitle)
        //{
        //    return _movies.FirstOrDefault(x => x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant());
        //}

        public Movie FetchMovie(string movieTitle)
        {
            //Movie movie = new Movie();
            var m = new Movie();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "Select * from dbo.Movie where Title = @Title";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Title", SqlDbType.VarChar).Value = movieTitle;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows && reader.Read())
                {
                    m = ReadMovie(reader);

                    return m;
                    //movie.Title = reader.GetString(0);
                    //movie.Director = reader.GetString(1);
                    //movie.ReleaseYear = reader.GetInt32(2);
                    //movie.RentalCost = reader.GetInt32(3);
                    //movie.PurchaseCost = reader.GetInt32(4);
                    //movie.InStock = reader.GetBoolean(5);

                    //_movies.Add(movie);

                }

            }


            //return _movies.FirstOrDefault(x => x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant());


            //return movie;

            return m;

        }


        public void DisplayMovieDetails(Movie movie)
        {
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Director: {movie.Director}");
            Console.WriteLine($"Release Year: {movie.ReleaseYear}");
            Console.WriteLine($"Rental Cost: ${movie.RentalCost}");
            Console.WriteLine($"Purchase Cost: ${movie.PurchaseCost}");

        }


        //public void ShowMovieDetails(string movieTitle)
        //{
        //    foreach (var movie in _movies)
        //    {
        //        if (movie.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant())
        //        {
        //            Console.WriteLine($"Title: {movie.Title}");
        //            Console.WriteLine($"Director: {movie.Director}");
        //            Console.WriteLine($"Release Year: {movie.ReleaseYear}");
        //            Console.WriteLine($"Rental Cost: ${movie.RentalCost}");
        //            Console.WriteLine($"Purchase Cost: ${movie.PurchaseCost}");
        //        }
        //    }


        //}

        //public void RemoveFromAllMovies(string movieTitle)
        //{
        //    _movies.Remove(_movies.FirstOrDefault(x => x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant()));
        //}
        public void AddToAllMovies(Movie movie)
        {
            string titleMovie = movie.Title;
            string director = movie.Director;
            int releaseYear = movie.ReleaseYear;
            int rentalCost = movie.RentalCost;
            int purchaseCost = movie.RentalCost;
            bool instock = movie.InStock;


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery = "Insert into dbo.Movie values (@Title, @Director, @ReleaseYear, @RentalCost, @PurchaseCost, @Instock)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = titleMovie;
                command.Parameters.Add("@Director", System.Data.SqlDbType.VarChar).Value = director;
                command.Parameters.Add("@ReleaseYear", System.Data.SqlDbType.Int).Value = releaseYear;
                command.Parameters.Add("@RentalCost", System.Data.SqlDbType.Int).Value = rentalCost;
                command.Parameters.Add("@PurchaseCost", System.Data.SqlDbType.Int).Value = purchaseCost;
                command.Parameters.Add("@Instock", System.Data.SqlDbType.Int).Value = instock;


                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        public void DeleteFromAllMovies(Movie movie)
        {
            string titleMovie = movie.Title;


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery = "Delete from dbo.Movie where Title = @Title";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = titleMovie;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //public void AddInstockMovies(string movieTitle)
        //{
        //    //_instockMovies.Add(_instockMovies.FirstOrDefault(x =>
        //    //    x.Title.ToLowerInvariant() == movieTitle.ToLowerInvariant()));

        //    string titleMovie = "";
        //    string director = "";
        //    int releaseYear = 0;
        //    int rentalCost = 0;
        //    int purchaseCost = 0;
        //    bool instock = true;



        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {

        //        var sqlQuery1 = "Insert into dbo.InstockMovies values (@Title, @Director, @ReleaseYear, @RentalCost, @PurchaseCost, @Instock)";

        //        SqlCommand command1 = new SqlCommand(sqlQuery1, connection);

        //        command1.Parameters.Add("@TitleMovie", System.Data.SqlDbType.VarChar).Value = titleMovie;
        //        command1.Parameters.Add("@Director", System.Data.SqlDbType.VarChar).Value = director;
        //        command1.Parameters.Add("@ReleaseYear", System.Data.SqlDbType.Int).Value = releaseYear;
        //        command1.Parameters.Add("@RentalCost", System.Data.SqlDbType.Int).Value = rentalCost;
        //        command1.Parameters.Add("@PurchaseCost", System.Data.SqlDbType.Int).Value = purchaseCost;
        //        command1.Parameters.Add("@Instock", System.Data.SqlDbType.Int).Value = instock;



        //        var sqlQuery = "Select * from dbo.Movies where Title = @movieTitle";

        //        SqlCommand command = new SqlCommand(sqlQuery, connection);


        //        command.Parameters.Add("@movieTitle", System.Data.SqlDbType.VarChar).Value = movieTitle;

        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                //Movie movie = new Movie();
        //                //movie.Title = reader.GetString(0);
        //                //movie.Director = reader.GetString(1);
        //                //movie.ReleaseYear = reader.GetInt32(2);
        //                //movie.RentalCost = reader.GetInt32(3);
        //                //movie.PurchaseCost = reader.GetInt32(4);
        //                //movie.InStock = reader.GetBoolean(5);


        //                //_instockMovies.Add(movie);



        //                titleMovie = reader.GetString(0);
        //                director = reader.GetString(1);
        //                releaseYear = reader.GetInt32(2);
        //                rentalCost = reader.GetInt32(3);
        //                purchaseCost = reader.GetInt32(4);
        //                instock = reader.GetBoolean(5);

        //                command1.ExecuteNonQuery();


        //            }
        //        }


        //    }
        //}

        public void InsertIntoInstockMovies(Movie movie)
        {
            string titleMovie = movie.Title;
            string director = movie.Director;
            int releaseYear = movie.ReleaseYear;
            int rentalCost = movie.RentalCost;
            int purchaseCost = movie.RentalCost;
            bool instock = movie.InStock;


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery = "Insert into dbo.InstockMovies values (@Title, @Director, @ReleaseYear, @RentalCost, @PurchaseCost, @Instock)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = titleMovie;
                command.Parameters.Add("@Director", System.Data.SqlDbType.VarChar).Value = director;
                command.Parameters.Add("@ReleaseYear", System.Data.SqlDbType.Int).Value = releaseYear;
                command.Parameters.Add("@RentalCost", System.Data.SqlDbType.Int).Value = rentalCost;
                command.Parameters.Add("@PurchaseCost", System.Data.SqlDbType.Int).Value = purchaseCost;
                command.Parameters.Add("@Instock", System.Data.SqlDbType.Int).Value = instock;


                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        public Movie GetMovieObj(string movieTitle)
        {

            var movie = new Movie();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery = "Select * from dbo.MasterListOfMovies where Title = @movieTitle";

                SqlCommand command = new SqlCommand(sqlQuery, connection);


                command.Parameters.Add("@movieTitle", System.Data.SqlDbType.VarChar).Value = movieTitle;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();



                if (reader.HasRows)
                {
                    while (reader.Read())
                    {


                        movie.Title = reader.GetString(0);
                        movie.Director = reader.GetString(1);
                        movie.ReleaseYear = reader.GetInt32(2);
                        movie.RentalCost = reader.GetInt32(3);
                        movie.PurchaseCost = reader.GetInt32(4);
                        movie.InStock = reader.GetBoolean(5);


                    }
                }


            }

            return movie;


        }

        public void RemoveFromInstock(Movie movie)
        {

            string titleMovie = movie.Title;


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery = "Delete from dbo.InstockMovies where Title = @Title";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = titleMovie;

                connection.Open();
                command.ExecuteNonQuery();

            }


            var findTitle = _instockMovies.Find(x => x.Title == titleMovie);

            _instockMovies.Remove(findTitle);

        }

        public void RemoveFromAllMovies(Movie movie)
        {

            string titleMovie = movie.Title;


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery = "Delete from dbo.Movie where Title = @Title";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = titleMovie;

                connection.Open();
                command.ExecuteNonQuery();

            }


            var findTitle = _instockMovies.Find(x => x.Title == titleMovie);

            _movies.Remove(findTitle);

        }


        //Need to look at this one to return all w/o list.
        public List<Movie> FetchInstockMovies()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "Select * from dbo.InstockMovies";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Movie movie = new Movie();
                        movie.Title = reader.GetString(0);
                        movie.Director = reader.GetString(1);
                        movie.ReleaseYear = reader.GetInt32(2);
                        movie.RentalCost = reader.GetInt32(3);
                        movie.PurchaseCost = reader.GetInt32(4);
                        movie.InStock = reader.GetBoolean(5);

                        _instockMovies.Add(movie);
                    }
                }
            }

            return _instockMovies;
        }

        //Need to look at this one to return all w/o list.
        public List<Movie> GetPurchaseList()
        {
            return _purchasedMovies;
        }


        //public void AddPurchase(string movie)
        //{
        //    var purchaseMovie = GetMovie(movie);

        //    if (purchaseMovie.Title.Equals(movie, StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        _purchasedMovies.Add(purchaseMovie);
        //        RemoveFromInstock(movie);
        //        RemoveFromAllMovies(movie);
        //    }


        //}

        public void InsertIntoPurchase(Movie movie)
        {
            string titleMovie = movie.Title;
            string director = movie.Director;
            int releaseYear = movie.ReleaseYear;
            int rentalCost = movie.RentalCost;
            int purchaseCost = movie.RentalCost;
            bool instock = movie.InStock;


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery =
                    "Insert into dbo.PurchasedMovies values (@Title, @Director, @ReleaseYear, @RentalCost, @PurchaseCost, @Instock)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = titleMovie;
                command.Parameters.Add("@Director", System.Data.SqlDbType.VarChar).Value = director;
                command.Parameters.Add("@ReleaseYear", System.Data.SqlDbType.Int).Value = releaseYear;
                command.Parameters.Add("@RentalCost", System.Data.SqlDbType.Int).Value = rentalCost;
                command.Parameters.Add("@PurchaseCost", System.Data.SqlDbType.Int).Value = purchaseCost;
                command.Parameters.Add("@Instock", System.Data.SqlDbType.Int).Value = instock;


                connection.Open();
                command.ExecuteNonQuery();


            }
        }

        public void AddNewMovies(Movie movie)
        {
            _movies.Add(movie);
            _instockMovies.Add(movie);
        }

        //Need to refactor this - really ugly right now
        public void CreateNewMovie(Movie movie)
        {
            // access the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                string sqlQuery =
                    "Insert into dbo.Movie Values(@Title, @Director, @ReleaseYear, @RentalCost, @PurchaseCost, @Instock)";
                string sqlQuery1 =
                    "Insert into dbo.MasterListOfMovies Values(@Title, @Director, @ReleaseYear, @RentalCost, @PurchaseCost, @Instock)";
                string sqlQuery2 =
                    "Insert into dbo.InstockMovies Values(@Title, @Director, @ReleaseYear, @RentalCost, @PurchaseCost, @Instock)";


                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlCommand command1 = new SqlCommand(sqlQuery1, connection);
                SqlCommand command2 = new SqlCommand(sqlQuery2, connection);

                command.Parameters.Add("@Title", System.Data.SqlDbType.VarChar, 50).Value = movie.Title;
                command.Parameters.Add("@Director", System.Data.SqlDbType.VarChar, 50).Value = movie.Director;
                command.Parameters.Add("@ReleaseYear", System.Data.SqlDbType.Int, 50).Value = movie.ReleaseYear;
                command.Parameters.Add("@RentalCost", System.Data.SqlDbType.Int, 50).Value = movie.RentalCost;
                command.Parameters.Add("@PurchaseCost", System.Data.SqlDbType.Int, 50).Value = movie.PurchaseCost;
                command.Parameters.Add("@Instock", System.Data.SqlDbType.Bit, 50).Value = movie.InStock;

                command1.Parameters.Add("@Title", System.Data.SqlDbType.VarChar, 50).Value = movie.Title;
                command1.Parameters.Add("@Director", System.Data.SqlDbType.VarChar, 50).Value = movie.Director;
                command1.Parameters.Add("@ReleaseYear", System.Data.SqlDbType.Int, 50).Value = movie.ReleaseYear;
                command1.Parameters.Add("@RentalCost", System.Data.SqlDbType.Int, 50).Value = movie.RentalCost;
                command1.Parameters.Add("@PurchaseCost", System.Data.SqlDbType.Int, 50).Value = movie.PurchaseCost;
                command1.Parameters.Add("@Instock", System.Data.SqlDbType.Bit, 50).Value = movie.InStock;
                
                
                command2.Parameters.Add("@Title", System.Data.SqlDbType.VarChar, 50).Value = movie.Title;
                command2.Parameters.Add("@Director", System.Data.SqlDbType.VarChar, 50).Value = movie.Director;
                command2.Parameters.Add("@ReleaseYear", System.Data.SqlDbType.Int, 50).Value = movie.ReleaseYear;
                command2.Parameters.Add("@RentalCost", System.Data.SqlDbType.Int, 50).Value = movie.RentalCost;
                command2.Parameters.Add("@PurchaseCost", System.Data.SqlDbType.Int, 50).Value = movie.PurchaseCost;
                command2.Parameters.Add("@Instock", System.Data.SqlDbType.Bit, 50).Value = movie.InStock;

                connection.Open();
                command.ExecuteNonQuery();
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
            }

        }

        private Movie ReadMovie(SqlDataReader reader)
        {
            Movie movie = new Movie();

            //movie.GetTotleAndDirtector();

            movie.Title = reader.GetValue<string>("Title");
            movie.Director = reader.GetString(1);
            movie.ReleaseYear = reader.GetInt32(2);
            movie.RentalCost = reader.GetInt32(3);
            movie.PurchaseCost = reader.GetInt32(4);
            movie.InStock = reader.GetBoolean(5);

            return movie;
        }
    }
}