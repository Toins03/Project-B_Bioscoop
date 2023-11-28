static class LogIn
{
    public static string Admin_json = "Admin_info.json";
    public static string user_json = "User_info.json";


    public static Customer LogInMenu()
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        System.Console.WriteLine(line);
        FrontPage.CreateTitleASCII();
        System.Console.WriteLine(line);
        // add in the basic frontpage items then login data

        Console.WriteLine("You have decided to log in");
        Console.WriteLine("Please enter your username.");
        string username = Console.ReadLine()!;

        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine()!;

        // check in if it is in admins else go back.
        List<Admin> admins = AdminSave.GetAdmins();
        foreach (Admin admin in admins)
        {
            if (username == admin.Name && password == admin.Password)
            {
                AdminMenu.Menu(admin);
                return null!;
            }
        }

        List<Customer> customers = Customer.LoadFromJsonFile();

        foreach (Customer customer in customers)
        {
            if (customer.UserName == username && customer.Password == password)
            {
                System.Console.WriteLine("You have successfully logged in.");
                System.Console.WriteLine($"Welcome back, {customer.UserName}");
                return customer;
            }
        }

        Console.WriteLine("The username or password are incorrect");
        Console.ReadKey();
        return null!;

    }
}