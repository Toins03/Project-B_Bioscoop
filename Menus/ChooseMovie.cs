class ChooseMovie : FrontPage
{
    public static void Films_kiezen(Customer currentCustomer)
    {
        MovieWriteAndLoad film_menu = new("Movies.json");

        List<string> options = new() { "Film zoeken op titel", "Sorteer op titel\n" };
        List<Film> Movies = film_menu.ReadFilms();

        foreach (var movie in film_menu.ReadFilms())
        {
            options.Add(movie.Title);
        }

        ConsoleKeyInfo keyInfo;
        int selectedIndex = 0;

        do
        {
            Display(options, selectedIndex);
            keyInfo = Console.ReadKey();
            Console.WriteLine(Movies.Count);
            HandleUserInput(keyInfo, options, ref selectedIndex);

        } while (keyInfo.Key != ConsoleKey.Enter);

        HandleSelecedOption(currentCustomer, Movies, options, selectedIndex);
    }

    public static void Display(List<string> options, int selectedIndex)
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        Console.WriteLine(line);
        CreateTitleASCII();
        Console.WriteLine(line);
        CenterText("Film kiezen om te bekijken:\n");

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
        }
    }

    public static void HandleSelecedOption(Customer currentCustomer, List<Film> movies, List<string> options, int selectedIndex)
    {
        if (selectedIndex >= 2 && selectedIndex < options.Count)
        {
            MovieWriteAndLoad.printfilmInfo(movies[selectedIndex - 2]);
            System.Console.WriteLine("Druk op Enter om stoelen te reserveren voor deze film \nDruk een ander willekeurige toets om terug te gaan naar de vorige pagina");
            ConfirmMovieSelection(currentCustomer, options[selectedIndex]);
        }
        else if (selectedIndex == 0)
        {
            InputTitleToSearch(currentCustomer, movies);
        }
        else if (selectedIndex == 1)
        {
            // Placeholder for sort method
            Console.WriteLine("Sort method");
            Console.ReadLine();
        }
    }

    public static void InputTitleToSearch(Customer currentCustomer, List<Film> movies)
    {
        Console.WriteLine("Druk Enter en laat het veld leeg om terug te gaan \nType de titel van de film om op te zoeken:");
        string title = Console.ReadLine()!;

        if (!string.IsNullOrEmpty(title) && IsMovieFound(title, movies, currentCustomer))
        {
            ConfirmMovieSelection(currentCustomer, title);
        }
        else
        {
            Films_kiezen(currentCustomer);
        }
    }

    public static bool IsMovieFound(string title, List<Film> movies, Customer currentCustomer)
    {
        if (string.IsNullOrEmpty(title)) return false;

        string foundMovie = SearchTitleThroughMovies(title, movies);

        if (foundMovie != null && foundMovie.ToLower() == title.ToLower())
        {
            return true;
        }
        else
        {
            if (ChooseToKeepSearching()) InputTitleToSearch(currentCustomer, movies);
            else Films_kiezen(currentCustomer);
        }
        return false;
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
        foreach (var movie in Movies)
        {
            if (title.ToLower() == movie.Title.ToLower())
            {
                MovieWriteAndLoad.printfilmInfo(movie);
                Console.WriteLine("Druk op Enter om stoelen te reserveren voor deze film \nDruk een ander willekeurige toets om terug te gaan naar de vorige pagina");
                return movie.Title;
            }
        }
        return null!;
    }
    public static void ConfirmMovieSelection(Customer currentCustomer, string MovieTitle)
    {
        ConsoleKeyInfo keyInfo;
        keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            AuditoriumMap150 map500 = new AuditoriumMap150();
            map500.TakeSeats(MovieTitle, currentCustomer, false);
        }
        else if (keyInfo.Key != ConsoleKey.Enter)
        {
            Films_kiezen(currentCustomer);
        }
    }
}