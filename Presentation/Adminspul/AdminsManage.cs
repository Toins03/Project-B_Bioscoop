static class AdminsManage
{
    public static void AdminmanageMenu()
    {
        Console.Clear();
        List<string> options = new List<string>()
        {
            "View admins",
            "Add admins",
            "Remove admins",
        };

        (string? optionChosen, ConsoleKey lastKey) menuResult = BasicMenu.MenuBasic(options, "Manage admins");

        string option_chosen = menuResult.optionChosen;
        ConsoleKey keyInfo = menuResult.lastKey;

        if (keyInfo == ConsoleKey.Escape)
        {
            Console.WriteLine(" LLeaving admin options!");
            return;
        }
        else if (option_chosen is null) 
        {
            Console.WriteLine("something went wrong");
            return;
        }

        if (option_chosen == "Add admins")
        {
            AddAdmin();
        }
        else if (option_chosen == "Remove admins")
        {
            RemoveAdmin();
        }
        else if (option_chosen == "View admins")
        {
            ViewAdmins();
        }

        AdminmanageMenu();
    }

    private static void AddAdmin()
    {
        System.Console.WriteLine("Please input the name of the admin. To go back to the admin manager keep this line empty.");
        string ToAddName = Console.ReadLine()!;
        if (ToAddName is null) return;
        else if (ToAddName == "") return;
        System.Console.WriteLine("Please input the password of the admin. To go back to the admin manager keep this line empty.");
        string ToAddPassWord = Console.ReadLine()!;
        if (ToAddPassWord is null) return;
        else if (ToAddPassWord == "") return;
        AdminSave.AddAdmin(ToAddName, ToAddPassWord);
    }

    private static void RemoveAdmin()
    {
        System.Console.WriteLine("Please input the name or ID of the admin. Inputting the name is not case sensitive. To go back to the admin manager keep this line empty.");
        string ToRemove = Console.ReadLine()!;
        if (ToRemove is null)
        {
            Console.WriteLine("Invalid input");
        }
        else if (ToRemove == "") return;
        else if (int.TryParse(ToRemove, out _))
        {
            int ToRemoveInt = Convert.ToInt32(ToRemove);
            AdminSave.RemoveAdmin(ToRemoveInt);
        }
        else
        {
            AdminSave.RemoveAdmin(ToRemove);
        }

    }

    private static void ViewAdmins()
    {
        List<Admin> admins = AdminSave.GetAdmins();
        if (admins is not null)
        {
            foreach (Admin admin in admins)
            {
                System.Console.WriteLine(admin.ToString());
            }
        }
        Console.WriteLine("press any button to leave this screen.");
        Console.ReadKey();
    }
    public static void FilmManagement()
    {
        Console.Clear();
        List<string> options = new List<string>()
        {
            "View Films",
            "Add Films",
            "Remove Films",
        };

        (string? optionChosen, ConsoleKey lastKey) menuResult = BasicMenu.MenuBasic(options, "Manage admins");

        string option_chosen = menuResult.optionChosen;
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
        
        if (option_chosen == "Add Films")
        {

            var filmInfo = GetFilmInfo();
            var GenreForMovie = GenreList();


            string title = filmInfo.Title;
            int runtime = filmInfo.Runtime;
            double price = filmInfo.Price;
            double filmRating = filmInfo.FilmRating;
            int releaseYear = filmInfo.ReleaseYear;
            string directorName = filmInfo.director;

            Film film = new Film(
                title,
                runtime,
                price,
                filmRating,
                releaseYear,
                directorName,
                GenreForMovie
            );
            film.AddDateTimeAndAuditorium();

            FilmSave.AppendToJason(film);
        }
        else if (option_chosen == "Remove Films")
        {
            Console.WriteLine("Enter the name of the film to remove:");
            string filmname = Console.ReadLine()!;
            FilmSave.Removefilm(filmname);
        }
        else if (option_chosen == "View Films")
        {
            MovieWriteAndLoad film_menu = new("Movies.json");
            List<Film> AllFilms = film_menu.ReadFilms();
            foreach (Film film in AllFilms)
            {
                MovieWriteAndLoad.printfilmInfo(film);

            }
            Console.ReadKey();
        }
        FilmManagement();
    }
    public static (string Title, int Runtime, double Price, double FilmRating, int ReleaseYear, string director) GetFilmInfo()
    {
        string title;
        do
        {
            Console.Write("Enter the film title: ");
            title = Console.ReadLine()!;
        } while (string.IsNullOrWhiteSpace(title));

        int runtime;
        while (!int.TryParse(Console.ReadLine(), out runtime) || runtime <= 0)
        {
            Console.Write("Enter a valid positive integer for runtime: ");
        }

        double price;
        while (!double.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.Write("Enter a valid positive number for price: ");
        }

        double filmRating;
        while (!double.TryParse(Console.ReadLine(), out filmRating) || filmRating < 0 || filmRating > 10)
        {
            Console.Write("Enter a valid film rating between 0 and 10: ");
        }

        int releaseYear;
        while (!int.TryParse(Console.ReadLine(), out releaseYear) || releaseYear < 1800 || releaseYear > DateTime.Now.Year)
        {
            Console.Write($"Enter a valid release year between 1800 and {DateTime.Now.Year}: ");
        }
        string director;
        do
        {
            Console.Write("Enter the film director: ");
            director = Console.ReadLine()!;
        } while (string.IsNullOrWhiteSpace(director));

        return (title, runtime, price, filmRating, releaseYear, director);
    }

    public static List<string> GenreList()
    {
        System.Console.WriteLine("Enter the genre for this movie\n if there are multiple space them with (,)");
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