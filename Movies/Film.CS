using System.Net;
using Newtonsoft.Json;

public class Film
{


    public string Title;
    public List<string> Genres;
    public string Director;
    public int ReleaseYear;
    public int FilmRunTime;
    public double FilmPrice = 0;
    private double _filmRating;
    public double FilmRating {get => _filmRating; set => _filmRating = Math.Clamp(value, 0, 10);}
    public List<string> CinemaAudience = new() { };

    public Dictionary<DateTime, string> DateAndAuditorium = new Dictionary<DateTime, string>();


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
    Dictionary<DateTime, string> DateAndAuditorium = null!)
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
        //test
        this.DateAndAuditorium = DateAndAuditorium ?? new Dictionary<DateTime, string>();
    }

    public Film(
    string title,
    int runtime,
    double price,
    double filmrating,
    int ReleaseYear,
    string director,
    List<string> genres,
    List<string> cinemaAudience = null!,
    Dictionary<DateTime, string> DateAndAuditorium = null!)
    {
        this.Title = title;
        this.FilmRunTime = runtime;
        this.FilmPrice = price;
        this.FilmRating = filmrating;
        this.ReleaseYear = ReleaseYear;
        this.Director = director;
        this.Genres = genres ?? new List<string>();
        this.CinemaAudience = cinemaAudience ?? new List<string>();
        this.DateAndAuditorium = DateAndAuditorium ?? new Dictionary<DateTime, string>();
    }

    // Add a property to store the first key from the dictionary

    // string.Join(", ", this.Genres!)
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

    public void AddDateTimeAndAuditorium()
    {
        string AuditorumNumber;
        do
        {
            System.Console.WriteLine("\nVoer het nummer in van de zaal waarin je wilt dat deze film wordt vertoond.");
            System.Console.WriteLine("(1,2 of 3)");
            AuditorumNumber = Console.ReadLine()!;
        } while (AuditorumNumber != "1" && AuditorumNumber != "2" && AuditorumNumber != "3");
        DateTime dateAndTime = AskDateAndTime();

        List<List<string>> Map = new();

        switch (AuditorumNumber)
        {
            case "1":
                {
                    AuditoriumMap150 cinemaMap = new();
                    cinemaMap.CreateCinemaMap();
                    Map = cinemaMap.CinemaMap1;
                    break;
                }
            case "2":
                {
                    AuditoriumMap300 cinemaMap = new();
                    cinemaMap.CreateCinemaMap();
                    Map = cinemaMap.CinemaMap1;
                    break;
                }
            case "3":
                {
                    AuditoriumMap500 cinemaMap = new();
                    cinemaMap.CreateCinemaMap();
                    Map = cinemaMap.CinemaMap1;
                    break;
                }
            default:
                {
                    Console.WriteLine("Iets ging fout");
                    return;
                }
        }


        SetTime(dateAndTime, AuditorumNumber);

        Dictionary<DateTime, List<List<string>>> dateAndAuditorium = new()
        {
            { dateAndTime, Map }
        };

        MovieScheduleInformation ToAdd = new(this.Title, dateAndAuditorium);

        MovieScheduleInformation.AddNewMovieScheduleInfo(ToAdd, dateAndTime);

        AskRepeatSchedule(ToAdd, dateAndTime);

    }

    public static void AskRepeatSchedule(MovieScheduleInformation toAdd, DateTime starttime)
    {
        while (true)
        {
            Console.WriteLine("Wilt u dat deze film op de volgende dagen wordt herhaald op dezelfde tijd? (ja/nee)");
            string answer = Console.ReadLine()!;
            if (answer is null)
            {
                Console.WriteLine("Geen antwoord gevonden");
                Console.ReadKey();
                return;
            }
            else if (answer == "nee")
            {
                return;
            }
            else if (answer == "ja")
            {
                break;
            }
        }

        while (true)
        {
            Console.WriteLine("Voor hoeveel dagen wilt u deze film laten herhalen? Houd deze lijn leeg om terug te gaan.");
            string? repeatAmountString = Console.ReadLine();
            if (repeatAmountString is null)
            {
                Console.WriteLine("Geen antwoord gevonden");
                Console.ReadKey();
                return;
            }
            else if (repeatAmountString == "")
            {
                Console.WriteLine("U gaat nu terug");
                Console.ReadKey();
                return;
            }
            else if (int.TryParse(repeatAmountString, out int repeatAmountint))
            {
                repeatAmountint = Math.Clamp(repeatAmountint, 0, 100);
                if (repeatAmountint == 0)
                {
                    return;
                }
                else
                {
                    for (int i = 1; i <= repeatAmountint; i++)
                    {
                        TimeSpan timeAdded = new TimeSpan(days: i, hours: 0, minutes: 0, seconds: 0);

                        toAdd.ScreeningTimeAndAuditorium[starttime + timeAdded] = toAdd.ScreeningTimeAndAuditorium[starttime];
                        MovieScheduleInformation.AddNewMovieScheduleInfo(toAdd, starttime + timeAdded);
                    }
                    break;
                }
            }


        }

    }

    private DateTime AskDateAndTime()
    {
        System.Console.WriteLine("vul hier de tijden in waar de film te zien is in jou gekozen auditorium");
        System.Console.WriteLine("druk op een knop om verder te gaan");
        Console.ReadKey();


        int year;


        // = DateTime.Now;
        Console.WriteLine("Voer het jaar in waar het te zien is in de bioscoop: ");
        while (!int.TryParse(Console.ReadLine(), out year) || year <= 0)
        {
            Console.WriteLine("Voer het jaar in waar het te zien is in de bioscoop: ");
        }

        Console.WriteLine("Voer het maand in: ");
        System.Console.WriteLine("van 1 tot en met 12");

        int month;
        while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
        {
            Console.WriteLine("Voer het maand in: ");
            System.Console.WriteLine("van 1 tot en met 12");
        }

        Console.WriteLine("Voer de dag in: ");
        System.Console.WriteLine("van 1 tot en met 31");

        int day;
        while (!int.TryParse(Console.ReadLine(), out day) || day < 1 || month > 31)
        {
            Console.WriteLine("Voer de dag in: ");
            System.Console.WriteLine("van 1 tot en met 31");

        }

        Console.WriteLine("Voer de uur in: ");
        System.Console.WriteLine("van 0 tot en met 24");

        int hour;
        while (!int.TryParse(Console.ReadLine(), out hour) || hour < 0 || hour > 24)
        {
            Console.WriteLine("Voer de uur in: ");
            System.Console.WriteLine("van 0 tot en met 24");
        }

        Console.WriteLine("Voer het minuut in: ");
        System.Console.WriteLine("van 0 tot en met 60");

        int minutes;
        while (!int.TryParse(Console.ReadLine(), out minutes) || minutes < 0 || minutes > 60)
        {
            Console.WriteLine("Voer het minuut in: ");
            System.Console.WriteLine("van 0 tot en met 60");
        }

        DateTime toReturn = new DateTime(year: year, month: month, day: day, hour: hour, minute: minutes, second: 0);

        return toReturn;


    }
    private void SetTime(DateTime specifiedTime, string AuditorumNumber)
    {
        // string formattedDateTime = specifiedTime.ToString($"MM-dd-yyyy HH:mm");
        DateAndAuditorium.Add(specifiedTime, AuditorumNumber);
    }

    public string ShowDate()
    {
        string Date;

        foreach (DateTime key in DateAndAuditorium.Keys)
        {
            // wijzig dit date display
            Date = key.ToString();
            return Date;

        }
        return "no date to be seen";
    }


    public static List<Film> LoadFilmFromJsonFile()
    {
        if (File.Exists("Movies.json"))
        {
            try
            {
                // Read the JSON from the file
                string json = File.ReadAllText("Movies.json");

                // Deserialize the JSON into a list of Customer objects
                List<Film> Films = JsonConvert.DeserializeObject<List<Film>>(json)!;

                return Films;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., JSON parsing errors)
                Console.WriteLine($"Error loading customer data: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File not found: {"Movies.json"}");
        }

        return null!;
    }


}