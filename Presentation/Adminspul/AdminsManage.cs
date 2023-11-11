static class AdminsManage
{
    public static void AdminmanageMenu()
    {
        Console.Clear();
        List<string> options = new List<string>()
        {
            "View admins",
            "Add admins",
            "Remove admins",
        };
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;


        do 
        {

            Console.Clear();
            System.Console.WriteLine("Manage admins");

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
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }

        } while (keyInfo.Key != ConsoleKey.Enter & keyInfo.Key != ConsoleKey.Escape);

        if (keyInfo.Key == ConsoleKey.Escape)
        {
            Console.WriteLine(" Leaving admin options!");
            return;
        }

        
        if (options[selectedIndex] == "Add admins")
        {
            AddAdmin();
        }
        else if (options[selectedIndex] == "Remove admins")
        {
            RemoveAdmin();
        }
        else if (options[selectedIndex] == "View admins")
        {
            ViewAdmins();
        }

    }

    private static void AddAdmin()
    {
        System.Console.WriteLine("Please input the name of the admin. To go back to the admin manager keep this line empty.");
        string ToAddName = Console.ReadLine()!;
        if (ToAddName is null) return;
        else if (ToAddName == "") return;
        System.Console.WriteLine("Please input the password of the admin. To go back to the admin manager keep this line empty.");
        string ToAddPassWord = Console.ReadLine()!;
        if (ToAddPassWord is null) return;
        else if (ToAddPassWord == "") return;
        AdminSave.AddAdmin(ToAddName, ToAddPassWord);
    }

    private static void RemoveAdmin()
    {
        System.Console.WriteLine("Please input the name or ID of the admin. To go back to the admin manager keep this line empty.");
        string ToRemove = Console.ReadLine()!;
        if (ToRemove is null) return;
        else if (ToRemove == "") return;

    }

    private static void ViewAdmins()
    {
        List<Admin> admins = AdminSave.GetAdmins();
        foreach (Admin admin in admins)
        {
            System.Console.WriteLine($"Name: {admin.Name}, ID: {admin.AdminID}");
        }
    }
}