static class SortedMovies
{
    public static List<Film> SortFilmByTitle(List<Film> Tosort, bool desc)
    {
        

        List<Film> toreturn = Tosort.OrderBy(q => q.Title).ToList();
        if (desc is true)
        {
            toreturn.Reverse();
        } 

        return toreturn;
    }


    public static void ViewSortOptions(Customer? currentcustomer, List<Film> ToView)
    {
        List<string> sortOptions = new() {
            "By Title"

        };
        
        (string? optionChosen, ConsoleKey keyLeaving) sortsChosen = BasicMenu.MenuBasic(sortOptions, "Sort Options");
        
        

        if (sortsChosen.keyLeaving is ConsoleKey.Escape)
        {
            Console.WriteLine("returning to movies unsorted");
            return;
        }
        else if (sortsChosen.optionChosen is null)
        {
            Console.WriteLine("returning to movies unsorted");
            return;
        }

        string sortFinal = sortsChosen.optionChosen;

        List<string> ascOrDesc = new List<string>() {
            "Ascending",
            "Descending"
        };

        (string? optionChosen, ConsoleKey keyLeaving) ascOrDescchosen = BasicMenu.MenuBasic(ascOrDesc, "Sort Options");


        if (ascOrDescchosen.keyLeaving is ConsoleKey.Escape)
        {
            Console.WriteLine("returning to movies unsorted");
            return;
        }
        else if (ascOrDescchosen.optionChosen is null)
        {
            Console.WriteLine("returning to movies unsorted");
            return;
        }

        bool chosenAscOrDesc = false;

        List<Film> sortedFilm = new();

        if (ascOrDescchosen.optionChosen == "Ascending")
        {
            chosenAscOrDesc = true;
        }

        switch (sortFinal)
        {
            case "By Title":
            {
                
                sortedFilm = SortFilmByTitle(ToView, chosenAscOrDesc);
                DisplaySortedMovies(currentcustomer, sortedFilm);
                break;
            }
            default:
            {
                Console.WriteLine("No valid sorting method found");
                return;
            }
        }
    }


    public static void DisplaySortedMovies(Customer? currentCustomer, List<Film> toDisplay)
    {
        MovieWriteAndLoad film_menu = new("Movies.json");
        List<Film> options = toDisplay;
        int selectedIndex = 0;

        ConsoleKeyInfo keyInfo;
        string line = new string('=', Console.WindowWidth);

        do
        {
            Console.Clear();
            System.Console.WriteLine(line);
            BasicMenu.CreateTitleASCII();
            System.Console.WriteLine(line);
            BasicMenu.CenterText("Film kiezen om te bekijken:\n");

            for (int i = 0; i < options.Count + 1; i++)
            {
                if (i == 0 && i == selectedIndex)
                {
                    Console.WriteLine("--> Sort Movies");
                }
                else if (i == 0 && i != selectedIndex)
                {
                    Console.WriteLine("    Sort Movies");
                }

                else if (i == selectedIndex)
                {
                    // display test van valdier
                    Console.WriteLine("--> " + options[i - 1].Title + " " + options[i - 1].ShowDate());
                }
                else
                {
                    Console.WriteLine("    " + options[i - 1].Title);
                }
            }
            System.Console.WriteLine(line);
            keyInfo = Console.ReadKey();

            // update aan de controls uparrow and W gaan niet meer boven de begin optie of onder de laatste optie dit graag behouden aub
            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0 || keyInfo.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1 || keyInfo.Key == ConsoleKey.DownArrow && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }
            else
            {

            }
        } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);

        if (keyInfo.Key == ConsoleKey.Escape)
        {
            System.Console.WriteLine("Je verlaat het film scherm!");
            return;
        }

// if they decide to sort movies
        if (selectedIndex == 0)
        {
            SortedMovies.ViewSortOptions(currentCustomer, options);
            
        }
        else
        {
            MovieWriteAndLoad.printfilmInfo(options[selectedIndex]);
            System.Console.WriteLine("Druk op Enter om stoelen te reserveren voor deze film \nDruk een ander willekeurige toets om terug te gaan naar de vorige pagina");
            string movieTitle = options[selectedIndex].Title;
            ChooseMovie.MovieConfirm(currentCustomer);
        }
    }
}