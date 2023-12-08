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

        (string? optionChosen, ConsoleKey lastKey) menuResult = BasicMenu.MenuBasic(options, "Manage admins");

        string option_chosen = menuResult.optionChosen!;
        ConsoleKey keyInfo = menuResult.lastKey;

        if (keyInfo == ConsoleKey.Escape)
        {
            Console.WriteLine(" LLeaving admin options!");
            return;
        }
        else if (option_chosen is null) 
        {
            Console.WriteLine("something went wrong");
            return;
        }

        if (option_chosen == "Add admins")
        {
            AddAdmin();
        }
        else if (option_chosen == "Remove admins")
        {
            RemoveAdmin();
        }
        else if (option_chosen == "View admins")
        {
            ViewAdmins();
        }

        AdminmanageMenu();
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
        System.Console.WriteLine("Please input the name or ID of the admin. Inputting the name is not case sensitive. To go back to the admin manager keep this line empty.");
        string ToRemove = Console.ReadLine()!;
        if (ToRemove is null)
        {
            Console.WriteLine("Invalid input");
        }
        else if (ToRemove == "") return;
        else if (int.TryParse(ToRemove, out _))
        {
            int ToRemoveInt = Convert.ToInt32(ToRemove);
            AdminSave.RemoveAdmin(ToRemoveInt);
        }
        else
        {
            AdminSave.RemoveAdmin(ToRemove);
        }

    }

    private static void ViewAdmins()
    {
        List<Admin> admins = AdminSave.GetAdmins();
        if (admins is not null)
        {
            foreach (Admin admin in admins)
            {
                System.Console.WriteLine(admin.ToString());
            }
        }
        Console.WriteLine("press any button to leave this screen.");
        Console.ReadKey();
    }
}