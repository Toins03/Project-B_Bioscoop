// using data_acces.Tests;
class Program
{
    static void Main(string[] args)
    {
        foreach (string arg in args)
        {

            Console.WriteLine(arg);
            switch (arg)
            {
                case "snack":
                    {
                        Console.WriteLine("");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("this is not a valid one");
                        break;
                    }
            }
        }
        FrontPage.MainMenu(null!);
    }
}