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
} 