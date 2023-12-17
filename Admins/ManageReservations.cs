using Newtonsoft.Json;

public class ManageReservations
{
    private static List<MovieScheduleInformation> allMovies = ReadDataFromJson()!;

    public static int DisplayMenu(List<string> options)
    {
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        do
        {
            Console.Clear();
            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine("--> " + options[i]);
                }
                else
                {
                    Console.WriteLine("    " + options[i]);
                }
            }

            keyInfo = Console.ReadKey();

            if ((keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow) && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if ((keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow) && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }

        } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);

        return selectedIndex;
    }


    public static void ManageReservationsOptions()
    {
        List<string> options = new()
        {
            "Reservatie(s) toevoegen",
            "Reservatie(s) verwijderen"
        };

        int selectedIndex = DisplayMenu(options);

        if (options[selectedIndex] == "Reservatie(s) toevoegen")
        {
            Console.Clear();
            MovieTitleToManageReservations(true);
        }
        else if (options[selectedIndex] == "Reservatie(s) verwijderen")
        {
            Console.Clear();
            MovieTitleToManageReservations(false);
        }
    }

    private static MovieScheduleInformation ChooseMovieToManageReservations()
    {
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        do
        {
            Console.Clear();

            for (int i = 0; i < allMovies.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine("--> " + allMovies[i].Title);
                }
                else
                {
                    Console.WriteLine("    " + allMovies[i].Title);
                }
            }
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0 || keyInfo.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < allMovies.Count - 1 || keyInfo.Key == ConsoleKey.DownArrow && selectedIndex < allMovies.Count - 1)
            {
                selectedIndex++;
            }

        } while (keyInfo.Key != ConsoleKey.Enter & keyInfo.Key != ConsoleKey.Escape);

        return allMovies[selectedIndex];
    }

    private static void MovieTitleToManageReservations(bool isToAdd)
    {
        if (isToAdd)
        {
            System.Console.WriteLine("Voor welke film wil je een reservatie toevoegen? \nDruk op Enter om te kiezen");
            AddReservation(ChooseMovieToManageReservations());

        }
        else
        {
            System.Console.WriteLine("Voor welke film wil je een reservatie verwijderen?: \nDruk op Enter om te kiezen");
            RemoveReservation(ChooseMovieToManageReservations());
        }
    }

    private static void AddReservation(MovieScheduleInformation Movie)
    {
        List<List<string>> movieAuditorium = Movie.ScreeningTimeAndAuditorium["11-11-2023"];
        switch (movieAuditorium.Count)
        {
            case 14:
                AuditoriumMap150 map150 = new AuditoriumMap150();
                map150.TakeSeats(Movie.Title!, FrontPage.CurrentCustomer, false);
                break;
            case 21:
                AuditoriumMap300 map300 = new AuditoriumMap300();
                map300.TakeSeats(Movie.Title!, FrontPage.CurrentCustomer, false);
                break;
            case 22:
                AuditoriumMap500 map500 = new AuditoriumMap500();
                map500.TakeSeats(Movie.Title!, FrontPage.CurrentCustomer!, false);
                break;
            default:
                break;
        }
    }

    private static void RemoveReservation(MovieScheduleInformation Movie)
    {
        System.Console.WriteLine("Kies de reservatie die je wilt verwijderen:\n");
        RemoveConfirmationcode(Movie.ReservationsList);
        WriteDataFromJson(allMovies);

        switch (Movie.ScreeningTimeAndAuditorium["11-11-2023"].Count)
        {
            case 14:
                AuditoriumMap150 map150 = new AuditoriumMap150();
                map150.TakeSeats(Movie.ScreeningTimeAndAuditorium["11-11-2023"], true);
                break;
            case 21:
                AuditoriumMap300 map300 = new AuditoriumMap300();
                map300.TakeSeats(Movie.ScreeningTimeAndAuditorium["11-11-2023"], true);
                break;
            case 22:
                AuditoriumMap500 map500 = new AuditoriumMap500();
                map500.TakeSeats(Movie.ScreeningTimeAndAuditorium["11-11-2023"], true);
                break;
            default:
                break;
        }

        System.Console.WriteLine("Reservatie is verwijderd");
    }


    private static void RemoveConfirmationcode(List<string> reservations)
    {
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        if (reservations.Count == 0)
        {
            System.Console.WriteLine("Er zijn geen reservaties voor deze film \nDruk op een toets om terug te gaan naar de opties voor reservaties beheren.");
            Console.ReadKey();
            ManageReservationsOptions();
        }
        do
        {
            Console.Clear();
            for (int i = 0; i < reservations.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine("--> " + reservations[i]);
                }
                else
                {
                    Console.WriteLine("    " + reservations[i]);
                }
            }
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < reservations.Count - 1)
            {
                selectedIndex++;
            }

        } while (keyInfo.Key != ConsoleKey.Enter & keyInfo.Key != ConsoleKey.Escape);
        reservations.RemoveAt(selectedIndex);
    }

    private static void WriteDataFromJson(List<MovieScheduleInformation> AddToJson)
    {
        if (File.Exists("MovieScheduleInformation.json"))
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("MovieScheduleInformation.json"))
                {
                    string List2Json = JsonConvert.SerializeObject(AddToJson, Formatting.Indented);
                    writer.Write(List2Json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON data: {ex.Message}");
            }
        }
    }

    private static List<MovieScheduleInformation>? ReadDataFromJson()
    {
        if (File.Exists("MovieScheduleInformation.json"))
        {
            try
            {
                using (StreamReader reader = new StreamReader("MovieScheduleInformation.json"))
                {
                    string json = reader.ReadToEnd();
                    List<MovieScheduleInformation> ExistingData = JsonConvert.DeserializeObject<List<MovieScheduleInformation>>(json)!;
                    if (ExistingData == null)
                    {
                        ExistingData = new List<MovieScheduleInformation>();
                    }
                    return ExistingData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON data: {ex.Message}");
            }
        }
        return null;
    }
}