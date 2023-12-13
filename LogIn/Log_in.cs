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

        Console.WriteLine("je gaat nu inloggen");
        Console.WriteLine("vul hier je gebruikersnaam in");
        string username = Console.ReadLine()!;

        Console.WriteLine("vul hier je wachtwoord in");
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
                System.Console.WriteLine("je bent met succes ingelogd");
                System.Console.WriteLine($"Welkom terug, {customer.UserName}");
                return customer;
            }
        }

        Console.WriteLine("je gebruikersnaam of wachtwoord is fout");
        Console.ReadKey();
        return null!;

    }
}