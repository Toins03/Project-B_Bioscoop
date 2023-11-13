using Newtonsoft.Json;

public class ManageReservations
{
    public static void ReservationsOptions()
    {
        Console.Clear();
        List<string> options = new List<string>()
        {
            "Reservatie(s) toevoegen",
            "Reservatie(s) verwijderen"
        };
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

            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0 || keyInfo.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1 || keyInfo.Key == ConsoleKey.DownArrow && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }

        } while (keyInfo.Key != ConsoleKey.Enter & keyInfo.Key != ConsoleKey.Escape);

        if (options[selectedIndex] == "Reservatie(s) toevoegen")
        {
            Console.Clear();
            MovieTitleToManageReservations(true);

        }
        if (options[selectedIndex] == "Reservatie(s) verwijderen")
        {
            Console.Clear();
            MovieTitleToManageReservations(false);

        }

    }

    private static void MovieTitleToManageReservations(bool IsAdd)
    {
        System.Console.WriteLine("Voor welke film wil je een reservatie toevoegen?:\n");
        string movieTitle = Console.ReadLine()!;
        List<MovieScheduleInformation> allMovies = ReadDataFromJson()!;
        foreach (var movie in allMovies)
        {
            if (movie.Title == movieTitle)
            {
                if (IsAdd)
                    AddReservation(movieTitle);
                else
                    RemoveReservation(movieTitle);
            }
            else
            {
                System.Console.WriteLine("Sorry maar die film bestaat niet in de database. \nZorg ervoor dat je de titel goed spelt.");
                MovieTitleToManageReservations(IsAdd);
            }
        }
    }

    private static void AddReservation(string MovieTitle)
    {
        List<MovieScheduleInformation> AllMoviesFromJson = ReadDataFromJson()!;
        foreach (var movie in AllMoviesFromJson)
        {
            if (movie.Title == MovieTitle)
            {
                List<List<string>> movieAuditorium = movie.ScreeningTimeAndAuditorium["11-11-2023"];
                switch (movieAuditorium.Count)
                {
                    case 14:
                        AuditoriumMap150 map150 = new AuditoriumMap150();
                        map150.TakeSeats(MovieTitle, false);
                        break;
                    case 19:
                        AuditoriumMap300 map300 = new AuditoriumMap300();
                        map300.TakeSeats(MovieTitle, false);
                        break;
                    case 20:
                        AuditoriumMap500 map500 = new AuditoriumMap500();
                        map500.TakeSeats(MovieTitle, false);
                        break;
                    default:
                        break;
                }

            }
        }
    }

    private static void RemoveReservation(string MovieTitle)
    {
        System.Console.WriteLine("Welke reservatie wil je verwijderen?:\n");
        List<MovieScheduleInformation> AllMoviesFromJson = ReadDataFromJson()!;
        foreach (var movie in AllMoviesFromJson!)
        {
            if (movie.Title == MovieTitle)
            {
                if (RemoveTheReservation(movie.ReservationsList))
                {

                    switch (movie.ScreeningTimeAndAuditorium["11-11-2023"].Count)
                    {
                        case 14:
                            AuditoriumMap150 map150 = new AuditoriumMap150();
                            map150.TakeSeats(MovieTitle, true);
                            break;
                        case 19:
                            AuditoriumMap300 map300 = new AuditoriumMap300();
                            map300.TakeSeats(MovieTitle, true);
                            break;
                        case 20:
                            AuditoriumMap500 map500 = new AuditoriumMap500();
                            map500.TakeSeats(MovieTitle, true);
                            break;
                        default:
                            break;
                    }
                    System.Console.WriteLine("Reservatie is verwijderd");
                }
                else
                {
                    ReservationsOptions();
                }

            }

        }
    }

    private static bool RemoveTheReservation(List<string> reservations)
    {
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        do
        {

            Console.Clear();
            System.Console.WriteLine("Admin commands");

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
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            Console.WriteLine("Je gaat terug naar het vorige menu");
            return false;
        }
        return true;
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
