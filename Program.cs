// using data_acces.Tests;
class Program
{
    static void Main(string[] args)
    {
        foreach (string arg in args)
        {

            Console.WriteLine(arg);
            switch (arg)
            {
                case "SHOWDAY":
                    {
                        DateTime specificDateTime = new DateTime(2023, 11, 11, 12, 0, 0); // November 11, 2023, 12:00 PM
                        DayOfWeek dayOfWeek = specificDateTime.DayOfWeek;
                        string dayOfWeekString = dayOfWeek.ToString();
                        System.Console.WriteLine(dayOfWeekString);
                        return;
                    }
                case "SAVE":
                    {
                        Film myFilm = new Film(
                        title: "Inception",
                        runtime: 148,
                        price: 10.99,
                        filmrating: 4.5,
                        ReleaseYear: 2000,
                        genres: new List<string> { "Sci-Fi", "Action", "Thriller" },
                        director: "Christopher Nolan",
                        cinemaAudience: new List<string> { },
                        DateAndAuditorium: new Dictionary<string, string>());
                        System.Console.WriteLine("object created");
                        myFilm.AddDateTimeAndAuditorium("1");
                        FilmSave.AddToJson(myFilm);

                        return;

                    }


                // case "TESTSAVE":
                //     {
                //         FilmSaveTest saveTest = new();
                //         saveTest.read_films_test("Test film", 1, 1, 1, 2000);
                //         break;
                //     }

                case "TESTCUSTOMER":
                    {
                        AuditoriumMap150 map1 = new AuditoriumMap150();
                        string code = map1.GenerateConfirmationCode();
                        Customer customer = new Customer("Jahlani", code);
                        System.Console.WriteLine(customer.ConfirmationCode);
                        customer.SaveToJsonFile();
                        Console.ReadKey();
                        break;
                    }
                case "TESTCUSTOMERTOFILM":
                    {
                        List<Film> films = FilmSave.ReadFilms();
                        List<Customer> customers = Customer.LoadFromJsonFile();
                        FilmSave.AddCustomerToFilm(films[0], customers[2]);
                        System.Console.WriteLine("Done");
                        Console.ReadKey();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("this is not a valid one");
                        break;
                    }
            }
        }
        FrontPage.MainMenu();
    }
}