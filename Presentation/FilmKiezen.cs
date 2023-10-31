<<<<<<< Updated upstream
class Films_UI: FrontPage
{
        public static void Films_kiezen()
    {
        FilmSave film_menu = new("Movies.json");
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
            CenterText("film opties");

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
            System.Console.WriteLine("gebruik WASD keys om je optie te selecteren druk daarna op Enter op je keuze te bevestigen\n\n");
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        FilmSave.printfilmInfo(options[selectedIndex]);
        Console.ReadKey();
    }
}
=======
// class Films_UI: FrontPage
// {
//         public static void Films_kiezen()
//     {
//         FilmSave film_menu = new("Movies.json");
//         List<Film> options = film_menu.ReadFilms();
//         int selectedIndex = 0;

//         ConsoleKeyInfo keyInfo;
//         string line = new string('=', Console.WindowWidth);

//         do
//         {
//             Console.Clear();
//             System.Console.WriteLine(line);
//             CreateTitleASCII();
//             System.Console.WriteLine(line);
//             CenterText("film opties");

//             for (int i = 0; i < options.Count; i++)
//             {
//                 if (i == selectedIndex)
//                 {
//                     Console.WriteLine("--> " + options[i].Title);
//                 }
//                 else
//                 {
//                     Console.WriteLine("    " + options[i].Title);
//                 }
//             }
//             System.Console.WriteLine(line);
//             System.Console.WriteLine("gebruik WASD keys om je optie te selecteren druk daarna op Enter op je keuze te bevestigen\n\n");
//             keyInfo = Console.ReadKey();

//             if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
//             {
//                 selectedIndex--;
//             }
//             else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
//             {
//                 selectedIndex++;
//             }
//         } while (keyInfo.Key != ConsoleKey.Enter);

//         FilmSave.printfilmInfo(options[selectedIndex]);
//         Console.ReadKey();
//     }
// }
>>>>>>> Stashed changes
