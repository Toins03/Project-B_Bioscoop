using System.Diagnostics.Tracing;

static class FilmsManage
{
    // public static void FilmmanageMenu()
    // {
    //     Console.Clear();
    //     List<string> options = new List<string>()
    //     {
    //         "Add Films",
    //         "Remove Films",
    //         "View Films",
    //     };


    //     (string? optionChosen, ConsoleKey keyLeaving) inputs = BasicMenu.MenuBasic(options, "Manage films");

    //     ConsoleKey Keyleaving = inputs.keyLeaving;

    //     if (Keyleaving == ConsoleKey.Escape)
    //     {
    //         Console.WriteLine("Leaving Film options!");
    //         return;
    //     }
    //     else if (inputs.optionChosen is null)
    //     {
    //         Console.WriteLine("Leaving Film options!");
    //         return;
    //     }

    //     string request = inputs.optionChosen;
    //     if (request is null) return;
    //     else if (request == "Add Films")//
    //     {
    //         AddFilm();
    //     }

    //     else if (request == "Remove Films")//
    //     {
    //         Console.WriteLine("Removing films has not been implemented yet!");
    //         throw new NotImplementedException();
    //     }
    //     else if (request == "View Films")//
    //     {
    //         ViewFilms();
    //     }

    //     FilmmanageMenu();
    // }

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
            System.Console.WriteLine("Please input the runtime of the Film in minutes. To go back to the Film manager keep this line empty.");
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

        List<string> toAddGenres = new List<string>();
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

        Film filmToAdd = new Film(ToAddName, ToAddRunTime, ToAddPrice, toAddFilmRating, 0, toAddGenres, ToAddDirector);

