using System;
using System.Data.SqlClient;

namespace Videoclub
{
    internal class Rental
    {
        // Database connection---------------------------------------------------------------------------------------------------------------------------------
        private SqlConnection connection = new SqlConnection("Data Source=TRAVELMATE\\SQLEXPRESS;Initial Catalog=videoclub;Integrated Security=True");

        // Class attributes------------------------------------------------------------------------------------------------------------------------------------

        public int RentalId { get; set; }

        public int ClientId { get; set; }

        public int FilmId { get; set; }

        public DateTime RentalStartDate { get; set; }

        public DateTime RentalEndDate { get; set; }

        public char Returned { get; set; }

        public Rental(int filmId)
        {
            FilmId = filmId;
        }

        public bool RentFilm(int filmId)
        {
            string query = $"INSERT INTO rentals (filmid, rentalstartdate, returned) VALUES ({filmId},'{DateTime.Now.Date})', 'N')";

            // try - catch block to check if database connection was ok ---------------------------------------------------------------------------------------
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t We had some problems connecting to the database.");
                Console.WriteLine("\n\t Try again...");
                Console.ResetColor();

                return false;
            }
        }
    }
}