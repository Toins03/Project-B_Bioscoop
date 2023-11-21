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

        // Extract the first key from DateAndAuditorium dictionary
        if (DateAndAuditorium is not null && DateAndAuditorium.Count > 0)
        {
            string dateString = DateAndAuditorium.Keys.First();
            DateTime dateTime = DateTime.ParseExact(dateString, "MM-dd-yyyy HH:mm", null);

            DayOfWeek dayOfWeek = dateTime.DayOfWeek;

            this.FirstDateAndAuditoriumKey = $"{DateAndAuditorium.Keys.First()} - {dayOfWeek}";
        }
        else
        {
            this.FirstDateAndAuditoriumKey = "No DateAndAuditorium information available";
        }

        this.DateAndAuditorium = DateAndAuditorium!;
    }

    // Add a property to store the first key from the dictionary
    public string FirstDateAndAuditoriumKey { get; }


    public void Add_genre(string genre)
    {
        this.Genres.Add(genre);
    }

    public void AddCinemaAudience(Customer customer)
    {

        CinemaAudience.Add("Customer ID " + customer.ID + " Customer Name " + customer.Name);
    }

    public void ChangeRating(double newRating)
    {
        this.FilmRating = newRating;
    }

    public override string ToString()
    {
        string toReturn = @$"Title: {this.Title}
Genres: {string.Join(", ", this.Genres!)}
Director: {this.Director}
Release Year: {this.ReleaseYear}
Film RunTime: {this.FilmRunTime} minutes
Film Price: Euro {this.FilmPrice}
Film Rating: {this.FilmRating}";
        return toReturn;
    }
    public override bool Equals(object? obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        if (GetType() != obj.GetType()) return this.Equals(obj as Film);
        // TODO: write your implementation of Equals() here
        return base.Equals(obj);
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return base.GetHashCode();
    }

    public bool Equals(Film? film)
    {
        if (this is null && film is null) return true;
        else if (film is null ^ this is null) return false;
        else if (film!.Title == this.Title && film.Genres == this.Genres && film.FilmRunTime == this.FilmRunTime && film.Director == this.Director) return true;
        else return false;
    }

    public static bool operator ==(Film? a1, Film a2)
    {
        if (a1 is null && a2 is null) return true;
        else if (a1 is null || a2 is null) return false;
        else return a1.Equals(a2);
    }

    public static bool operator !=(Film? a1, Film a2)
    {
        return !(a1 == a2);
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
}