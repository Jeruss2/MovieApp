using System;

namespace MovieApp.Models
{
    public class Rentals
    {

        public RentalTypes RentalTypes { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public Account Account { get; set; }
        public Movie Movie { get; set; }





    }
}