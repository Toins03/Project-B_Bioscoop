
class registreren
{
    public static string customerPath { get => Customer.CustomerPath; }

    public static Customer RegistreerMenu()
    {
        BasicMenu.ShowBasics();

        List<string> ConfirmationData = new List<string>();


        Console.WriteLine("Alsjeblieft, vul hier je echte naam in. Om terug te gaan naar de hoofdpagina houd dit lijn dan leeg.");
        string RealName = Console.ReadLine()!;
        if (RealName is null) return null!;
        else if (RealName.Length == 0) return null!;
        ConfirmationData.Add($"Echte naam: {RealName}");

        // get list of usernames ready
        List<Customer> current_customers = Customer.LoadFromJsonFile();
        List<string> existingNames = new();
        foreach (Customer customer in current_customers)
        {
            existingNames.Add(customer.UserName.ToLower());
        }

        string userName;
        while (true)
        {
            Console.WriteLine("Alsjeblieft, vul hier je gebruikersnaam in. Om terug te gaan naar de hoofdpagina houd dit lijn dan leeg.");
            userName = Console.ReadLine()!;
            if (userName is null) return null!;
            else if (userName.Length == 0) return null!;
            else if (existingNames.Contains(userName.ToLower()))
            {
                Console.WriteLine("Die gebruikersnaam is al genomen! kies een nieuwe.");
            }
            else
            {
                ConfirmationData.Add($"Gebruikersnaam: {userName}");
                break;
            }
        }

        Console.WriteLine("Alsjeblieft, vul hier je wachtwoord in. Om terug te gaan naar de hoofdpagina houd dit lijn dan leeg.");
        string Password = Console.ReadLine()!;
        if (Password is null) return null!;
        else if (Password.Length == 0) return null!;
        ConfirmationData.Add($"wachtwoord: {Password}");


        Console.WriteLine("Alsjeblieft, vul hier je Email-adres in. Om terug te gaan naar de hoofdpagina houd dit lijn dan leeg.");
        string Email;
        while (true)
        {
            Email = Console.ReadLine()!;
            if (Email is null) return null!;
            else if (Email.Length == 0) return null!;
            else if (EmailParser.IsEmailValid(Email)) break;
            else System.Console.WriteLine("De ingevulde Email-adres is fout, probeer het nog is.");

        }
        ConfirmationData.Add($"Email: {Email}");



        Customer new_customer = new Customer(name: RealName, username: userName, password: Password, email: Email);

        Customer.AddCustomerToJson(new_customer);

        FrontPage.CurrentCustomer = new_customer;
        return new_customer!;
    }
}