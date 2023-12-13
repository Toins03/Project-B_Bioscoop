using System.Security.Cryptography.X509Certificates;
public class Snack
{
    public string Name { get; }
    public double Price { get; }
    public static Customer currentCustomer { get; set; }
    public static string MovieTitle { get; set; }
    public static string Confirmationcode { get; set; }


    public Snack(string name, double price)
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

    // private static void FoodOptionsToList(ShoppingCart ShoppingCart)
    // {
    //     List<Snack> Food = LoadFoodOptions();
    //     LoopOverSnacks(Food, ShoppingCart);


    // }

    private static List<Snack> LoadFoodOptions()
    {
        List<Snack> Snacks =
        new()
        {
            new Snack("kinder Bueno", 1.28),
            new Snack("M&M's Pinda", 4.19),
            new Snack("Kinder bueno x8", 6.99),
            new Snack("Lays chips", 2.99),
            new Snack("Popcorn zoet", 3.59),
            new Snack("Popcorn zout", 3.59),
            new Snack("Popcorn caramel", 3.59),
            new Snack("Milka Oreo", 1.09)
    };

        return Snacks;

    }

    private static List<Snack> LoadDrinkOptions()
    {
        List<Snack> Snacks =
        new()
        {
            new Snack("Fanta 0,5L", 1.25),
            new Snack("Cola 0,5L", 1.25),
            new Snack("Pepsi 0,5L", 1.25),
            new Snack("Water 0,5L", 1.25)
        };


        return Snacks;

    }
    // private static void DrinksOptionsToList(ShoppingCart shoppingCart)
    // {
    //     List<Snack> Drinks = LoadDrinkOptions();
    //     LoopOverSnacks(Drinks, shoppingCart);

    // }


    public static void ChooseToAddSnackOrNot(string movieTitle, string conformationCode, Customer currentCustomer)
    {
        MovieTitle = movieTitle;
        Confirmationcode = conformationCode;
        currentCustomer = currentCustomer; // kan deze Warning weg?
        string choice;
        ShoppingCart shoppingCart = new ShoppingCart();
        do

        {
            System.Console.WriteLine("wil jij een snack toevoegen aan je reservering (ja/nee)");
            choice = Console.ReadLine()!;


        } while (choice != "ja" && choice != "nee");
        System.Console.WriteLine("out");

        if (choice == "ja")
        {
            // List<string> options = new List<string> { };
            // options.Add("eten");
            // options.Add("drinken");
            LoopOverSnacks(LoadDrinkOptions(), LoadFoodOptions(), shoppingCart);
            // SnackMainMenu(options, shoppingCart);
        }
        else Customer.CreateCustomer(MovieTitle, Confirmationcode, currentCustomer, shoppingCart);


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
                return;

            }
            else if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                Confirmation(shoppingcart);
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

            // if (options[selectedIndex] == "eten")
            // {
            //     FoodOptionsToList(shoppingcart);
            //     Console.ReadKey();
            // }
            // else if (options[selectedIndex] == "drinken")
            // {
            //     DrinksOptionsToList(shoppingcart);
            //     Console.ReadKey();

            // }
        }

    }
    private static void LoopOverSnacks(List<Snack> Food, List<Snack> Drinks, ShoppingCart shoppingcart)
    {
        // bool Flag = true;
        List<Snack> options = Food.Concat(Drinks).ToList();
        // while (Flag)
        // {
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
                        Console.WriteLine("    " + options[i].Name + " - " + "€" + options[i].Price);
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
                // Flag = false;
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                Confirmation(shoppingcart);
                // break;
            }
            else if (keyInfo.Key == ConsoleKey.P)
            {
                shoppingcart.ModifyShoppingCart();
                // LoopOverSnacks(Food, Drinks, shoppingcart);

            }
            else
            {




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

            // }
        }




    }
    private static void Confirmation(ShoppingCart shoppingcart)
    {
        string choice;
        do

        {
            System.Console.WriteLine("ben je klaar met het kopen van Snacks? (ja/nee)");
            choice = Console.ReadLine()!;


        } while (choice != "ja" && choice != "nee");
        if (choice == "ja")
        {
            Customer.CreateCustomer(MovieTitle, Confirmationcode, currentCustomer, shoppingcart);
        }
        else if (choice == "nee")
        {
            // zelf implenmenteren bij het mergen dit is nu een placeholder
            // Snack.ChooseToAddSnackOrNot(MovieTitle, Confirmationcode, currentCustomer);
        }
    }
}