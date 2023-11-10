class ChooseMovie : FrontPage
{
    public static void Films_kiezen()
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

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine("--> " + options[i].Title);
                }
                else
                {
                    Console.WriteLine("    " + options[i].Title);
                }
            }
            System.Console.WriteLine(line);
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        MovieWriteAndLoad.printfilmInfo(options[selectedIndex]);
        System.Console.WriteLine("Druk op Enter om stoelen te reserveren voor deze film \nDruk een ander willekeurige toets om terug te gaan naar de vorige pagina");
        MovieConfirm();
    }

    public static void MovieConfirm()
    {
        ConsoleKeyInfo keyInfo;
        keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            AuditoriumMap500 map500 = new AuditoriumMap500();
            map500.TakeSeats();
        }
        else if (keyInfo.Key != ConsoleKey.Enter)
        {
            Films_kiezen();
        }
    }
}