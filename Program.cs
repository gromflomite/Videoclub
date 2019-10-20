using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Videoclub
{
    internal class Program
    {
        // Database connection---------------------------------------------------------------------------------------------------------------------------------
        private static SqlConnection connection = new SqlConnection("Data Source=TRAVELMATE\\SQLEXPRESS;Initial Catalog=videoclub;Integrated Security=True");

        // Calling the main menu-------------------------------------------------------------------------------------------------------------------------------
        public static void Main(string[] args)
        {
            WelcomeMenu();
        }

        // Welcome menu---------------------------------------------------------------------------------------------------------------------------------------
        public static void WelcomeMenu()
        {
            Console.Clear();
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\t**********  V I D E O C L U B   -   M a n a g e m e n t  a p p  ********** ");
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\n\t 1 - Sign in");
            Console.WriteLine("\n\t 2 - Register");
            Console.WriteLine("\n\t 3 - Quit");
            Console.Write("\n\t Select option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            if (option == 1)
            {
                SiginMenu();
            }
            else if (option == 2)
            {
                RegisterMenu();
            }
            else if (option == 3)
            {
                SignoutMenu();
            }
            else
            {
                ErrorMenu();
            }
        }

        // User sign in menu-----------------------------------------------------------------------------------------------------------------------------------
        public static void SiginMenu()
        {
            Console.Clear();
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\t********************  V I D E O C L U B   -   Sign in ******************** ");
            Console.WriteLine("\t************************************************************************** ");

            Console.WriteLine("\n\t ->  Sign in to the app <-");
            Console.Write("\n\t Enter your mail address: ");
            string email = Console.ReadLine().ToLower();

            Console.Write("\n\t Enter your password: ");
            string password = Console.ReadLine();

            Clients loginData = new Clients(email, password);

            if (loginData.Signin(email, password))
            {
                ClientMenu(loginData);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t We had a problem checking your details, are both your email and password correct?. \n\t Try to sign in again or create a new account.");
                Console.ResetColor();
                Console.WriteLine("\n\t Press any key to return to main menu...");
                Console.ReadKey();
                WelcomeMenu();
            }
        }

        // User register menu----------------------------------------------------------------------------------------------------------------------------------
        public static void RegisterMenu()
        {
            Console.Clear();
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\t****************  V I D E O C L U B   -   R e g i s t e r **************** ");
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\n\t ->  Sign up  <-");
            Console.Write("\n\t Enter your name: ");
            string name = Console.ReadLine();

            Console.Write("\n\t Enter your surname: ");
            string surname = Console.ReadLine();

            Console.Write("\n\t Enter your mail address (you will use it as username): ");
            string email = Console.ReadLine().ToLower();

            Console.Write("\n\t Enter a password: ");
            string password = Console.ReadLine();

            Console.Write("\n\t Enter your date of birth (YYYY/MM/DD format): ");
            string dob = Console.ReadLine();

            // Calling the method Register() in Clients class
            Clients register = new Clients(name, surname, dob, email, password);
            register.Register();

            if (register.Register())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\t You are now a registered user, welcome aboard!!");
                Console.ResetColor();
                Console.WriteLine("\n\t Press any key to get the main menu.");
                Console.ReadKey();
                WelcomeMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t We had a problem with your register, please try again");
                Console.ResetColor();
                Console.WriteLine("\n\t Press any key to return to main menu.");
                Console.ReadKey();
            }
        }

        // Client menu-----------------------------------------------------------------------------------------------------------------------------------------
        public static void ClientMenu(Clients loginData)
        {
            Console.Clear();
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\t******************  V I D E O C L U B   -   Client menu ****************** ");
            Console.WriteLine("\t************************************************************************** ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n\t -> Welcome again, {loginData.Name} <-");
            Console.ResetColor();
            Console.WriteLine("\n\t 1 - Have a look at our catalog");
            Console.WriteLine("\n\t 2 - Rent a film");
            Console.WriteLine("\n\t 3 - Check your current rentings");
            Console.WriteLine("\n\t 0 - Log out");

            Console.Write("\n\t Select option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    ShowAllFilms(loginData);
                    break;

                case 2:
                    RentFilm(loginData);
                    break;

                case 3:
                    break;

                case 0:
                    SignoutMenu();
                    break;

                default:
                    ErrorMenu();
                    ClientMenu(loginData);
                    break;
            }
        }

        // Sign out menu---------------------------------------------------------------------------------------------------------------------------------------
        public static void SignoutMenu()
        {
            Console.Clear();
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\t*******************  V I D E O C L U B   -   Sign out ******************** ");
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\n\n\t Thanks for using our app.");
            Console.WriteLine("\n\n\t Hope to see you soon!!");
            Console.WriteLine("\n\n\t**************************************************************************\n\n\n\n ");
        }

        // Retrieve and show films-----------------------------------------------------------------------------------------------------------------------------
        public static void ShowAllFilms(Clients loginData)
        {
            // DB query to retrieve data-----------------------------------------------------------------------------------------------------------------------
            string query = $"SELECT * FROM films WHERE agerate <= {loginData.GetAge()}";

            //try - catch block to check if database connection was ok-----------------------------------------------------------------------------------------
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<Films> films = new List<Films>();

                while (reader.Read())
                {
                    films.Add(new Films(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), Convert.ToInt32(reader[3].ToString()), Convert.ToChar(reader[4].ToString())));
                }
                connection.Close();

                Console.Clear();
                Console.WriteLine("\t************************************************************************** ");
                Console.WriteLine("\t******************  V I D E O C L U B   -   Film list ******************** ");
                Console.WriteLine("\t************************************************************************** ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n\t We consider your age ({loginData.GetAge()} years old) in order to show the films you will be able to rent \n");
                Console.ResetColor();

                foreach (Films film in films)
                {
                    Console.WriteLine("\t ===================================================================================================");
                    Console.WriteLine($"\t Catalog reference (needed to rent this film): {film.FilmId}");
                    Console.WriteLine($"\t Title: {film.Title}");
                    Console.WriteLine($"\t Plot: {film.Plot}");
                    if (film.Rented.ToString().Contains("Y")) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\n\t Film not available to rent"); Console.ResetColor(); } else { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\n\t Film available to rent"); Console.ResetColor(); };
                    Console.WriteLine("\t ===================================================================================================");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\t Press 1 to rent a film - Press 2 to return to previous menu - Press 0 to quit the app  ");
                Console.ResetColor();

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        RentFilm(loginData);
                        break;

                    case 2:
                        ClientMenu(loginData);
                        break;

                    case 0:
                        SignoutMenu();
                        break;

                    default:
                        ErrorClientMenu(loginData);
                        break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t We had some problems connecting the database.");
                Console.WriteLine("\n\t Try again...");
                Console.ResetColor();
            }
        }

        // Renting films menu----------------------------------------------------------------------------------------------------------------------------------
        public static void RentFilm(Clients loginData)
        {
            Console.Clear();
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\t*******************  V I D E O C L U B   -   Renting ********************* ");
            Console.WriteLine("\t************************************************************************** ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n\t Enter the film catalog reference number you want to rent: ");
            Console.ResetColor();

            int filmId = 0;
            int.TryParse(Console.ReadLine(), out filmId);

            // DB query to save data---------------------------------------------------------------------------------------------------------------------------
            string query = $"INSERT INTO rentals (filmid, rentalstartdate, returned) VALUES ({filmId},{DateTime.Today},'N')";

            // try - catch block to check if database connection was ok ---------------------------------------------------------------------------------------
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t We had some problems connecting to the database.");
                Console.WriteLine("\n\t Try again...");
                Console.WriteLine("\n\t Press enter to return to client menu");
                Console.ResetColor();
                Console.ReadKey();
                ClientMenu(loginData);
            }
        }

        // Error menu (client logged in)-------------------------------------------------------------------------------------------------------------------
        public static void ErrorClientMenu(Clients loginData)
        {
            Console.Clear();
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\t********************  V I D E O C L U B   -   Error ********************** ");
            Console.WriteLine("\t************************************************************************** ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t You have entered an incorrect option");
            Console.WriteLine("\n\t Press enter to return to client menu");
            Console.ResetColor();
            Console.ReadKey();
            ClientMenu(loginData);
        }

        // Error menu (client NOT logged in)-------------------------------------------------------------------------------------------------------------------
        public static void ErrorMenu()
        {
            Console.Clear();
            Console.WriteLine("\t************************************************************************** ");
            Console.WriteLine("\t********************  V I D E O C L U B   -   Error ********************** ");
            Console.WriteLine("\t************************************************************************** ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t You have entered an incorrect option");
            Console.WriteLine("\n\t Press enter to return to main menu");
            Console.ResetColor();
            Console.ReadKey();
            WelcomeMenu();
        }
    }
}