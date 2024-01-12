public class RentedMovieInfo : IEquatable<RentedMovieInfo>
{
    public string FilmTitle;
    public List<string> SeatsTaken;
    public DateTime TimeViewing;
    public string ConfirmationCode;

    public RentedMovieInfo(string filmTitle, List<string> seatstaken, DateTime timeViewing, string confirmationCode)
    {
        this.FilmTitle = filmTitle;
        this.SeatsTaken = seatstaken;
        this.TimeViewing = timeViewing;
        this.ConfirmationCode = confirmationCode;
    }

    public override string ToString()
    {
        return $"Title: {this.FilmTitle}, Seats chosen: {string.Join(", ", this.SeatsTaken)}, Time viewing: {this.TimeViewing.ToString("dd/MM/yy HH:mm")}, {auditoriumNumber()}\n    Bewijscode: {this.ConfirmationCode}\n\n";
    }
    public string auditoriumNumber()
    {
        if (ConfirmationCode.Count() > 0)
        {
            char auditoriumZaal = ConfirmationCode[1];
            return $"op zaal {auditoriumZaal}";
        }
        return "geen zaal gevonden dit is fout bij RentedMovieInfo.cs Method AuditoriumNumber";

    }

    public string seeTimeViewing()
    {
        string toReturn = this.TimeViewing.ToString("dd/MM/yy HH:mm");
        return toReturn;
    }

    public bool Equals(RentedMovieInfo? movie)
    {
        if (this is null && movie is null) return true;
        else if (this is null || movie is null) return false;
        else if (this.FilmTitle == movie.FilmTitle && this.SeatsTaken == movie.SeatsTaken && this.TimeViewing == movie.TimeViewing) return true;
        else return false;
    }

    public static bool operator ==(RentedMovieInfo? m1, RentedMovieInfo? m2)
    {
        if (m1 is null && m2 is null) return true;
        else if (m1 is null || m2 is null) return false;
        else return m1.Equals(m2);
    }

    public static bool operator !=(RentedMovieInfo? m1, RentedMovieInfo? m2)
    {
        return !(m1 == m2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is RentedMovieInfo movie) return Equals(movie);
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}