using System;

namespace MovieApp.Models
{
    public class Rentals
    {

        public Account Account { get; set; }
        public Movie Movie { get; set; }
        //public RentalTypes RentalTypes { get; set; }
        public int RentalTypes { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public string AccountId { get; set; }
        public string MovieId { get; set; }




    }
}