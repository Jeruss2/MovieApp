namespace MovieApp.Models
{
    public class Account
    {
        public string MemberNumber { get; set; }
        public int Pin { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        //commented out to use app - Changed AccountTypes to string.

        //public AccountTypes AccountTypes { get; set; }

        public string AccountTypes { get; set; }

    }
}