using Newtonsoft.Json;

public class Film: IEquatable<Film>
{
    public string Title;
    public List<string> Genres;
    public string Director;
    public int ReleaseYear;
    public int FilmRunTime;
    public double FilmPrice = 0;
    public double FilmRating = 5;
    public List<string> CinemaAudience = new() {};


    [JsonConstructor]
    public Film(string title, int runtime, double price, double filmrating, List<string> genres = null!, string director = "unknown", List<string> cinemaAudience = null!)
    {
        this.Genres = new List<string>() { };
        if (genres is not null) this.Genres = genres;
        this.Title = title;
        this.FilmRunTime = runtime;
        this.FilmPrice = price;
        this.FilmRating = filmrating;
        this.Director = director;
        if (cinemaAudience is not null) this.CinemaAudience = cinemaAudience;
    }

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
        string toReturn =@$"Title: {this.Title}
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
        return base.Equals (obj);
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
}