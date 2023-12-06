class ShoppingCart
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
        return Total;
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
        System.Console.WriteLine("welke product wil je uit je winkelwagen?");

        bool CheckFound = false;
        string choice = Console.ReadLine()!;


        for (int i = shoppingcart.Count - 1; i >= 0; i--)
        {
            Snack snack = shoppingcart[i];
            if (choice == snack.Name)
            {
                shoppingcart.RemoveAt(i);
                CheckFound = true;
                // zet hier break als je alleen wilt dat de eerste snack eruit gaat zondar break gaan ze allemaal weg met hetzelfde naam
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

}