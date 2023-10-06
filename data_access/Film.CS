using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

public class Film
{
    public string Title;
    public List<string> Genres = new() {};
    public string Director;
    public int ReleaseYear;
    public int FilmRunTime;
    public double FilmPrice = 0;
    public double FilmRating = 5;

    [JsonConstructor]
    public Film(string title,  int runtime, double price, double filmrating, string director = "unknown")
    {
        this.Title = title;
        this.FilmRunTime = runtime;
        this.FilmPrice = price;
        this.FilmRating = filmrating;
        this.Director = director;
    }


}