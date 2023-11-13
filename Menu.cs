class FrontPage
{
    public static void MainMenu()
    {

        List<string> options = new List<string>
                    {
            "Inloggen",
            "Film kiezen",
            "Bioscoop informatie"
                    };

        int selectedIndex = 0;

        ConsoleKeyInfo keyInfo;
        string line = new string('=', Console.WindowWidth);

        do
        {
            Console.Clear();
            System.Console.WriteLine(line);
            CreateTitleASCII();
            System.Console.WriteLine(line);
            CenterText("Menu opties");

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
            System.Console.WriteLine(line);
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0 || keyInfo.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1 || keyInfo.Key == ConsoleKey.DownArrow && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }
        } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);

        if (keyInfo.Key == ConsoleKey.Escape)
        {
            Console.WriteLine("TTot ziens!");
            return;
        }

        if (options[selectedIndex] == "Inloggen")
        {
            LogIn();
        }
        else if (options[selectedIndex] == "Bioscoop informatie")
        {
            CinemaInfo();
        }
        else if (options[selectedIndex] == "Film kiezen")
        {
            ChooseMovie.Films_kiezen();
        }
    }

    public static void CinemaInfo()
    {
        Console.Clear();
        System.Console.WriteLine("Bioscoop informatie \nLocatie: Wijnhaven 107, 3011 WN in Rotterdam\n\n\ndruk op een willekeurige knop om terug naar de voorpagina te gaan ");
        Console.ReadKey();
        MainMenu();

    }

    protected static void CreateTitleASCII()
    {
        string[] asciiArt =
        {
      @"  __  .__                                        .___",
      @"_/  |_|  |__   ____     _____ _____    ____    __| _/____   _____",
      @"\   __\  |  \_/ __ \   /     \\__  \  /    \  / __ |/ __ \ /     \",
      @" |  | |   Y  \  ___/  |  Y Y  \/ __ \|   |  \/ /_/ \  ___/|  Y Y  \",
      @" |__| |___|  /\___  > |__|_|  (____  /___|  /\____ |\___  >__|_|  /",
      @"           \/     \/        \/     \/     \/      \/    \/      \/"
        };

        foreach (string line in asciiArt)
        {
            Console.WriteLine(line);
        }
    }

    protected static void CenterText(string text)
    {
        int screenWidth = Console.WindowWidth;
        int textLength = text.Length;

        // Calculate the number of spaces to insert before the text to center it
        int spacesToInsert = (screenWidth - textLength) / 2;

        // Create a string with the calculated spaces
        string centeredText = new string(' ', spacesToInsert) + text;

        // Print the centered text
        Console.WriteLine(centeredText);
    }

    public static void LogIn()
    {
        Console.Clear();
        System.Console.Write("Toets je gebruikersnaam in:  ");
        string username = Console.ReadLine()!;
        System.Console.Write("Toets je wachtwoord in:  ");
        string Password = Console.ReadLine()!;
        MainMenu();
    }
}