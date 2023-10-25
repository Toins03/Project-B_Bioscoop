class LogIn
{
    public static string Admin_json = "Admin_info.json";
    public static string user_json = "User_info.json";

    public LogIn() 
    {}

    public static void LogInMenu()
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        System.Console.WriteLine(line);
        FrontPage.CreateTitleASCII();
        System.Console.WriteLine(line);
        Console.WriteLine("You have decided to log in");
        Console.WriteLine("");
        Console.ReadKey();
        Console.WriteLine("Please enter your username.");
        string username = Console.ReadLine()!;
        Console.WriteLine("Please enter your username");
        string password = Console.ReadLine()!;
        AdminSave adminSave = new("Admin_info.json");
        List<Admin> admins = adminSave.GetAdmins();
        foreach (Admin admin in admins)
        {
            if (username == admin.Name && password == admin.Password)
            {
                AdminMenu.Menu(admin);
                return;
            }
        }
        Console.WriteLine("The username or password are incorrect");
        Console.ReadKey();
    }
}