<<<<<<< Updated upstream
﻿using data_acces.Tests;
=======
﻿// using data_acces.Tests;
>>>>>>> Stashed changes
class Program
{
    static void Main(string[] args)
    {
<<<<<<< Updated upstream
        foreach (string arg in args)
        {
            Console.WriteLine(arg);
            switch(arg)
            {
                case "TESTSAVE":
                {
                    FilmSaveTest saveTest = new();
                    saveTest.read_films_test("Test film", 1, 1, 1);
                    break;
                }
                case "TESTMAP":
                {
                    AuditoriumMap500 map500 = new AuditoriumMap500();
                    map500.TakeSeats();
                    return;
                }
                default:
                {
                    Console.WriteLine("this is not a valid one");
                    break;
                }
            }
        }
        FrontPage.MainMenu();
=======

        List<Customer> customers = Customer.LoadFromJsonFile();
        Film myFilm = new Film("Sample Film", 120, 12.99, 4.5, new List<string> { "Action", "Adventure" }, "John Doe", new List<string>());
        foreach (Customer customer in customers)
        {
            FilmSave.AddCustomerToFilm(myFilm, customer);
        }







        // foreach (string arg in args)
        // {
        //     Console.WriteLine(arg);
        //     switch(arg)
        //     {
        //         case "TESTSAVE":
        //         {
        //             FilmSaveTest saveTest = new();
        //             saveTest.read_films_test("Test film", 1, 1, 1);
        //             break;
        //         }
        //         case "TESTMAP":
        //         {
        //             AuditoriumMap500 map500 = new AuditoriumMap500();
        //             map500.TakeSeats();
        //             return;
        //         }
        //         default:
        //         {
        //             Console.WriteLine("this is not a valid one");
        //             break;
        //         }
        //     }
        // }
        // FrontPage.MainMenu();
>>>>>>> Stashed changes
    }
}