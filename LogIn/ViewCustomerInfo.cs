class ViewCustomerInfo
{
    public static void ViewInfoMenu(Customer? toView)
    {
        if (toView is null)
        {
            Console.WriteLine("Je bent niet ingelogd!");
            return;
        }
        else
        {
            Console.WriteLine($"naam: {toView.Name}");
            Console.WriteLine($"Gebruikersnaam: {toView.UserName}");
            Console.WriteLine($"Email account: {toView.Email}");
            Console.WriteLine($"Wachtwoord: {toView.Password}");
            Console.WriteLine("\ndruk op een willekeurige knop om terug te gaan");
            Console.ReadKey();
        }
    }
}