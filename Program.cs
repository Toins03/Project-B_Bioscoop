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
                case "empty":
                    {
                        Console.WriteLine("current errors and warnings shown. Press any key to continue.");
                        Console.ReadKey();
                        return;
                    }
                default:
                    {
                        Console.WriteLine("this is not a valid one");
                        Console.ReadKey();
                        break;
                    }
            }
        }
        FrontPage.MainMenu(null!);
    }
}