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
                        Snack.ChooseToAddSnackOrNot();
                        break;
                    }                
                default:
                    {
                        Console.WriteLine("this is not a valid one");
                        break;
                    }
            }
        }
        Snack.ChooseToAddSnackOrNot();
    }
}