namespace MovieApp.Models
{
    public interface IConnectionStrings
    {
        string SQLServerConnection { get; set; }
    }

    public class ConnectionStrings : IConnectionStrings
    {
        public string SQLServerConnection { get; set; }
    }
}