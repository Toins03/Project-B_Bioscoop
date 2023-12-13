using System.Xml.Serialization;

public class ShoppingCart
{
    public List<Snack> shoppingcart;

    public ShoppingCart()
    {
        shoppingcart = new List<Snack>();
    }
    public void AddtoShoppingCart(Snack snack)
    {
        shoppingcart.Add(snack);
    }

    public double ShoppingCartCosts()
    {
        double Total = 0;
        foreach (Snack snack in shoppingcart)
        {
            Total += snack.Price;
        }
        return Math.Round(Total, 2);
    }

    public void ModifyShoppingCart()
    {
        Console.Clear();
        System.Console.WriteLine("je winkelwagen:\n");
        foreach (Snack snack in shoppingcart)
        {
            System.Console.WriteLine(snack.Name);
        }
        System.Console.WriteLine("");
        bool CheckFound = false;

        string choice;
        do
        {
            System.Console.WriteLine("welke product wil je uit je winkelwagen?");
            choice = Console.ReadLine()!;

        } while (choice is null);


        int AmountOfTimes;

        do
        {
            System.Console.WriteLine("Hoeveel wil je ervan verwijderen");
        } while (!int.TryParse(Console.ReadLine(), out AmountOfTimes));



        int removedCount = 0;









        for (int i = shoppingcart.Count - 1; i >= 0; i--)
        {
            Snack snack = shoppingcart[i];
            if (choice == snack.Name)
            {
                if (removedCount == AmountOfTimes)
                {
                    break; // controle over hoeveel er eruit gaat
                }

                shoppingcart.RemoveAt(i);
                CheckFound = true;
                removedCount++;

            }
        }

        if (CheckFound == true)
        {
            System.Console.WriteLine("het product is uit je winkelwagen gehaalt");
        }
        else if (CheckFound == false)
        {
            System.Console.WriteLine("het product is niet gevonden in de winkelwagen");
        }
        Console.ReadKey();




    }
    public void ModifyShoppingCartForTests(string choice, int AmountOfTimes)
    {


        bool CheckFound = false;

        int removedCount = 0;

        for (int i = shoppingcart.Count - 1; i >= 0; i--)
        {
            Snack snack = shoppingcart[i];
            if (choice == snack.Name)
            {
                if (removedCount == AmountOfTimes)
                {
                    break; // controle over hoeveel er eruit gaat
                }

                shoppingcart.RemoveAt(i);
                CheckFound = true;
                removedCount++;

            }
        }

        if (CheckFound == true)
        {
            System.Console.WriteLine("het product is uit je winkelwagen gehaalt");
        }
        else if (CheckFound == false)
        {
            System.Console.WriteLine("het product is niet gevonden in de winkelwagen");
        }

    }

}