using System.Runtime.CompilerServices;
using Newtonsoft.Json;

public class Customer : IEquatable<Customer>
{
    public static string CustomerPath = "Customer.json";
    public int ID;
    public string Name;
    public string UserName;
    public string Password { get; }
    public string Email;
    public List<RentedMovieInfo> RentedMovieInfo = new();
    public static int Counter { get => Customer.LoadFromJsonFile().Count + 1; }

    public List<Snack> SnacksBought { get; private set; } = new();

    public Dictionary<Snack, int> SnacksBoughtDict() => ListToDict(SnacksBought);


    [JsonConstructor]
    public Customer(int ID, string name, string username, string password, string email, List<RentedMovieInfo> rentedMoviesInfo)
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        this.RentedMovieInfo = rentedMoviesInfo;
        this.ID = ID;
    }

    public Customer(string name, string username, string password, string email)
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        this.RentedMovieInfo = new();
        ID = Counter;
        this.SnacksBought = new();
    }

    public static void CreateCustomer(RentedMovieInfo rentedMovie, Customer? currentCustomer, ShoppingCart shoppingcart, double TotalSeatCost)
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        Console.WriteLine(line);
        FrontPage.CreateTitleASCII();
        Console.WriteLine(line);
        if (currentCustomer is not null)
        {
            Console.WriteLine($"Ingelogd als: {currentCustomer.Name}");
            Console.WriteLine($"Email: {currentCustomer.Email}");
            Console.WriteLine($"Gebruikersnaam: {currentCustomer.UserName}");
            Console.WriteLine(rentedMovie.ToString());


            System.Console.WriteLine("Gekochte Snacks:");
            Dictionary<Snack, int> ShoppingCartDict = ListToDict(shoppingcart.shoppingcart);
            foreach (KeyValuePair<Snack, int> snack in ShoppingCartDict)
            {
                Console.WriteLine($"{snack.Key.Name} X {snack.Value}");
            }


            List<Film> Films = Film.LoadFilmFromJsonFile();
            List<Film> FilmPrices = Films.Where(film => film.Title == rentedMovie.FilmTitle).ToList();
            if (FilmPrices.Count == 1)
            {
                // dit is nog niet helemaal goed want dit moet ook code hebben om te calculeren hoeveel een bepaalde zitplek kost
                // oplossing voor de bug dat het met meer dan 2 decimalen achter de komma afrond :D
                double RoundedNumber = shoppingcart.ShoppingCartCosts() + TotalSeatCost;
                System.Console.WriteLine($"\n je totale kosten zijn € {Math.Round(RoundedNumber, 2)}");
            }


            rentedMovie.SnacksBought = ListToDict(shoppingcart.shoppingcart);
            currentCustomer.RentedMovieInfo.Add(rentedMovie);

            FilmSave.AddCustomerToFilm(rentedMovie.FilmTitle, currentCustomer);
            currentCustomer.SaveToJsonFile();
        }
        else
        {
            Customer customerNow = Customer.LoginOrRegisterCustomer();
            if (customerNow is null) return;
            // first check if you want to log in or register


            if (customerNow.SnacksBought is null) customerNow.SnacksBought = new();

            rentedMovie.SnacksBought = ListToDict(shoppingcart.shoppingcart);
            customerNow.RentedMovieInfo.Add(rentedMovie);

            customerNow.SaveToJsonFile();
            FilmSave.AddCustomerToFilm(rentedMovie.FilmTitle, customerNow);

        }
        Console.WriteLine("\n\nWil je terug naar de hoofdpagina?\ntoets dan een willekeurig knop in.\n");
        Console.ReadKey();
        Console.Clear();
        return;
    }


    public static Customer LoginOrRegisterCustomer()
    {
        Customer currentCustomer = null!;
        List<string> options = new List<string>() { "Registreren", "Inloggen" };
        while (currentCustomer is null)
        {
            string Uitleg = "Om door te gaan moet u registreren of uitloggen. Welke optie kiest u?";

            (string? optionChosen, ConsoleKey KeyLeaving) registerOrLogIn = BasicMenu.MenuBasic(options: options, MenuName: Uitleg);

            if (registerOrLogIn.KeyLeaving == ConsoleKey.Escape ^ registerOrLogIn.optionChosen is null)
            {
                Console.WriteLine("om te vertrekken druk nog eens op ESC. Om opnieuw te kiezen om in te loggen druk op een willekeurige andere knop.");
                ConsoleKeyInfo consoleKey = Console.ReadKey();
                if (consoleKey.Key == ConsoleKey.Escape) return null!;
            }
            else if (registerOrLogIn.optionChosen == "Registreren")
            {
                currentCustomer = registreren.RegistreerMenu();
            }
            else if (registerOrLogIn.optionChosen == "Inloggen")
            {
                currentCustomer = LogIn.LogInCustomer();
            }
        }
        return currentCustomer;
    }


    // alle eigenschappen van customer word naar Customer.json geschreven
    public void SaveToJsonFile()
    {
        // List<Customer> number = LoadFromJsonFile();
        List<Customer> customers = LoadFromJsonFile() ?? new List<Customer>();
        // RAAK DIT NIET AAN AUB

        bool IsInCustomers = false;
        for (int i = 0; i < customers.Count; i++)
        {
            if (this == customers[i])
            {
                IsInCustomers = true;
                customers[i] = this;
                break;
            }
        }
        if (!IsInCustomers) customers.Add(this);

        // Serialize the list of customers to JSON
        string json = JsonConvert.SerializeObject(customers, Formatting.Indented);

        // Write the JSON to the file
        File.WriteAllText(CustomerPath, json);
    }

    public static void AddCustomerToJson(Customer? toAdd)
    {
        if (File.Exists(CustomerPath) && toAdd is not null)
        {
            List<Customer> customers = LoadFromJsonFile();
            customers.Add(toAdd);
            StreamWriter writer = new StreamWriter(CustomerPath);
            Console.WriteLine("jij bent toegevoegd");
            string toJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            writer.Write(toJson);
            writer.Close();
        }
    }

    public static List<Customer> LoadFromJsonFile()
    {
        if (File.Exists(CustomerPath))
        {
            try
            {
                // Read the JSON from the file
                string json = File.ReadAllText(CustomerPath);

                // Deserialize the JSON into a list of Customer objects
                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(json)!;

                return customers;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., JSON parsing errors)
                Console.WriteLine($"Error loading customer data: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File not found: {CustomerPath}");
        }

        return null!;
    }

    public void AddSnack(List<Snack>? snacks)
    {
        if (snacks is null) return;
        this.SnacksBought.AddRange(snacks);

    }

    public void AddSnack(Snack? snack)
    {
        if (snack is null) return;

        this.SnacksBought.Add(snack);

    }

    private static Dictionary<T, int> ListToDict<T>(List<T> toConvert) where T : IEquatable<T>
    {
        Dictionary<T, int> toReturn = new();
        foreach (T item in toConvert)
        {
            bool itemfound = false;
            foreach (T tofind in toReturn.Keys)
            {
                if (tofind.Equals(item))
                {
                    toReturn[tofind] += 1;
                    itemfound = true;
                    break;
                }
            }
            if (!itemfound) toReturn[item] = 1;
        }
        return toReturn;
    }


    public override bool Equals(object? obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        if (GetType() != obj.GetType()) return this.Equals(obj as Customer);
        // TODO: write your implementation of Equals() here
        return base.Equals(obj);
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return base.GetHashCode();
    }

    public bool Equals(Customer? customer)
    {
        if (this is null && customer is null) return true;
        else if (customer is null ^ this is null) return false;
        else if (customer!.Name == this.Name && customer.UserName == this.UserName && customer.Password == this.Password && customer.Email == this.Email) return true;
        else return false;
    }

    public static bool operator ==(Customer? a1, Customer a2)
    {
        if (a1 is null && a2 is null) return true;
        else if (a1 is null || a2 is null) return false;
        else return a1.Equals(a2);
    }

    public static bool operator !=(Customer? a1, Customer a2)
    {
        return !(a1 == a2);
    }

}
