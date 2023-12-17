
public class Screenings
{
    public string Date { get; set; }
    public List<List<string>> Auditorium {  get; set; }
    public List<Customer> Audience { get; set; }

    public Screenings(string date, List<List<string>> auditorium, List<Customer> audience)
    {
        this.Date = date;
        this.Auditorium = auditorium;
        this.Audience = audience;
    }
}