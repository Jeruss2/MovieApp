using System.Collections.Generic;
using System.Linq;
using MovieApp.Models;

namespace MovieApp.Data
{
    public class RentalsRepo
    {

        private List<Rentals> _rentals;
        

        public RentalsRepo()
        {
            _rentals = new List<Rentals>();
        }


        //Gives me the list of all rentals
        public List<Rentals> GetRentals()
        {
            return _rentals;
        }

        //Gives me the list of rentals for each account
        public List<Rentals> GetAccountRental(string acctNumber)
        {
            return _rentals.Where(x => x.Account.MemberNumber == acctNumber).OrderBy(x=>x.DueDate).ToList();
        }

        //Adds rentals to the list
        public void AddRentals(Rentals rentalsList)
        {
            _rentals.Add(rentalsList);
        }

        public void RemoveRentals(Rentals rentalsList)
        {
            _rentals.Remove(_rentals.FirstOrDefault(x => x.Movie.Title.ToLowerInvariant() == rentalsList.Movie.Title.ToLowerInvariant()));
        }
    }
}