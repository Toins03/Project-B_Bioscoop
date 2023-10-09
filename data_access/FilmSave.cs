using Newtonsoft.Json;

class FilmSave
{
    public List<Film> Films = new() { };

    public string PathName;

    public FilmSave(string Json_file)
    {
        this.PathName = Json_file;
        if (File.Exists(Json_file))
        {
            Films = ReadFilms();
        }
    }

    public List<Film> ReadFilms()
    {
        if (File.Exists(this.PathName))
        {
            StreamReader reader = new(this.PathName);
            string filefromjson = reader.ReadToEnd();
            List<Film> films = JsonConvert.DeserializeObject<List<Film>>(filefromjson)!;
            reader.Close();
            return films;
        }
        else return new List<Film> {};
    }

    public void ReadJsonFile()
    {
        if (File.Exists(this.PathName))
        {
            StreamReader reader = new(this.PathName);
            string filefromjson = reader.ReadToEnd();
            List<Film> films = JsonConvert.DeserializeObject<List<Film>>(filefromjson)!;
            reader.Close();
            foreach (Film film in films)
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
        }
    }

    public void AddToJson(Film filmToAdd)
    {
        List<Film> films = this.ReadFilms();
        films.Add(filmToAdd);
        StreamWriter writer = new(this.PathName);
        string list_to_json = JsonConvert.SerializeObject(films, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();
    }
}