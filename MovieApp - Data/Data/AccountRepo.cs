using MovieApp.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MovieApp.Data
{
    public class AccountRepo
    {
        private List<Account> _accounts;
        private string _ConnectionString;

        public AccountRepo()
        {
            _accounts = new List<Account>()
            {
                //This is my lists - commented out to use SQL!


                //new Account(){MemberNumber = "123", Pin = 123, Name = "Josh", Balance = 0.00m, AccountTypes = AccountTypes.Admin},
                //new Account(){MemberNumber = "456", Pin = 456, Name = "Evan", Balance = 0.00m, AccountTypes = AccountTypes.Member}
            };

            _ConnectionString = @"Data Source=JERUSS2-DESKTOP\SQLEXPRESS01;Initial Catalog=Josh;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }



        public List<Account> FetchAllAccounts()
        {

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                var sqlQuery = "Select * from dbo.Account";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Account account = new Account();
                        account.MemberNumber = reader.GetString(0);
                        account.Pin = reader.GetInt32(1);
                        account.Name = reader.GetString(2);
                        account.Balance = reader.GetDecimal(3);

                        //Figure out how to retrive enum

                        account.AccountTypes = reader.GetString(4);

                        


                        _accounts.Add(account);
                    }
                }
            }

            return _accounts;

        }


        public Account FetchOne(string memberNumber, int pin)
        {


            // access the database
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string sqlQuery = "Select * from dbo.Account Where memberNumber = @MemberNumber and pin = @Pin";

                //associate @id with id parameter

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@MemberNumber", System.Data.SqlDbType.VarChar).Value = memberNumber;

                command.Parameters.Add("@Pin", System.Data.SqlDbType.Int).Value = pin;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Account account = new Account();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //create a new book object. Add it to the list to return.


                        account.MemberNumber = reader.GetString(0);
                        account.Pin = reader.GetInt32(1);
                        account.Name = reader.GetString(2);
                        account.Balance = reader.GetDecimal(3);

                        //Figure out how to retrive enum

                        account.AccountTypes = reader.GetString(4);



                    }
                }

                return account;
            }


        }


        public List<Account> GetAccounts()
        {
            return _accounts;
        }

        public Account GetAccount(string acctNumber, int pin)
        {
            return _accounts.FirstOrDefault(x => x.MemberNumber == acctNumber && x.Pin == pin);
        }

        public Account GetAcct(string acctNumber)
        {
            return _accounts.FirstOrDefault(x => x.MemberNumber == acctNumber);
        }

        public void Create(Account account)
        {

            // access the database
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                //string sqlQuery = "";

                string sqlQuery =
                     "Insert into dbo.Account Values(@MemberNumber, @Pin, @Name, @Balance, @AccountTypes)";
                //: "Update dbo.Account set Pin = @Pin, Name = @Name, Balance = @Balance, AccountTypes = @AccountTypes where MemberNumber = @MemberNumber";


                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@MemberNumber", System.Data.SqlDbType.VarChar, 50).Value = account.MemberNumber;
                command.Parameters.Add("@Pin", System.Data.SqlDbType.Int, 50).Value = account.Pin;
                command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, 50).Value = account.Name;
                command.Parameters.Add("@Balance", System.Data.SqlDbType.Decimal, 50).Value = account.Balance;
                command.Parameters.Add("@AccountTypes", System.Data.SqlDbType.VarChar, 50).Value = account.AccountTypes;



                connection.Open();
                command.ExecuteNonQuery();


            }


        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public void RemoveAccount(Account account)
        {
            _accounts.Remove(_accounts.FirstOrDefault(x => x.MemberNumber == account.MemberNumber));
        }


        public void Delete(Account account)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string sqlQuery = "Delete from dbo.Account where MemberNumber = @MemberNumber";


                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@MemberNumber", System.Data.SqlDbType.VarChar, 1000).Value = account.MemberNumber;

                connection.Open();
                command.ExecuteNonQuery();


            }
        }


    }
}