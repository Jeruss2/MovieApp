using System;
using MovieApp.Models;
using System.Data.SqlClient;

namespace MovieApp___Data
{
    public static class DataExtensions
    {
        public static T GetValue<T>(this SqlDataReader reader, string columnName)
        {
            int index = reader.GetOrdinal(columnName); // read column index

            return reader.IsDBNull(index) ? default(T) : (T)reader.GetValue(index);
        }

        public static string GetTotleAndDirtector(this Movie movie)
        {


            return movie.Title + " " + movie.Director;
        }

        public static int ReturnBigger(IComparable someObject, IComparable someObject2)
        {
            return someObject.CompareTo(someObject2);
        }
    }
}
