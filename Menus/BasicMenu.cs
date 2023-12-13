static class BasicMenu
{
    public static (string? optionChosen, ConsoleKey lastKey) MenuBasic(List<string> options, string MenuName)
    {
        if (options is null) return (null, ConsoleKey.Escape);
        if (options.Count == 0) return (null, ConsoleKey.Escape);
        if (MenuName is null) return (null, ConsoleKey.Escape);
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        string line = new string('=', Console.WindowWidth);


        do
        {


            ShowBasics();
            System.Console.WriteLine(MenuName);

            System.Console.WriteLine("menu opties");


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

        (string, ConsoleKey) ToReturn = (options[selectedIndex], keyInfo.Key);
        return ToReturn;
    }

    public static void ShowBasics()
    {
        string line = new string('=', Console.WindowWidth);

        Console.Clear();
        System.Console.WriteLine(line);
        CreateTitleASCII();
        System.Console.WriteLine(line);
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

    public static void CenterText(string text)
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