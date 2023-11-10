using Newtonsoft.Json;

public class Film
{
    public string Title;
    public List<string> Genres;
    public string Director;
    public int ReleaseYear;
    public int FilmRunTime;
    public double FilmPrice = 0;
    public double FilmRating = 5;
    public List<string> CinemaAudience = new() { };

    public Dictionary<string, string> DateAndAuditorium = new Dictionary<string, string>();




    [JsonConstructor]
    public Film(
    string title,
    int runtime,
    double price,
    double filmrating,
    int ReleaseYear,
    List<string> genres = null!,
    string director = "unknown",
    List<string> cinemaAudience = null!,
    Dictionary<string, string> DateAndAuditorium = null!)
    {
        this.Genres = new List<string>() { };
        if (genres is not null) this.Genres = genres;
        this.Title = title;
        this.FilmRunTime = runtime;
        this.FilmPrice = price;
        this.FilmRating = filmrating;
        this.ReleaseYear = ReleaseYear;
        this.Director = director;
        if (cinemaAudience is not null) this.CinemaAudience = cinemaAudience;
        this.DateAndAuditorium = DateAndAuditorium;
    }

    public void Add_genre(string genre)
    {
        this.Genres.Add(genre);
    }

    public void AddCinemaAudience(Customer customer)
    {

        CinemaAudience.Add("Customer ID " + customer.ID + " Customer Name " + customer.Name);
    }

    public void AddDateTimeAndAuditorium(string AuditorumNumber)
    {
        DateTime specificDateTime = new DateTime(2023, 11, 11, 12, 0, 0); // November 11, 2023, 12:00 PM
        // DayOfWeek dayOfWeek = specificDateTime.DayOfWeek;
        // string dayOfWeekString = dayOfWeek.ToString();
        // deze code later gebruiken voor het uitlezen van de json file maar niet om ernaar toe te schrijven
        string formattedDateTime = specificDateTime.ToString($"MM-dd-yyyy HH:mm");



        DateAndAuditorium.Add(formattedDateTime, AuditorumNumber);
    }

    // public string GetDate()
    // {
    //     List<Film> films = FilmSave.ReadFilms();
    //     foreach (Film this.base in films)
    //     {

    //     }
    // }
}