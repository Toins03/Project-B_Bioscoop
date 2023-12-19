public class RentedMovieInfo
{
    public string FilmTitle;
    public List<string> SeatsTaken;
    public DateTime TimeViewing;

    public RentedMovieInfo(string filmTitle, List<string> seatstaken, DateTime timeViewing)
    {
        this.FilmTitle = filmTitle;
        this.SeatsTaken = seatstaken;
        this.TimeViewing = timeViewing;
    }

    public override string ToString()
    {
        return $"Title: {this.FilmTitle}, Seats chosen: {string.Join(", ", this.SeatsTaken)}, Time viewing: {this.TimeViewing}";
    }

    public string seeTimeViewing()
    {
        string toReturn = $"{this.TimeViewing.Day}/{this.TimeViewing.Month}/{this.TimeViewing.Year} {this.TimeViewing.Hour}:{this.TimeViewing.Minute}";
        return toReturn;
    }
} 