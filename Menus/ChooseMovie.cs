public class ChooseMovie
{
    public static void Films_kiezen(Customer? currentCustomer)
    {


        List<string> options = new() { "Sorteer en filter opties\n" };
        List<MovieScheduleInformation> Movies = MovieScheduleInformation.ReadDataFromJson()!;

        if (Movies is null)
        {
            Console.WriteLine("Er zijn geen films gepland. Druk op een willekeurige knop om terug te gaan.");
            Console.ReadKey();
            return;
        }
        // check hier maken voor films die later dan vandaag te zien is op de bioscoop zodat je niet naar oude films gaat
        // die niet meer te zien is

        List<MovieScheduleInformation> MoviesAfterFilter = FilterMovies.FutureMoviesScheduled(Movies, DateTime.Now);
        if (MoviesAfterFilter is null)
        {
            Console.WriteLine("Er zijn geen films gepland. Druk op een willekeurige knop om terug te gaan.");
            Console.ReadKey();
            return;
        }
        
        foreach (MovieScheduleInformation scheduleInformation in MoviesAfterFilter)
        {
            options.Add(scheduleInformation.Title);
        }

        (string? optionChosen, ConsoleKey lastKey) moviechosen = BasicMenu.MenuBasic(options, "Kies een film die u wilt zien");

        if (moviechosen.lastKey == ConsoleKey.Escape)
        {
            return;
        }
        else if (moviechosen.optionChosen is null)
        {
            return;
        }
        else if (moviechosen.optionChosen =="Sorteer en filter opties\n")
        {
            List<Film> tosortby = FilmSave.FindFilmsWithSchedule(MoviesAfterFilter);
            
            SortedMovies.ViewSortOptions(currentCustomer, tosortby);
        }
        else
        {
            ConfirmMovieSelection(currentCustomer, moviechosen.optionChosen);

        }

    }

    public static void Display(List<string> options, int selectedIndex)
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        Console.WriteLine(line);
        BasicMenu.CreateTitleASCII();
        Console.WriteLine(line);
        Console.WriteLine("Film kiezen om te bekijken:\n");

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
        Console.WriteLine(line);
    }


    public static void HandleUserInput(ConsoleKeyInfo keyInfo, List<string> options, ref int selectedIndex)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.W or ConsoleKey.UpArrow:
                if (selectedIndex > 0) selectedIndex--;
                break;
            case ConsoleKey.S or ConsoleKey.DownArrow:
                if (selectedIndex < options.Count - 1) selectedIndex++;
                break;
            case ConsoleKey.Escape:
                return;
        }
    }

    public static void HandleSelecedOption(Customer? currentCustomer, List<Film> movies, List<string> options, int selectedIndex)
    {
        if (selectedIndex >= 1 && selectedIndex < options.Count)
        {
            MovieWriteAndLoad.printfilmInfo(movies[selectedIndex - 1]);

            ConfirmMovieSelection(currentCustomer, options[selectedIndex]);
        }
        else if (selectedIndex == 0)
        {
            SortedMovies.ViewSortOptions(currentCustomer, movies);
        }
    }

    public static void InputTitleToSearch(Customer currentCustomer, List<Film> movies)
    {
        Console.WriteLine("Druk Enter en laat het veld leeg om terug te gaan \nType de titel van de film om op te zoeken:");
        string title = Console.ReadLine()!;
        if (!IsMovieFound(title, movies, currentCustomer)) Films_kiezen(currentCustomer);

    }

    public static bool IsMovieFound(string title, List<Film> movies, Customer currentCustomer)
    {
        if (string.IsNullOrEmpty(title)) return false;

        string foundMovie = SearchTitleThroughMovies(title, movies);

        if (foundMovie != null && foundMovie.ToLower() == title.ToLower())
        {
            ConfirmMovieSelection(currentCustomer, foundMovie);
            return true;
        }
        else
        {
            if (ChooseToKeepSearching()) InputTitleToSearch(currentCustomer, movies);
            else Films_kiezen(currentCustomer);
            return false;
        }
    }

    public static bool ChooseToKeepSearching()
    {
        string choice;
        do
        {
            Console.WriteLine("Titel niet gevonden");
            Console.WriteLine("Wil je verder zoeken? \n\nType 'j' voor ja en 'n' voor nee");
            choice = Console.ReadLine()!;
            if (choice.ToLower() == "j")
            {
                return true;
            }
            else if (choice.ToLower() == "n")
            {
                return false;
            }
        }
        while (choice.ToLower() != "j" && choice.ToLower() != "n");
        return false;
    }

    public static string SearchTitleThroughMovies(string title, List<Film> Movies)
    {
        foreach (Film movie in Movies)
        {
            if (title.ToLower() == movie.Title.ToLower())
            {
                return movie.Title;
            }
        }
        return null!;
    }

    public static void ConfirmMovieSelection(Customer? currentCustomer, string MovieTitle)
    {
        Console.WriteLine("Druk op enter om een tijdoptie voor deze film te kiezen. Om terug te gaan druk op een willekeurige ander knop");
        ConsoleKeyInfo keyInfo;
        keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            MovieScheduleInformation movie = MovieScheduleInformation.findMovieScheduleInfoByTitle(MovieTitle)!;
            if (movie is null) return;
            DateTime showChosen = ChooseBetweenShowings(movie);

            if (showChosen == DateTime.MinValue)
            {
                return;
            }

            Console.Clear();
            // elke film word tot nu toe bij AuditoriumMap150 gezet en dat klopt niet hier is de fix ervoor
            List<Film> AllFilms = Film.LoadFilmFromJsonFile();

            List<Film> FilmWithSameTitle =
            AllFilms.
            Where(Movie => Movie.Title == MovieTitle)
            .ToList();
            // alle auditorums zijn 150,300 en 500
            KeyValuePair<DateTime, string> dateAuditoriumPair = FilmWithSameTitle[0].DateAndAuditorium.First();
            string auditorium = dateAuditoriumPair.Value;
            if (auditorium == "1")
            {
                AuditoriumMap150 map150 = new AuditoriumMap150();
                map150.TakeSeats(MovieTitle, currentCustomer, false, showChosen);
            }
            else if (auditorium == "2")
            {
                AuditoriumMap300 map300 = new AuditoriumMap300();
                map300.TakeSeats(MovieTitle, currentCustomer, false, showChosen);
            }
            else
            {
                AuditoriumMap500 map500 = new AuditoriumMap500();
                map500.TakeSeats(MovieTitle, currentCustomer, false, showChosen);
            }

        }
        else if (keyInfo.Key != ConsoleKey.Enter)
        {
            return;
            // Films_kiezen(currentCustomer);
            // infinite ?
            // ja was infinite 
        }
    }

    public static DateTime ChooseBetweenShowings(MovieScheduleInformation movie)
    {
        if (movie.ScreeningTimeAndAuditorium.Count == 0) return DateTime.MinValue;

        string menuName = "Kies op welke tijd u deze film wilt zien.";
        List<string> options = movie.ScreeningTimeAndAuditorium.Keys
        .Where(Timeoption => Timeoption > DateTime.Now)
        .Select(TimeOption => TimeOption.ToString("dd/MM/yyyy HH:mm"))
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
            string toCompare = viewoption.ToString("dd/MM/yyyy HH:mm");
            if (toCompare == reservationChosen.optionChosen)
            {
                return viewoption;
            }
        }
        return DateTime.MinValue;

    }

}