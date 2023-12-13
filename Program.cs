// using data_acces.Tests;
class Program
{
    static void Main(string[] args)
    {
        foreach (string arg in args)
        {

            Console.WriteLine(arg);
            switch (arg)
            {
                case "snack":
                    {
                        Snack.ChooseToAddSnackOrNot();
                        break;
                    }
                case "modify movies":
                    {
                        List<Film> films = FilmSave.ReadFilms();
                        foreach (var film in films)
                        {
                            if (film.Title == "Mario")
                            {
                                film.Add_genre("survival");
                                System.Console.WriteLine(film.Genres);
                                Console.ReadKey();
                            }
                        }

                        FilmSave.WritefilmList(films);
                        return;
                    }
                case "START":
                    {
                        var filmInfo = FilmsManage.GetFilmInfo();

                        string title = filmInfo.Title;
                        int runtime = filmInfo.Runtime;
                        double price = filmInfo.Price;
                        double filmRating = filmInfo.FilmRating;
                        int releaseYear = filmInfo.ReleaseYear;

                        Film film = new Film(
                            title: title,
                            runtime: runtime,
                            price: price,
                            filmrating: filmRating,
                            ReleaseYear: releaseYear
                        );
                        System.Console.WriteLine(film.ToString());

                        film.AddDateTimeAndAuditorium();
                        System.Console.WriteLine(film.ToString());

                        FilmSave.AppendToJason(film);
                        return;
                    }
                case "SAVE":
                    {

                        string genrestring = Console.ReadLine();
                        List<string> genresList;

                        if (genrestring.Contains(","))
                        {
                            genresList = genrestring.Split(",").Select(genre => genre.Trim()).ToList();
                        }
                        else
                        {
                            genresList = new List<string> { genrestring.Trim() };
                        }
                        System.Console.WriteLine($"{string.Join(" - ", genresList)}");

                        // Film myFilm = new Film(
                        // title: "Inception",
                        // runtime: 148,
                        // price: 10.99,
                        // filmrating: 4.5,
                        // ReleaseYear: 2000,
                        // genres: new List<string> { "Sci-Fi", "Action", "Thriller" },
                        // director: "Christopher Nolan",
                        // cinemaAudience: ,
                        // DateAndAuditorium: new Dictionary<DateTime, string>());
                        // System.Console.WriteLine("object created");
                        // myFilm.AddDateTimeAndAuditorium();
                        // System.Console.WriteLine(myFilm.ShowDate());

                        // FilmSave.AddToJson(myFilm);

                        return;

                    }


                case "TESTSAVE":
                    {
                        MovieWriteAndLoad film_menu = new("Movies.json");
                        List<Film> options = film_menu.ReadFilms();
                        System.Console.WriteLine(options[4].ShowDate());
                        return;
                    }


                case "TESTCUSTOMERTOFILM":
                    {
                        // List<Film> films = FilmSave.ReadFilms();
                        // List<Customer> customers = Customer.LoadFromJsonFile();
                        // FilmSave.AddCustomerToFilm(films[0], customers[2]);
                        // System.Console.WriteLine("Done");
                        // Console.ReadKey();
                        break;
                    }                
                default:
                    {
                        Console.WriteLine("this is not a valid one");
                        break;
                    }
            }
        }
        FrontPage.MainMenu(null!);
    }
}