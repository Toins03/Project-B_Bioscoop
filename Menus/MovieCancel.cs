public static class MovieCancel
{
    public static void CancelRegistry(Customer? currentCustomer)
    {
        if (currentCustomer is null)
        {
            Console.WriteLine("Om te annuleren, log eerst in.");
            Console.ReadKey();
            return;
        }

        List<string> movies = MoviesOfCustomer(currentCustomer);

        (string? optionChosen, ConsoleKey lastKey) chosen = BasicMenu.MenuBasic(movies, "Kies welke film u wilt annuleren");

        if (chosen.lastKey == ConsoleKey.Escape || chosen.optionChosen is null)
        {
            return;
        }

        List<RentedMovieInfo> chosenfilm = currentCustomer.RentedMovieInfo.Where(movie => movie.ToString() == chosen.optionChosen).ToList();
        if (chosenfilm.Count == 0)
        {
            Console.WriteLine("Iets ging fout");
            Console.ReadKey();
            return;
        }
        else
        {
            ManageReservations.RemoveReservation(chosenfilm[0]);

            bool successfullyremoved = currentCustomer.RentedMovieInfo.Remove(chosenfilm[0]);
            currentCustomer.SaveToJsonFile();
            if (!successfullyremoved)
            {
                Console.WriteLine("Het verwijderen is mislukt!");
                Console.ReadKey();
            }
        }
        
    }
    public static List<string> MoviesOfCustomer(Customer currentCustomer)
    {
        List<string> movies = currentCustomer.RentedMovieInfo.Where(movie =>
        {
            TimeSpan timeFromNow = movie.TimeViewing - DateTime.Now;
            return timeFromNow.Hours >= 2;
        }).Select(movie => movie.ToString()).ToList();

        return movies;
    }
    public static List<string> MoviesOfCustomer(Customer currentCustomer)
    {
        List<string> movies = currentCustomer.RentedMovieInfo.Where(movie =>
        {
            TimeSpan timeFromNow = movie.TimeViewing - DateTime.Now;
            return timeFromNow.Hours >= 2;
        }).Select(movie => movie.ToString()).ToList();

        return movies;
    }
}