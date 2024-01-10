public static class FrontPage
{
    public static Customer? CurrentCustomer { get; set; }

    public static void MainMenu(Customer? customer)
    {

        Customer? currentCustomer = customer;



        while (true)
        {
            if (FrontPage.CurrentCustomer is not null && currentCustomer is null) currentCustomer = FrontPage.CurrentCustomer;

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
                options.Add("Reservering annuleren");
            }

            options.AddRange(new List<string>
                    {
            "film kiezen",
            "bioscoop informatie"
                    });

            (string? optionChosen, ConsoleKey lastKey) MainMenu = BasicMenu.MenuBasic(options, "Hoofdmenu");


            if (MainMenu.lastKey == ConsoleKey.Escape)
            {
                Console.WriteLine(" Tot ziens!");
                return;
            }

            Console.Clear();

            string? optionChosen = MainMenu.optionChosen;


            if (optionChosen == "inloggen")
            {
                currentCustomer = LogIn.LogInMenu();
            }
            else if (optionChosen == "registreren")
            {
                currentCustomer = registreren.RegistreerMenu();
            }
            else if (optionChosen == "bioscoop informatie")
            {
                CinemaInfo.PrintCinemaInfo();
            }
            else if (optionChosen == "film kiezen")
            {
                ChooseMovie.Films_kiezen(currentCustomer!);
            }
            else if (optionChosen == "uitloggen")
            {
                Console.WriteLine("Weet je zeker dat je wilt uitloggen? Zo ja typ in ja. zo nee typ iets anders in.");
                string response = Console.ReadLine()!;
                if (response is null) continue;
                else if (response.ToLower() == "ja" ^ response.ToLower() == "y")
                {
                    FrontPage.CurrentCustomer = null;
                    currentCustomer = null!;
                }
            }
            else if (optionChosen == "zie persoonlijke informatie")
            {
                ViewCustomerInfo.ViewInfoMenu(currentCustomer);
            }
            else if (optionChosen == "Reservering annuleren")
            {
                MovieCancel.CancelRegistry(currentCustomer);
            }

        }
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


}