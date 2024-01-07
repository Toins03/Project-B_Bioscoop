using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

public class ManageReservations
{
    private static List<MovieScheduleInformation> allMovies = ReadDataFromJson()!;

    public static List<string> options { get; } = new()
        {
            "Reservering(en) toevoegen",
            "Reservering(en) verwijderen"
        };
    public static Admin? LoggedinAdmin;

    private static void PrintLogo()
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        Console.WriteLine(line);
        FrontPage.CreateTitleASCII();
        Console.WriteLine(line);
    }
    public static int DisplayMenu()
    {
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        do
        {
            PrintLogo();
            Console.WriteLine("Admin reservering opties\n");
            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"{(i == selectedIndex ? "--> " : "    ")}{options[i]}");

            keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape) return 0;

            HandeUserInput(ref selectedIndex, keyInfo, options);

        } while (keyInfo.Key != ConsoleKey.Enter);

        return selectedIndex;
    }

    public static void HandeUserInput<T>(ref int selectedIndex, ConsoleKeyInfo keyInfo, List<T> optionsList)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.W or ConsoleKey.UpArrow:
                if (selectedIndex > 0)
                    selectedIndex--;
                break;
            case ConsoleKey.S or ConsoleKey.DownArrow:
                if (selectedIndex < optionsList.Count - 1)
                    selectedIndex++;
                break;
        }
    }

    public static void ManageReservationsOptions(Admin admin)
    {
        LoggedinAdmin = admin;
        int selectedIndex = DisplayMenu();
        switch (selectedIndex)
        {
            case 0:
                AdminMenu.Menu(admin);
                break;
            case int selIndex when options[selIndex] == "Reservering(en) toevoegen":
                Console.Clear();
                MovieTitleToManageReservations(true);
                break;
            case int selIndex when options[selIndex] == "Reservering(en) verwijderen":
                Console.Clear();
                MovieTitleToManageReservations(false);
                break;
            default:
                break;
        }
    }

    private static void MovieTitleToManageReservations(bool isToAdd)
    {
        if (isToAdd)
            AddReservation(ChooseMovieToManageReservations(isToAdd));
        else
            RemoveConfirmationcode(ChooseMovieToManageReservations(isToAdd));
    }

    public static MovieScheduleInformation ChooseMovieToManageReservations(bool isToAdd)
    {
        Console.Clear();
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        do
        {
            PrintLogo();
            Console.WriteLine($"Voor welke film wil je een reservering {(isToAdd ? "toevoegen" : "verwijderen")}: \nDruk op Enter om te kiezen:");
            for (int i = 0; i < allMovies.Count; i++)
                Console.WriteLine($"{(i == selectedIndex ? "--> " : "    ")}{allMovies[i].Title}");

            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Escape) ManageReservationsOptions(LoggedinAdmin);
            HandeUserInput(ref selectedIndex, keyInfo, allMovies);
        }
        while (keyInfo.Key != ConsoleKey.Enter & keyInfo.Key != ConsoleKey.Escape);

        return allMovies[selectedIndex];
    }

    private static void AddReservation(MovieScheduleInformation Movie)
    {
        DateTime showingchosen = ChooseBetweenShowings(Movie);
        if (showingchosen == DateTime.MinValue) return;
        
        
        
        List<List<string>> movieAuditorium = Movie.ScreeningTimeAndAuditorium[showingchosen];
        switch (movieAuditorium.Count)
        {
            case 14:
                AuditoriumMap150 map150 = new AuditoriumMap150();
                map150.TakeSeats(Movie.Title!, FrontPage.CurrentCustomer, false, showingchosen);
                break;
            case 21:
                AuditoriumMap300 map300 = new AuditoriumMap300();
                map300.TakeSeats(Movie.Title!, FrontPage.CurrentCustomer, false, showingchosen);
                break;
            case 22:
                AuditoriumMap500 map500 = new AuditoriumMap500();
                map500.TakeSeats(Movie.Title!, FrontPage.CurrentCustomer!, false, showingchosen);
                break;
            default:
                break;
        }
    }

    private static void RemoveReservation(MovieScheduleInformation Movie, List<string> reservations, int selectedIndex)
    {
        DateTime showingchosen = ChooseBetweenShowings(Movie);
        if (showingchosen == DateTime.MinValue) return;

        switch (Movie.ScreeningTimeAndAuditorium[showingchosen].Count)
        {
            case 14:
                AuditoriumMap150 map150 = new AuditoriumMap150();
                map150.TakeSeatsRemove(Movie.ScreeningTimeAndAuditorium[showingchosen], reservations, selectedIndex, true);
                break;
            case 21:
                AuditoriumMap300 map300 = new AuditoriumMap300();
                map300.TakeSeatsRemove(Movie.ScreeningTimeAndAuditorium[showingchosen], reservations, selectedIndex, true);
                break;
            case 22:
                AuditoriumMap500 map500 = new AuditoriumMap500();
                map500.TakeSeatsRemove(Movie.ScreeningTimeAndAuditorium[showingchosen], reservations, selectedIndex, true);
                break;
            default:
                break;
        }

        System.Console.WriteLine("Reservatie is verwijderd");
        Console.ReadKey();
    }

    public static bool RemoveReservation(RentedMovieInfo movieInfo)
    {
        List<MovieScheduleInformation>? movies = MovieScheduleInformation.ReadDataFromJson();
        if (movies is null) 
        {
            Console.WriteLine("Geen opgeslagen films gevonden.");
            Console.ReadKey();
            return false;
        }
        List<MovieScheduleInformation> movieWithTitle = movies.Where(movie => movie.Title == movieInfo.FilmTitle).ToList();
        if (movieWithTitle.Count == 0)
        {
            Console.WriteLine("geen film met die titel gevonden");
            Console.ReadKey();
            return false;
        }

        MovieScheduleInformation foundMovie = movieWithTitle[0];

        if (foundMovie.ScreeningTimeAndAuditorium.ContainsKey(movieInfo.TimeViewing))
        {
            List<List<string>> currentAuditorium = foundMovie.ScreeningTimeAndAuditorium[movieInfo.TimeViewing];
            int auditoriumlenght = currentAuditorium.Count;
            foreach (string seat in movieInfo.SeatsTaken)
            {
                Console.WriteLine(seat);
                (int row, int col) position = FindRowAndColFromSeat(seat, auditoriumlenght);
                
                List<List<string>>? changedauditorium = UpdateAtseatposition(seat, position.row, position.col, currentAuditorium);
                if (changedauditorium is null)
                {
                    Console.WriteLine($"stoel {seat} is niet geupdated.");
                    Console.ReadKey();
                }
                else currentAuditorium = changedauditorium;
            }

            MovieScheduleInformation.UpdateJsonFile(foundMovie, movieInfo.TimeViewing, currentAuditorium);
            return true;
        }
        else 
        {
            Console.WriteLine("De tijd van de film is niet gevonden");
            Console.ReadKey();
            return false;
        }
        
    }

    private static (int row, int col) FindRowAndColFromSeat(string seat, int auditoriumlenght)
    {
        string seatearlypart = seat.Split("]").First();
        string seatnumberlettersonly = seatearlypart.Split("[").Last();
        string lettersonly = "";
        int intonly = 0;
        foreach (char letter in seatnumberlettersonly)
        {
            if (letter == ' ') continue;
            else if (int.TryParse(letter.ToString(), out int number))
            {
                intonly *= 10;
                intonly += number;
            }
            else
            {
                lettersonly += letter;
            }
        }
        int row = auditoriumlenght - intonly;
        int column = 0;

        List<string> alphabet = new List<string>();
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            alphabet.Add(letter.ToString());
        }
        alphabet.AddRange(new List<string> { "AA", "BB", "CC", "DD" });

        for (int i = 0; i < alphabet.Count; i++)
        {
            if (alphabet[i] == lettersonly)
            {
                column = i + 1;
                break;
            }
        }

        return (row, column);
    }

    private static List<List<string>>? UpdateAtseatposition(string seatfullname, int row, int seatColumn, List<List<string>> auditorium)
    {
        int currentColumn = 0;
        for (int i = 0; i < auditorium[row].Count; i++)
        {
            if (auditorium[row][i].Trim() != "")
            {
                currentColumn += 1;
                if (currentColumn == seatColumn)
                {
                    Console.WriteLine("Before");
                    Console.WriteLine(auditorium[row][i]);
                    auditorium[row][i] = seatfullname;

                    Console.WriteLine($"stoel op rij {row} kolom {seatColumn} is verwijderd");
                    Console.ReadKey();
                    return auditorium;
                }
            }
        }
        return null;
    }



    public static void RemoveConfirmationcode(MovieScheduleInformation Movie)
    {
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        List<string> reservations = Movie.ReservationsList;
        do
        {
            PrintLogo();
            if (reservations.Count == 0)
            {
                Console.WriteLine("Er zijn geen reservaties voor deze film \nDruk op een toets om terug te gaan naar de opties voor reservaties beheren.");
                Console.ReadKey();
                ChooseMovieToManageReservations(false);
            }

            Console.WriteLine("Kies de reservatie die je wilt verwijderen:\n");
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"{(reservation == reservations[selectedIndex] ? "--> " : "    ")}{reservation}");
            }

            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Escape) ChooseMovieToManageReservations(false);
            HandeUserInput(ref selectedIndex, keyInfo, reservations);
        }
        while (keyInfo.Key != ConsoleKey.Enter);
        RemoveReservation(Movie, reservations, selectedIndex);
    }

    public static void RemoveConfirmationcode(List<string> reservations, int selectedIndex)
    {
        reservations.RemoveAt(selectedIndex);
        WriteDataFromJson(allMovies);
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

    public static DateTime ChooseBetweenShowings(MovieScheduleInformation movie)
    {
        if (movie.ScreeningTimeAndAuditorium.Count == 0) return DateTime.MinValue;
        
        string menuName = "Kies op welke tijd u deze film wilt zien.";
        List<string> options = movie.ScreeningTimeAndAuditorium.Keys
        .Select(TimeOption => $"{TimeOption.Day}/{TimeOption.Month}/{TimeOption.Year} {TimeOption.Hour}:{TimeOption.Minute}")
        .ToList();
        
        (string? optionChosen, ConsoleKey lastKey) reservationChosen = BasicMenu.MenuBasic(options, menuName);

        if (reservationChosen.lastKey == ConsoleKey.Escape)
        {
            Console.WriteLine(" LLeaving admin options!");
            return DateTime.MinValue;
        }
        else if (reservationChosen.optionChosen is null)
        {
            Console.WriteLine("something went wrong");
            return DateTime.MinValue;
        }

        foreach (DateTime viewoption in movie.ScreeningTimeAndAuditorium.Keys)
        {
            string toCompare = $"{viewoption.Day}/{viewoption.Month}/{viewoption.Year} {viewoption.Hour}:{viewoption.Minute}";
            if (toCompare == reservationChosen.optionChosen)
            {
                return viewoption;
            }
        }
        return DateTime.MinValue;

    }

}
