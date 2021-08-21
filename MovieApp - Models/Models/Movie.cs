namespace MovieApp.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public int RentalCost { get; set; }
        public int PurchaseCost { get; set; }
        public bool InStock { get; set; }
    }
}
