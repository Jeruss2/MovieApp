using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MovieApp.Data
{
    public class RentalsRepo
    {

        private List<Rentals> _rentals;
        private string _connectionString;
        //private List<RentalsRepoTest> _rentalsTests;


        public RentalsRepo(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string was empty");
            }

            _connectionString = connectionString;

            _rentals = new List<Rentals>();
            //_rentalsTests = new List<RentalsRepoTest>();
        }


        //Gives me the list of all rentals
        public List<Rentals> GetRentals()
        {
            return _rentals;
        }

        //Gives me the list of rentals for each account
        public List<Rentals> GetAccountRental(string acctNumber)
        {
            return _rentals.Where(x => x.Account.MemberNumber == acctNumber).OrderBy(x => x.DueDate).ToList();

            //return _rentalsTests.Where(x => x.Account == acctNumber).OrderBy(x => x.DueDate).ToList();
        }

        public List<Rentals> FetchAccountRental(string acctNumber)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "Select * from dbo.Rentals where Account = @Account";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Account", System.Data.SqlDbType.VarChar, 50).Value = acctNumber;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();



                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //RentalsRepoTest rentals = new RentalsRepoTest();

                        Rentals rentals = new Rentals();

                        rentals.Account = new Account();
                        rentals.Movie = new Movie();


                        rentals.Account.MemberNumber = reader["Account"].ToString();
                        rentals.Movie.Title = reader["Movie"].ToString();
                        rentals.RentalTypes = reader.GetInt32(2);
                        rentals.RentalDate = reader.GetDateTime(3);
                        rentals.DueDate = reader.GetDateTime(4);


                        if (_rentals.All(m => m.Movie.Title != rentals.Movie.Title))
                        {
                            _rentals.Add(rentals);
                        }

                    }
                }
            }

            return _rentals;
        }

        //Adds rentals to the list
        public void AddRentals(Rentals rentalsList)
        {
            //_rentals.Add(rentalsList);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                //var sqlQuery = "Select * from dbo.Rentals";

                var sqlQuery = "Insert into dbo.Rentals(Account, Movie, RentalTypes, RentalDate, DueDate) values(@Account, @Movie, @RentalTypes, @RentalDate, @DueDate)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Account", System.Data.SqlDbType.VarChar, 50).Value = rentalsList.Account.MemberNumber;
                command.Parameters.Add("@Movie", System.Data.SqlDbType.VarChar, 50).Value = rentalsList.Movie.Title;
                command.Parameters.Add("@RentalTypes", System.Data.SqlDbType.Int, 50).Value = 1;
                command.Parameters.Add("@RentalDate", System.Data.SqlDbType.DateTime, 50).Value = rentalsList.RentalDate;
                command.Parameters.Add("@DueDate", System.Data.SqlDbType.DateTime, 50).Value = rentalsList.DueDate;



                connection.Open();
                command.ExecuteNonQuery();
            }

        }
        //Changing the repo for this one - testing it out to see if it works!
        //changed RentalRepo to RentalRepoTest
        public void RemoveRentals(Rentals rentalsList)
        {


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery = "Delete from dbo.Rentals where Movie = @Movie";

                SqlCommand command = new SqlCommand(sqlQuery, connection);


                command.Parameters.Add("@Movie", System.Data.SqlDbType.VarChar, 50).Value = rentalsList.Movie.Title;

                connection.Open();
                command.ExecuteNonQuery();
            }


            //_rentalsTests.Remove(_rentalsTests.FirstOrDefault(x =>
            //    x.Movie.ToLowerInvariant() == rentalsList.Movie.ToLowerInvariant()));

            _rentals.Remove(_rentals.FirstOrDefault(x =>
                x.Movie.Title.ToLowerInvariant() == rentalsList.Movie.Title.ToLowerInvariant()));
        }


        public void UpdateDueDate(string movieTitle, string memberNumber)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                var sqlQuery = "Update dbo.Rentals set DueDate = DATEADD(day, 7, DueDate) where Movie = @movieTitle and Account = @memberNumber";

                SqlCommand command = new SqlCommand(sqlQuery, connection);


                command.Parameters.Add("@movieTitle", System.Data.SqlDbType.VarChar, 50).Value = movieTitle;

                command.Parameters.Add("@memberNumber", System.Data.SqlDbType.VarChar, 50).Value = memberNumber;

                connection.Open();
                command.ExecuteNonQuery();
            }



        }



    }









}


