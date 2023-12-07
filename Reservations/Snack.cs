
class Snack
{
    public string Name { get; }

    public int Price { get; }


    public Snack(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public static string DisplayShoppingCart(ShoppingCart shoppingcart)
    {
        System.Console.WriteLine("");
        string Items = $"Je ShoppingCart - je snack kosten tot nu toe: €{shoppingcart.ShoppingCartCosts()}\ndit zijn de producten die je tot nu toe wilt kopen\n\n";

        foreach (Snack snack in shoppingcart.shoppingcart)
        {

            Items += $"{snack.Name}\n";
        };
        return Items;
    }

    private static void FoodOptionsToList(ShoppingCart ShoppingCart)
    {
        List<Snack> Food = LoadFoodOptions();
        LoopOverSnacks(Food, ShoppingCart);


    }

    private static List<Snack> LoadFoodOptions()
    {
        List<Snack> Snacks = new();
        Snack bueno = new Snack("bueno", 30);
        Snack LaysChips = new Snack("Lays Chips", 20);
        Snacks.Add(bueno);
        Snacks.Add(LaysChips);
        return Snacks;

    }

    private static List<Snack> LoadDrinkOptions()
    {
        List<Snack> Snacks = new();
        Snack Fanta = new Snack("Fanta 1L", 30);
        Snack Cola = new Snack("Cola 1L", 20);
        Snacks.Add(Fanta);
        Snacks.Add(Cola);
        return Snacks;

    }
    private static void DrinksOptionsToList(ShoppingCart shoppingCart)
    {
        List<Snack> Drinks = LoadDrinkOptions();
        LoopOverSnacks(Drinks, shoppingCart);

    }


    public static void ChooseToAddSnackOrNot()
    {
        string choice;
        do

        {
            System.Console.WriteLine("wil jij een snack toevoegen aan je reservering (ja/nee)");
            choice = Console.ReadLine()!;


        } while (choice != "ja" && choice != "nee");
        System.Console.WriteLine("out");

        if (choice == "ja")
        {
            ShoppingCart shoppingCart = new();
            List<string> options = new List<string> { };
            options.Add("eten");
            options.Add("drinken");

            SnackMainMenu(options, shoppingCart);
        }


    }
    private static void SnackMainMenu(List<string> options, ShoppingCart shoppingcart)
    {
        while (true)
        {
            int selectedIndex = 0;
            ConsoleKeyInfo keyInfo;
            string line = new string('=', Console.WindowWidth);

            do
            {
                Console.Clear();
                System.Console.WriteLine(line);

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.WriteLine("--> " + options[i]);
                    }
                    else
                    {
                        Console.WriteLine("    " + options[i]);
                    }
                }
                System.Console.WriteLine(line);
                System.Console.WriteLine(@"gebruik WASD keys om je optie te selecteren druk daarna op Enter op je keuze te bevestigen
Druk op ESC om te vertrekken.
druk op spatie om je bestellingen to confirmeren
druk op p om je producten uit je winkelwagen te verwijderen
");
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
                {
                    selectedIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
                {
                    selectedIndex++;
                }
            } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Spacebar && keyInfo.Key != ConsoleKey.P);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                System.Console.WriteLine(" SSee you!");
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                Confirmation();
                break;
            }
            else if (keyInfo.Key == ConsoleKey.P)
            {
                shoppingcart.ModifyShoppingCart();
                break;
            }

            Console.WriteLine("je hebt dit geselecteerd: " + options[selectedIndex]);
            Console.ReadKey();
            Console.Clear();

            if (options[selectedIndex] == "eten")
            {
                FoodOptionsToList(shoppingcart);
                Console.ReadKey();
            }
            else if (options[selectedIndex] == "drinken")
            {
                DrinksOptionsToList(shoppingcart);
                Console.ReadKey();

            }
        }

    }
    private static void LoopOverSnacks(List<Snack> options, ShoppingCart shoppingcart)
    {
        while (true)
        {
            int selectedIndex = 0;
            ConsoleKeyInfo keyInfo;
            string line = new string('=', Console.WindowWidth);

            do
            {
                Console.Clear();
                System.Console.WriteLine(line);

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.WriteLine("--> " + options[i].Name + " - " + "€" + options[i].Price);
                    }
                    else
                    {
                        Console.WriteLine("    " + options[i].Name);
                    }

                }
                System.Console.WriteLine(DisplayShoppingCart(shoppingcart));
                System.Console.WriteLine(line);
                System.Console.WriteLine(@"gebruik WASD keys om je optie te selecteren druk daarna op Enter op je keuze te bevestigen
Druk op ESC om te vertrekken.
druk op spatie om je bestellingen to confirmeren
druk op p om je producten uit je winkelwagen te verwijderen
");
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
                {
                    selectedIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
                {
                    selectedIndex++;
                }
            } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Spacebar && keyInfo.Key != ConsoleKey.P);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                System.Console.WriteLine(" SSee you!");
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                Confirmation();
                break;
            }
            else if (keyInfo.Key == ConsoleKey.P)
            {
                shoppingcart.ModifyShoppingCart();
                break;
            }


            Console.WriteLine("je hebt dit geselecteerd: " + options[selectedIndex]);
            Console.ReadKey();
            Console.Clear();

            foreach (Snack snack in options)
            {
                if (options[selectedIndex].Name == snack.Name)
                {
                    System.Console.WriteLine(snack.Name);
                    shoppingcart.AddtoShoppingCart(snack);
                    System.Console.WriteLine("is toegevoegd aan je ShoppingCart");
                    Console.ReadKey();
                }
            }

        }

    }
    private static void Confirmation()
    {
        string choice;
        do

        {
            System.Console.WriteLine("ben je klaar met het kopen van Snacks? (ja/nee)");
            choice = Console.ReadLine()!;


        } while (choice != "ja" && choice != "nee");
        if (choice == "ja")
        {
            // zelf implenmenteren bij het mergen dit is nu een placeholder
            ChooseToAddSnackOrNot();
        }
        else if (choice == "nee")
        {
            // zelf implenmenteren bij het mergen dit is nu een placeholder
            ChooseToAddSnackOrNot();
        }
    }
}