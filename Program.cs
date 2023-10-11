using data_acces.Tests;
class Program
{
    static void Main(string[] args)
    {
        foreach (string arg in args)
        {
            Console.WriteLine(arg);
            switch(arg)
            {
                case "TESTSAVE":
                {
                    FilmSaveTest saveTest = new();
                    saveTest.read_films_test("Test film", 1, 1, 1);
                    break;
                }
                case "TESTMAP":
                {
                    AuditoriumMap500 map500 = new AuditoriumMap500();
                    map500.TakeSeats();
                    return;
                }
                default:
                {
                    Console.WriteLine("this is not a valid one");
                    break;
                }
            }
        }
        FrontPage.MainMenu();
    }
}