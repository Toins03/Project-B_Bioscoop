class ChooseMovie : FrontPage
{
    public static void Films_kiezen(Customer currentCustomer)
    {
        MovieWriteAndLoad film_menu = new("Movies.json");

        List<string> options = new()
        {
            "Film zoeken op titel", "Sorteer op titel\n"
        };
        List<Film> Movies = film_menu.ReadFilms();

        foreach (var movie in film_menu.ReadFilms())
        {
            options.Add(movie.Title);
        }

        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;

        do
        {
            Display(options, selectedIndex);
            keyInfo = Console.ReadKey();

            // update aan de controls uparrow and W gaan niet meer boven de begin optie of onder de laatste optie dit graag behouden aub
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
        while (keyInfo.Key != ConsoleKey.Enter);

        if (selectedIndex >= 2 && selectedIndex < options.Count)
        {
            MovieWriteAndLoad.printfilmInfo(Movies[selectedIndex - 2]);
            System.Console.WriteLine("Druk op Enter om stoelen te reserveren voor deze film \nDruk een ander willekeurige toets om terug te gaan naar de vorige pagina");
            MovieConfirm(currentCustomer, options[selectedIndex]);

        }
        else if (selectedIndex == 0)
        {
            // Placeholder for search method
            System.Console.WriteLine("Search method");
            System.Console.ReadLine();
        }
        else if (selectedIndex == 1)
        {
            System.Console.WriteLine("Sort method");
            System.Console.ReadLine();
            // Placeholder for sort method
        }

    }

    private static void Display(List<string> options, int selectedIndex)
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

    public static void MovieConfirm(Customer currentCustomer, string MovieTitle)
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
