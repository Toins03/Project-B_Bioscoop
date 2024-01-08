using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;

static class FilmSave
{
    public static string PathName = "Movies.json";

    public static List<Film> ReadFilms()
    {
        if (File.Exists(PathName))
        {
            StreamReader reader = new(PathName);
            string filefromjson = reader.ReadToEnd();
            List<Film> films = JsonConvert.DeserializeObject<List<Film>>(filefromjson)!;
            reader.Close();
            return films;
        }
        else return new List<Film> { };
    }

    public static void PrintAllFilms()
    {
        if (File.Exists(PathName))
        {
            List<Film> films = ReadFilms();
            foreach (Film film in films)
            {
                if (film is not null) Console.WriteLine(film.ToString());
            }
        }
    }

    public static List<Film> FindFilmsWithSchedule(List<MovieScheduleInformation> scheduleInformation)
    {
        List<Film> allfilms = ReadFilms();

        List<string> filmtitles = scheduleInformation.Select(schedule => schedule.Title).ToList();


        List<Film> scheduledFilms = allfilms.Where(film => filmtitles.Contains(film.Title)).ToList();
        return scheduledFilms;
    }


    public static Film? FindFilmwithTitle(string toFind)
    {
        List<Film>? allFilms = FilmSave.ReadFilms();
        if (allFilms is null)
        {
            Console.WriteLine("Geen Films van deze waren gevonden");
            Console.ReadKey();
            return null;
        }

        List<Film> filmtitles = allFilms.Where(film => film.Title == toFind).ToList();

        if (filmtitles.Count == 0)
        {
            Console.WriteLine($"Film {toFind} niet gevonden");
            Console.ReadKey();
            return null;
        }
        Film toReturn = filmtitles.First();

        return toReturn;
    }

    public static void printfilmInfo(Film film)
    {
        Console.WriteLine();
        Console.WriteLine("Movie Information:");
        Console.WriteLine($"Title: {film.Title}");
        Console.WriteLine($"Genres: {string.Join(", ", film.Genres!)}");
        Console.WriteLine($"§Director: {film.Director}");
        Console.WriteLine($"Release Year: {film.ReleaseYear}");
        Console.WriteLine($"Film RunTime: {film.FilmRunTime} minutes");
        Console.WriteLine($"Film Price: Euro {film.FilmPrice}");
        Console.WriteLine($"Film Rating: {film.FilmRating}");
        Console.WriteLine();
    }

    public static void AddToJson(Film filmToAdd)
    {
        List<Film> films = ReadFilms();
        films.Add(filmToAdd);
        WritefilmList(films);
    }
    public static void AppendToJason(Film filmToAdd)
    {
        List<Film> films = ReadFilms();
        films.Add(filmToAdd);
        WritefilmList(films);
    }

    public static void Removefilm(string filmName)
    {
        List<Film> films = ReadFilms();
        Film ToDelete = null!;
        foreach (Film film in films)
        {
            if (film.Title == filmName)
            {
                ToDelete = film;
                break;
            }
        }

        if (ToDelete is null)
        {
            Console.WriteLine("The film with this Name does not exist!");
            return;
        }
        else
        {
            MovieScheduleInformation.RemoveMovieScheduleObject(filmName);
            films.Remove(ToDelete);
            WritefilmList(films);
            Console.WriteLine("The film with this name has been deleted");
        }
    }

    public static void AddCustomerToFilm(string filmtoaddto, Customer customertoadd)
    {
        List<Film> films = ReadFilms();
        System.Console.WriteLine(filmtoaddto);

        foreach (Film film in films)
        {
            if (film.Title == filmtoaddto)
            {
                film.AddCinemaAudience(customertoadd);
            }
        }
        WritefilmList(films);
    }

    private static void WritefilmList(List<Film> ToWrite)
    {
        StreamWriter writer = new(PathName);
        string list_to_json = JsonConvert.SerializeObject(ToWrite, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();
    }
}