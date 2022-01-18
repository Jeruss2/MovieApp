using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class Movie
    {
        public string Title { get; set; }
        
        public string Director { get; set; }
        
        public int ReleaseYear { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public int RentalCost { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        
        public int PurchaseCost { get; set; }
        
        public bool InStock { get; set; }
    }
}
