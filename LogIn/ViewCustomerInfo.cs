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
            if (toView.RentedMovieInfo is null);
            else if (toView.RentedMovieInfo.Count >= 1)
            {
                foreach (RentedMovieInfo info in toView.RentedMovieInfo)
                {
                    Console.WriteLine($"Film gereserveerd {info.FilmTitle}");
                    Console.WriteLine($"stoelen gereserveerd voor deze film {info.SeatsTaken}");
                    Console.WriteLine($"Tijd van deze film gereserveerd {info.TimeViewing}");
                }
            }
            Console.WriteLine("\ndruk op een willekeurige knop om terug te gaan");
            Console.ReadKey();
        }
    }
}