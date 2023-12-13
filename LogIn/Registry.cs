
class registreren
{
    public static string customerPath { get => Customer.CustomerPath; }

    public static Customer RegistreerMenu()
    {
        BasicMenu.ShowBasics();

        List<string> ConfirmationData = new List<string>();


        Console.WriteLine("Please input your real name. To go back to the front Page keep this line empty.");
        string RealName = Console.ReadLine()!;
        if (RealName is null) return null!;
        else if (RealName.Length == 0) return null!;
        ConfirmationData.Add($"Real Name: {RealName}");

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
            Console.WriteLine("Please input your username. To go back to the front Page keep this line empty.");
            userName = Console.ReadLine()!;
            if (userName is null) return null!;
            else if (userName.Length == 0) return null!;
            else if (existingNames.Contains(userName.ToLower()))
            {
                Console.WriteLine("Die gebruikersnaam is al genomen! kies een nieuwe.");
            }
            else
            {
                ConfirmationData.Add($"UserName: {userName}");
                break;
            }
        }

        Console.WriteLine("Please input your Password. To go back to the front Page keep this line empty.");
        string Password = Console.ReadLine()!;
        if (Password is null) return null!;
        else if (Password.Length == 0) return null!;
        ConfirmationData.Add($"Password: {Password}");


        Console.WriteLine("Please input your Email address. To go back to the front Page keep this line empty.");
        string Email;
        while (true)
        {
            Email = Console.ReadLine()!;
            if (Email is null) return null!;
            else if (Email.Length == 0) return null!;
            else if (EmailParser.IsEmailValid(Email)) break;
            else System.Console.WriteLine("This was not a valid email addres! Please try again.");

        }
        ConfirmationData.Add($"Email: {Email}");



        Customer new_customer = new Customer(name: RealName, username: userName, password: Password, email: Email);

        Customer.AddCustomerToJson(new_customer);

        return new_customer!;
    }
}