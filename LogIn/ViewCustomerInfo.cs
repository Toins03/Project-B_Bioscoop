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
                    Console.WriteLine($"stoelen gereserveerd voor deze film:\n {string.Join("\n", info.SeatsTaken)}");
                    Console.WriteLine($"Starttijd van de gereserveerde film: {info.seeTimeViewing()}");
                }
            }

            if (toView.SnacksBought is null);
            else if (toView.SnacksBought.Count >= 1)
            {
                foreach (KeyValuePair<Snack, int> snack in toView.SnacksBoughtDict())
                {
                    Console.WriteLine($"{snack.Key.Name} X {snack.Value}");
                }
            }
            Console.WriteLine("\ndruk op een willekeurige knop om terug te gaan");
            Console.ReadKey();
        }
    }
}