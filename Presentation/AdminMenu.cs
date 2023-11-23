static class AdminMenu
{
    public static void Menu(Admin admin)
    {
        Console.Clear();
        List<string> options = new List<string>()
        {
            "films beheren",
            "Reserveringen beheren"
        };
        Admin adminUsed = admin;
        if (admin.AdminID == 0)
        {
            options.Add("Admins beheren");
        }

        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;


        do
        {

            Console.Clear();
            System.Console.WriteLine("Admin commands");

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
            Console.WriteLine("  LLeaving admin options!");
            Console.ReadLine();
            return;
        }

        if (options[selectedIndex] == "films beheren")
        {
            AdminsManage.FilmManagement();
            AdminMenu.Menu(admin);
        }
        if (options[selectedIndex] == "Reserveringen beheren")
        {
            Console.Clear();
            ManageReservations.ReservationsOptions();
            AdminMenu.Menu(admin);

        }
        if (options[selectedIndex] == "Admins beheren")
        {
            AdminsManage.AdminmanageMenu();
            AdminMenu.Menu(admin);

        }


    }
}