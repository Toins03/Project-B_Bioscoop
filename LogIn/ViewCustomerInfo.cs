class ViewCustomerInfo
{
    public static void ViewInfoMenu(Customer? toView)
    {

        // Bij persoonlijk informatie moet je van toekomstige reserveringen de titel, stoelen en datum te zien.
        // Van reserveringen in het verleden hetzelfde behalve dat je de stoelen niet ziet. 
        // Dit is voor de refinement

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
            System.Console.WriteLine();
            System.Console.WriteLine("De door jou gereserveerde films:\n");
            System.Console.WriteLine(new string('=', Console.WindowWidth));


            if (toView.RentedMovieInfo is null) ;
            else if (toView.RentedMovieInfo.Count >= 1)
            {
                toView.RentedMovieInfo = toView.RentedMovieInfo.Where(rentedmovie => rentedmovie.TimeViewing > DateTime.Now).ToList();
                List<RentedMovieInfo> FilmsReservationsOfThePast = toView.RentedMovieInfo.Where(rentedmovie => rentedmovie.TimeViewing < DateTime.Now).ToList();

                if (toView.RentedMovieInfo.Count >= 1)
                {
                    foreach (RentedMovieInfo info in toView.RentedMovieInfo)
                    {
                        Console.WriteLine($"Film gereserveerd:\n {info.FilmTitle}");
                        Console.WriteLine($"stoelen gereserveerd voor deze film:\n {string.Join("\n", info.SeatsTaken)}");
                        Console.WriteLine($"Starttijd van de gereserveerde film:\n {info.seeTimeViewing()}\n");
                        System.Console.WriteLine(info.auditoriumNumber());
                        System.Console.WriteLine($"Bewijscode: {info.ConfirmationCode}\n");

                        System.Console.WriteLine(new string('=', Console.WindowWidth));

                    }
                }
                // RentedMovieInfo of the past
                // Van reserveringen in het verleden hetzelfde behalve dat je de stoelen niet ziet. 
                if (FilmsReservationsOfThePast.Count >= 1)
                {
                    foreach (RentedMovieInfo info in FilmsReservationsOfThePast)
                    {
                        Console.WriteLine($"Film gereserveerd:\n {info.FilmTitle}");
                        Console.WriteLine($"Starttijd van de gereserveerde film:\n {info.seeTimeViewing()}\n");
                        System.Console.WriteLine(new string('=', Console.WindowWidth));
                        System.Console.WriteLine(info.ConfirmationCode);


                    }

                }
            }

            // if (toView.SnacksBought is null) return;
            // else if (toView.SnacksBought.Count >= 1)
            // {
            //     foreach (KeyValuePair<Snack, int> snack in toView.SnacksBoughtDict())
            //     {
            //         Console.WriteLine($"{snack.Key.Name} X {snack.Value}");
            //     }
            // }
            Console.WriteLine("\ndruk op een willekeurige knop om terug te gaan");
            Console.ReadKey();
        }
    }
}