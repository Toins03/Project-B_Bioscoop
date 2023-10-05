public class Film
{
    public string Title;
    public int FilmRunTime;
    public double FilmPrice;
    public double FilmRating;

    public Film(string title, int runtime, double price, double filmrating)
    {
        this.Title = title;
        this.FilmRunTime = runtime;
        this.FilmPrice = price;
        this.FilmRating = filmrating;
    }
}