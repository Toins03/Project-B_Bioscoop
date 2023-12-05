class ChooseMovie : FrontPage
{
    static string movieTitle = "";
    public static void Films_kiezen(Customer currentCustomer)
    {
        MovieWriteAndLoad film_menu = new("Movies.json");
        List<Film> options = film_menu.ReadFilms();
        int selectedIndex = 0;

        ConsoleKeyInfo keyInfo;
        string line = new string('=', Console.WindowWidth);

        do
        {
            Console.Clear();
            System.Console.WriteLine(line);
            CreateTitleASCII();
            System.Console.WriteLine(line);
            CenterText("Film kiezen om te bekijken:\n");

            for (int i = 0; i < options.Count + 1; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("--> Sort Movies");
                }
                else if (i == selectedIndex)
                {
                    // display test van valdier
                    Console.WriteLine("--> " + options[i - 1].Title + " " + options[i - 1].ShowDate());
                }
                else
                {
                    Console.WriteLine("    " + options[i - 1].Title);
                }
            }
            System.Console.WriteLine(line);
            keyInfo = Console.ReadKey();

            // update aan de controls uparrow and W gaan niet meer boven de begin optie of onder de laatste optie dit graag behouden aub
            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0 || keyInfo.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1 || keyInfo.Key == ConsoleKey.DownArrow && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }
            else
            {

            }
        } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);

        if (keyInfo.Key == ConsoleKey.Escape)
        {
            System.Console.WriteLine("Je verlaat het film scherm!");
            return;
        }
// if they decide to sort movies
        if (selectedIndex == 0)
        {
            SortedMovies.ViewmoviesSorted(currentCustomer, options);
        }
        else
        {
            MovieWriteAndLoad.printfilmInfo(options[selectedIndex]);
            System.Console.WriteLine("Druk op Enter om stoelen te reserveren voor deze film \nDruk een ander willekeurige toets om terug te gaan naar de vorige pagina");
            movieTitle = options[selectedIndex].Title;
            MovieConfirm(currentCustomer);
        }
    }

    public static void MovieConfirm(Customer currentCustomer)
    {
        ConsoleKeyInfo keyInfo;
        keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            AuditoriumMap150 map500 = new AuditoriumMap150();
            map500.TakeSeats(movieTitle, currentCustomer, false);
        }
        else if (keyInfo.Key != ConsoleKey.Enter)
        {
            Films_kiezen(currentCustomer);
        }
    }


}
