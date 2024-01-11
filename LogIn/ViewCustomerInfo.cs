class ViewCustomerInfo
{
    public static void ViewInfoMenu(Customer? toView)
    {
        CinemaInfo.PrintLogo();
        if (toView is null)
        {
            Console.WriteLine("Je bent niet ingelogd!");
            return;
        }

        Console.WriteLine($"Naam: {toView.Name}");
        Console.WriteLine($"Gebruikersnaam: {toView.UserName}");
        Console.WriteLine($"Email account: {toView.Email}");
        Console.WriteLine($"Wachtwoord: {toView.Password}");
        Console.WriteLine();
        Console.WriteLine("De door jou gereserveerde films:\n");
        Console.WriteLine(new string('=', Console.WindowWidth));

        if (toView.RentedMovieInfo is not null && toView.RentedMovieInfo.Count >= 1)
        {
            // Future reservations
            List<RentedMovieInfo> futureReservations = toView.RentedMovieInfo.Where(rentedMovie => rentedMovie.TimeViewing > DateTime.Now).OrderBy(rentedMovie => rentedMovie.TimeViewing).ToList();
            DisplayReservations(futureReservations);

            // Past reservations
            List<RentedMovieInfo> pastReservations = toView.RentedMovieInfo.Where(rentedMovie => rentedMovie.TimeViewing < DateTime.Now).OrderBy(rentedMovie => rentedMovie.TimeViewing).ToList();
            DisplayReservations(pastReservations, false);
        }

        Console.WriteLine("\nDruk op een willekeurige knop om terug te gaan");
        Console.ReadKey();
    }

    private static void DisplayReservations(List<RentedMovieInfo> reservations, bool ShowSeats = true)
    {
        foreach (var info in reservations)
        {
            Console.WriteLine($"Film gereserveerd: \n{info.FilmTitle}");

            if (ShowSeats)
            {
                Console.WriteLine($"Stoelen gereserveerd voor deze film: \n{string.Join(", ", info.SeatsTaken)}");
            }

            Console.WriteLine($"Tijd van de voorstelling: \n{info.seeTimeViewing()}");
            Console.WriteLine(info.auditoriumNumber());
            

            Console.WriteLine($"Bewijscode: {info.ConfirmationCode}\n");
            Console.WriteLine(new string('=', Console.WindowWidth));

            // if (toView.SnacksBought is null) return;
            // else if (toView.SnacksBought.Count >= 1)
            // {
            //     foreach (KeyValuePair<Snack, int> snack in toView.SnacksBoughtDict())
            //     {
            //         Console.WriteLine($"{snack.Key.Name} X {snack.Value}");
            //     }
            // }

        }
    }
}
