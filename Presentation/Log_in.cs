static class LogIn
{
    public static string Admin_json = "Admin_info.json";
    public static string user_json = "User_info.json";


    public static void LogInMenu()
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

        List<Admin> admins = AdminSave.GetAdmins();
        Console.WriteLine(admins);
        Console.WriteLine(admins.Count);
// check in if it is correct else go back.
        foreach (Admin admin in admins)
        {
            Console.WriteLine($"{admin.Name}, {admin.Name == username} \n");
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