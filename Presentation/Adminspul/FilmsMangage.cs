using System.Diagnostics.Tracing;

static class FilmsManage
{
    public static void FilmmanageMenu()
    {
        Console.Clear();
        List<string> options = new List<string>()
        {
            "Add Films",
            "Remove Films",
        };


        List<dynamic> inputs = BasicMenu.MenuBasic(options, "Manage films");

        bool isKeyEsc = inputs[0];

        if (isKeyEsc)
        {
            Console.WriteLine("Leaving Film options!");
            return;
        }

        string request = inputs[1];
        if (request is null) return;
        else if (request == "Add Films")
        {
            AddFilm();
        }

        else if (request == "Remove Films")
        {
            Console.WriteLine("Removing films has not been implemented yet!");
            throw new NotImplementedException();
        }

    }

    private static void AddFilm()
    {
        System.Console.WriteLine("Please input the name of the Film. To go back to the Film manager keep this line empty.");
        string ToAddName = Console.ReadLine()!;
        if (ToAddName is null) return;
        else if (ToAddName == "") return;
        
        int ToAddRunTime;
        while (true)
        {
            System.Console.WriteLine("Please input the runtime of the Film in seconds. To go back to the Film manager keep this line empty.");
            string ToAddRunTimestring = Console.ReadLine()!;
            if (ToAddRunTimestring is null) return;
            else if (ToAddRunTimestring == "") return;
            if (int.TryParse(ToAddRunTimestring, out _))
            {
                ToAddRunTime = Convert.ToInt32(ToAddRunTimestring);
                break;
            }
        }
        
        double ToAddPrice;
        while (true)
        {
            System.Console.WriteLine("Please input the price of the Film in euros. To go back to the Film manager keep this line empty.");
            string ToAddPricestring = Console.ReadLine()!;
            if (ToAddName is null) return;
            else if (ToAddName == "") return;
            if (double.TryParse(ToAddPricestring, out _))
            {
                ToAddPrice = Convert.ToDouble(ToAddPricestring);
                break;
            }
        }

        double toAddFilmRating = 0;

        List<string> toAddGenres = new List<string> ();
        while (true)
        {
            System.Console.WriteLine("Do you wish to add any genres? (Y/n) To go back to the Film manager keep this line empty.");
            string genreAdd = Console.ReadLine()!;
            if (genreAdd is null) return;
            if (genreAdd.ToLower() == "y" ^ genreAdd.ToLower() == "yes")
            {
                while (true)
                {
                    System.Console.WriteLine("Please input the genre to add. To continue leave this line empty");
                    string genreToAdd = Console.ReadLine()!;
                    if (genreToAdd is null) break;
                    else if (genreToAdd == "") break;
                    else toAddGenres.Add(genreToAdd);
                }
            }
            else if (genreAdd.ToLower() == "n" ^ genreAdd.ToLower() == "no")
            {
                break;
            }
        }

        System.Console.WriteLine("Please input the name of the director of the Film. To go back to the Film manager keep this line empty.");
        string ToAddDirector = Console.ReadLine()!;
        if (ToAddDirector is null) return;
        else if (ToAddDirector == "") return;
        
        Film filmToAdd = new Film(ToAddName, ToAddRunTime, ToAddPrice, toAddFilmRating, toAddGenres, ToAddDirector);

        FilmSave.AddToJson(filmToAdd);
        Console.WriteLine("succes");
    }

}