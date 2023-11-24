class FrontPage
{
    public static void MainMenu()
    {
        Customer currentCustomer = null!;

        while (true)
        {
            List<string> options = new List<string> { };
            if (currentCustomer is null)
            {
                options.Add("inloggen");
                options.Add("registreren");
            }
            else if (currentCustomer is Customer)
            {
                options.Add("zie persoonlijke informatie");
                options.Add("uitloggen");
            }

            options.AddRange(new List<string>
                    {
            "film kiezen",
            "bioscoop informatie"
                    });

            int selectedIndex = 0;

            ConsoleKeyInfo keyInfo;
            string line = new string('=', Console.WindowWidth);

            do
            {
                Console.Clear();
                System.Console.WriteLine(line);
                CreateTitleASCII();
                System.Console.WriteLine(line);
                CenterText("menu opties");

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
                System.Console.WriteLine(@"gebruik WASD keys om je optie te selecteren druk daarna op Enter op je keuze te bevestigen
Druk op ESC om te vertrekken.
");
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
                {
                    selectedIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
                {
                    selectedIndex++;
                }
            } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                System.Console.WriteLine(" SSee you!");
                break;
            }

            Console.WriteLine("je hebt dit geselecteerd: " + options[selectedIndex]);
            Console.ReadKey();
            Console.Clear();

            if (options[selectedIndex] == "inloggen")
            {
                currentCustomer = LogIn.LogInMenu();
            }
            else if (options[selectedIndex] == "registreren")
            {
                currentCustomer = registreren.RegistreerMenu();
            }
            else if (options[selectedIndex] == "bioscoop informatie")
            {
                CinemaInfo();
            }
            else if (options[selectedIndex] == "film kiezen")
            {
                ChooseMovie.Films_kiezen(currentCustomer);
            }
            else if (options[selectedIndex] == "uitloggen")
            {
                Console.WriteLine("Weet je zeker dat je wilt uitloggen? Zo ja typ in ja. zo nee typ iets anders in.");
                string response = Console.ReadLine()!;
                if (response is null) continue;
                else if (response.ToLower() == "ja" ^ response.ToLower() == "y")
                {
                    currentCustomer = null!;
                }
            }
            else if (options[selectedIndex] == "zie persoonlijke informatie")
            {
                ViewCustomerInfo.ViewInfoMenu(currentCustomer);
            }
        }
    }

    public static void CinemaInfo()
    {
        Console.Clear();
        System.Console.WriteLine("Bioscoop informatie\n Wijnhaven 107, 3011 WN in Rotterdam\n\n\ndruk op een willekeurige knop om terug naar de voorpagina te gaan ");
        Console.ReadKey();

    }

    public static void CreateTitleASCII()
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

}
