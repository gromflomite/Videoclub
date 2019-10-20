using System;
using System.Data.SqlClient;

namespace Videoclub
{
    public class Clients
    {
        // Database connection---------------------------------------------------------------------------------------------------------------------------------
        private SqlConnection connection = new SqlConnection("Data Source=TRAVELMATE\\SQLEXPRESS;Initial Catalog=videoclub;Integrated Security=True");

        // Class attributes------------------------------------------------------------------------------------------------------------------------------------
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Dob { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        // Empty constructor-----------------------------------------------------------------------------------------------------------------------------------
        public Clients()
        {
        }

        // Constructor for user login--------------------------------------------------------------------------------------------------------------------------
        public Clients(string email, string password)
        {
            Email = email;
            Password = password;
        }

        // Constructor to get the unknown darkness-------------------------------------------------------------------------------------------------------------
        public Clients(int clientId, string name, string surname, string dob, string email, string password)
        {
            ClientId = clientId;
            Name = name;
            Surname = surname;
            Dob = dob;
            Email = email;
            Password = password;
        }

        // Constructor to register user------------------------------------------------------------------------------------------------------------------------
        public Clients(string name, string surname, string dob, string mail, string password)
        {
            Name = name;
            Surname = surname;
            Dob = dob;
            Email = mail;
            Password = password;
        }

        // User register method--------------------------------------------------------------------------------------------------------------------------------
        public bool Register()
        {
            // DB query to save data---------------------------------------------------------------------------------------------------------------------------
            string query = $"INSERT INTO Clients(name, surname, dob, email, password) VALUES ('{Name}','{Surname}','{Dob}','{Email}','{Password}')";

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

        // ----------------------------------------------------------------------------------------------------------------------------------------------------

        // User sign  method-----------------------------------------------------------------------------------------------------------------------------------
        public bool Signin(string email, string password)
        {
            // DB query to retrieve data-----------------------------------------------------------------------------------------------------------------------
            string query = $"SELECT * FROM clients WHERE email LIKE '{email}' AND password LIKE '{password}'";

            // try - catch block to check if database connection was ok ---------------------------------------------------------------------------------------
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ClientId = Convert.ToInt32(reader[0].ToString());
                    Name = reader[1].ToString();
                    Surname = reader[2].ToString();
                    Dob = reader[3].ToString();
                    Email = reader[4].ToString();
                    Password = reader[5].ToString();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t We had some problems connecting the database.");
                Console.WriteLine("\n\t Try again...");
                Console.ResetColor();

                return false;
            }
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------

        // Getting the age method------------------------------------------------------------------------------------------------------------------------------
        public int GetAge()
        {
            TimeSpan time = (DateTime.Now - Convert.ToDateTime(Dob));
            int age = time.Days / 365;

            return age;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------
    }
}