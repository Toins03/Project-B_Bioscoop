using Newtonsoft.Json;

public class Customer : IEquatable<Customer>
{
    public static string CustomerPath = "Customer.json";
    public int ID;
    public string Name;
    public string UserName;
    public string Password { get; }
    public string Email;
    public string? ConfirmationCode;
    public static int Counter { get; private set; } = 1;

    public List<Snack> SnacksBought { get; private set; } = new();


    [JsonConstructor]
    public Customer(int ID, string name, string username, string password, string email, string? confirmationcode, List<Snack>? snacksReserved)
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        ConfirmationCode = confirmationcode;
        this.ID = ID;
        if (SnacksBought is null) this.SnacksBought = new List<Snack>();
        else this.SnacksBought = snacksReserved!;
    }

    public Customer(string name, string confirmationcode, string username = "none ", string password = "none", string email = "none")
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        ConfirmationCode = confirmationcode;
        ID = Counter;
        Counter++;
    }

    public Customer(string name, string username, string password, string email)
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        ConfirmationCode = "none";
        ID = Counter;
        Counter++;
    }

    public static void CreateCustomer(string MovieTitle, string confirmationCode, Customer currentCustomer, ShoppingCart shoppingcart)
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        Console.WriteLine(line);
        FrontPage.CreateTitleASCII();
        Console.WriteLine(line);
        List<Film> FilmsToFilter = FilmSave.ReadFilms();
        List<Film> SearchForMovie = FilmsToFilter
        .Where(films => films.Title == MovieTitle).ToList();
        if (currentCustomer != null!)
        {
            Console.WriteLine($"Ingelogd als: {currentCustomer.Name}");
            Console.WriteLine($"Email: {currentCustomer.Email}");
            Console.WriteLine($"Gebruikersnaam: {currentCustomer.UserName}");
            FilmSave.AddCustomerToFilm(MovieTitle, currentCustomer);
        }
        else
        {
            Console.WriteLine("Voer je naam in: ");
            string name = Console.ReadLine()!;
            string email;
            do
            {
                Console.WriteLine("Voer je email in: ");
                email = Console.ReadLine()!;
            } while (!email.Contains("@"));

            Console.WriteLine($"\n\nFilm: {MovieTitle}");
            Console.WriteLine($"Bevestegingscode: {confirmationCode}");
            string Snack = "Snacks: ";
            double Total = 0;
            if (shoppingcart != null)
            {
                foreach (var snack in shoppingcart.shoppingcart)
                {
                    Snack += $" {snack.Name} -";
                    Total += snack.Price;
                }

            }
            else Snack += "Geen Snacks gekocht";
            Console.WriteLine(Snack);
            double TrueTotal = Total + SearchForMovie[0].FilmPrice;
            Console.WriteLine($"Prijs {Math.Round(TrueTotal, 2)}");
            System.Console.WriteLine(SearchForMovie[0].ShowDate());
            Customer newCustomer = new Customer(name, name, email, confirmationCode);
            newCustomer.SaveToJsonFile();
            FilmSave.AddCustomerToFilm(MovieTitle, newCustomer);
        }
        Console.WriteLine("\n\nWil je terug naar de hoofdpagina?\ntoets dan een willekeurig knop in.\n");
        Console.ReadKey();
        Console.Clear();
        FrontPage.MainMenu(currentCustomer!);
    }


    // alle eigenschappen van customer word naar Customer.json geschreven
    public void SaveToJsonFile()
    {
        // List<Customer> number = LoadFromJsonFile();
        List<Customer> customers = LoadFromJsonFile() ?? new List<Customer>();
        // RAAK DIT NIET AAN AUB
        ID = customers.Count() + 1;

        customers.Add(this); // Add the current customer to the list

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