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
                case "FilterMovies":
                    {
                        MovieWriteAndLoad film_menu = new MovieWriteAndLoad("Movies.json");

                        List<Film> Movies = film_menu.ReadFilms();

                        // Define the time gap (2 hours in this case)
                        TimeSpan timeGap = TimeSpan.FromHours(2);

                        // Filter the movies where any DateAndAuditorium entry has a date in the future and not within the next 2 hours
                        List<Film> MoviesPlayedInTheFuture = Movies
                            .Where(movie => movie.DateAndAuditorium.Any(kv => kv.Key > DateTime.Now.Add(timeGap)))
                            .ToList();

                        foreach (Film movie in MoviesPlayedInTheFuture)
                        {
                            Console.WriteLine("Movie Title: {0}", movie.Title);

                            foreach (var keyValue in movie.DateAndAuditorium)
                            {
                                Console.WriteLine("Key: {0}, Value: {1}", keyValue.Key, keyValue.Value);
                            }

                            Console.WriteLine("---------------");
                        }
                        Console.ReadKey();
                        break;
                    }
                case "MovieSchedule":
                    {
                        string movieTitle = Console.ReadLine()!;
                        bool CheckIfInMovies = false;
                        // check if movieTitle is even in movies
                        MovieWriteAndLoad film_menu = new MovieWriteAndLoad("Movies.json");
                        List<Film> Movies = film_menu.ReadFilms();
                        foreach (Film movie in Movies)
                        {
                            if (movie.Title == movieTitle)
                            {
                                CheckIfInMovies = true;
                            }
                        }
                        if (CheckIfInMovies)
                        {
                            MovieScheduleInformation machine = new MovieScheduleInformation();
                            machine.RemoveMovieScheduleObject(movieTitle);
                        }
                        else
                        {
                            System.Console.WriteLine("de film is niet gevonden");
                        }


                        Console.ReadKey();
                        break;
                    }
                case "empty":
                    {
                        Console.WriteLine("current errors and warnings shown. Press any key to continue.");
                        Console.ReadKey();
                        return;
                    }
                default:
                    {
                        Console.WriteLine("this is not a valid one");
                        Console.ReadKey();
                        break;
                    }
            }
        }
        FrontPage.MainMenu(null!);
    }
}