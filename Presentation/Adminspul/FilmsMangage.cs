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
            "View Films",
        };


        List<string> inputs = BasicMenu.MenuBasic(options, "Manage films");

        string Keyleaving = inputs[0];

        if (Keyleaving == "escape")
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
        else if (request == "View Films")
        {
            ViewFilms();
        }
        
        FilmmanageMenu();
    }

    private static void AddFilm()
    {
        System.Console.WriteLine("Please input the name of the Film. To go back to the Film manager keep this line empty.");
        string ToAddName = Console.ReadLine()!;
        if (ToAddName is null) return;
        else if (ToAddName == "") return;
        Console.Clear();
        
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
        Console.Clear();
        
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
            else
            {
                Console.WriteLine("This is not an integer! Press any button to retry.");
                Console.ReadKey();
                Console.Clear();
            }
        }
        Console.Clear();

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
                    Console.Clear();
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
        Console.WriteLine("succes! Press any key to continue.");
        Console.ReadKey();
    }

    public static void ViewFilms()
    {
        List<Film> films = FilmSave.ReadFilms();
        List<string> filmNames = new() {};
        foreach (Film film in films)
        {
            filmNames.Add(film.Title);
        }

        List<string> inputs = BasicMenu.MenuBasic(filmNames, "view all films");

        string Keyleaving = inputs[0];

        if (Keyleaving == "escape")
        {
            Console.WriteLine("Leaving Film options!");
            return;
        }

        string film_chosen = inputs[1];

        Film ToShow = null!;

        foreach (Film film in films)
        {
            if (film.Title == film_chosen) 
            {
                ToShow = film;
                break;
            }
        }

        Console.WriteLine(ToShow.ToString());
        Console.WriteLine("press any button to continue");
        Console.ReadKey();

        ViewFilms();
    }

}