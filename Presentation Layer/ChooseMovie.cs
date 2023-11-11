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
                    // display test van valdier
                    Console.WriteLine("--> " + options[i].Title + " " + options[i].FirstDateAndAuditoriumKey);
                }
                else
                {
                    Console.WriteLine("    " + options[i].Title);
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