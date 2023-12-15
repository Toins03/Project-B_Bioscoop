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
                        List<MovieScheduleInformation> lijst = MovieScheduleInformation.ToList()!;
                        foreach (var item in lijst)
                        {
                            System.Console.WriteLine(item.Title);
                        }
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