        FilmSave.AddToJson(filmToAdd);
        Console.WriteLine("succes! Press any key to continue.");
        Console.ReadKey();
    }

    public static void ViewFilms()
    {
        List<Film> films = FilmSave.ReadFilms();
        List<string> filmNames = new() { };
        foreach (Film film in films)
        {
            filmNames.Add(film.Title);
        }

        (string? optionChosen, ConsoleKey keyLeaving) inputs = BasicMenu.MenuBasic(filmNames, "view all films");

        ConsoleKey Keyleaving = inputs.keyLeaving;

        if (Keyleaving == ConsoleKey.Escape)
        {
            Console.WriteLine("Leaving Film options!");
            return;
        }
        if (inputs.optionChosen is null)
        {
            Console.WriteLine("Leaving Film options!");
            return;
        }

        string film_chosen = inputs.optionChosen!;

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
    public static void FilmManagement()
    {
        Console.Clear();
        List<string> options = new List<string>()
        {
            "Alle films zien",
            "Film toevoegen",
            "Bestaande film inplannen",
            "Film verwijderen",
        };

        (string? optionChosen, ConsoleKey lastKey) menuResult = BasicMenu.MenuBasic(options, "Manage admins");

        string option_chosen = menuResult.optionChosen!;
        ConsoleKey keyInfo = menuResult.lastKey;

        if (keyInfo == ConsoleKey.Escape)
        {
            Console.WriteLine("  LLeaving admin options!");

            return;
        }
        else if (option_chosen is null)
        {
            Console.WriteLine("something went wrong");
            return;
        }

        if (option_chosen == "Film toevoegen")
        {
            AddNewMovie();
        }
        else if (option_chosen == "Film verwijderen")
        {
            Console.WriteLine("Vul in de naam van de film die je wilt verwijdern:");
            string filmname = Console.ReadLine()!;
            FilmSave.Removefilm(filmname);
        }
        else if (option_chosen == "Alle films zien")
        {
            MovieWriteAndLoad film_menu = new("Movies.json");
            List<Film> AllFilms = film_menu.ReadFilms();
            foreach (Film film in AllFilms)
            {
                MovieWriteAndLoad.printfilmInfo(film);

            }
            Console.ReadKey();
        }
        else if (option_chosen == "Bestaande film inplannen")
        {
            Addmovieschedule();
        }
        FilmManagement();
    }
    public static Film? GetFilmInfo()
    {
        string title;
        while (true)
        {
            Console.Write("Vul in de titel naam: Om te stoppen houdt deze lijn leeg");
            title = Console.ReadLine()!;
            if (string.IsNullOrEmpty(title))
            {
                return null;
            }
            else break;
        }

        int runtime;
        while (true)
        {
            Console.WriteLine("Vul in de film tijd in minuten: Om te stoppen houdt deze lijn leeg");
            string? runtTimeString = Console.ReadLine();
            if (!string.IsNullOrEmpty(runtTimeString)) return null;
            else if (int.TryParse(runtTimeString, out runtime)) break;
        }

        double price;
        while (true)
        {
            Console.Write("vul in de film prijs in euro's: Om te stoppen houdt deze lijn leeg");
            string? pricestring = Console.ReadLine();
            if (!string.IsNullOrEmpty(pricestring)) return null;
            else if (double.TryParse(pricestring, out price) && price > 0) break;
        }

        double filmRating;
        while (!double.TryParse(Console.ReadLine(), out filmRating) || filmRating < 0 || filmRating > 10)
        {
            Console.Write("vul in de rating van de film van 0 tot 10: Om te stoppen houdt deze lijn leeg");            
            string? filmratingstring = Console.ReadLine();
            if (!string.IsNullOrEmpty(filmratingstring)) return null;
            else if (double.TryParse(filmratingstring, out filmRating) && filmRating > 0 && filmRating < 10) break;
        }

        int releaseYear;
        while (true)
        {
            Console.Write($"vul het Jaar van uitgave in: Om te stoppen houdt deze lijn leeg");
            string? releaseYearstring = Console.ReadLine();
            if (!string.IsNullOrEmpty(releaseYearstring)) return null;
            else if (int.TryParse(releaseYearstring, out releaseYear) && releaseYear > 1800 && releaseYear < DateTime.Now.Year) break;
        }
        string director;
        while (true)
        {
            Console.Write("vul de regisseur van de film in: Om te stoppen houdt deze lijn leeg");
            director = Console.ReadLine()!;
            if (string.IsNullOrEmpty(director))
            {
                return null;
            }
            else break;
        }


        return new Film(title, runtime, price, filmRating, releaseYear, director: director);
    }

    public static void AddNewMovie()
    {
        
            Film? film = GetFilmInfo();
            if (film is null) return;

            List<string> genres = GenreList();


            foreach (string genre in genres) 
            {
                film.Add_genre(genre);
            }

            ConsoleKeyInfo keyread;

            do
            {
                Console.WriteLine("Wil je een auditorium toevoegen? (j/n)");
                keyread = Console.ReadKey();
            } while (keyread.Key != ConsoleKey.J && keyread.Key != ConsoleKey.N);

            if (keyread.Key == ConsoleKey.J)
            {
                film.AddDateTimeAndAuditorium();
            }

            FilmSave.AppendToJason(film);
    }


    public static void Addmovieschedule()
    {
        List<Film> films = FilmSave.ReadFilms();
        List<string> options = films.Select(film => film.Title).ToList();

        string MenuName = "Kies het film dat u wilt inplannen";

        (string? optionChosen, ConsoleKey lastKey) chosenresult = BasicMenu.MenuBasic(options, MenuName);

        if (chosenresult.lastKey == ConsoleKey.Escape)
        {
            return;
        }
        else if (chosenresult.optionChosen is null)
        {
            return;
        }

        string titleToFind = chosenresult.optionChosen;

        List<Film> FoundFilms = films.Where(film => film.Title == titleToFind).ToList();
        if (FoundFilms.Count == 0)
        {
            return;
        }

        Film toFind = FoundFilms.First();
        toFind.AddDateTimeAndAuditorium();
    }


    public static List<string> GenreList()
    {
        System.Console.WriteLine("Vul in de genre voor deze film\n als er meer dan 1 is doe dit (,) tussen de genre's");
        string genrestring = Console.ReadLine()!;
        List<string> genresList;

        if (genrestring!.Contains(","))
        {
            genresList = genrestring.Split(",").Select(genre => genre.Trim()).ToList();
        }
        else
        {
            genresList = new List<string> { genrestring.Trim() };
        }
        // System.Console.WriteLine($"{string.Join(" - ", genresList)}");

        return genresList;
    }

}