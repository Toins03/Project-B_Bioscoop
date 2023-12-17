static class SortedMovies
{

    // sort on genre and date available TODO
    public static List<Film>? SortfilmByGenreQuestions(List<Film> toSort, bool isDesc = false)
    {
        List<string> genreoptions = new();
        foreach (Film film in toSort)
        {
            genreoptions.AddRange(film.Genres);
        }
        List<string> uniqueGenres = genreoptions.Distinct().ToList();
        (string? optionChosen, ConsoleKey keyLeaving) genre = BasicMenu.MenuBasic(uniqueGenres, "Different genres possible");

        if (genre.keyLeaving is ConsoleKey.Escape)
        {
            Console.WriteLine("Je gaat terug naar de ongesorteerde films.");
            return null;
        }
        else if (genre.optionChosen is null)
        {
            Console.WriteLine("Je gaat terug naar de ongesorteerde films.");
            return null;
        }


        return FilterfilmByGenre(toSort, genre.optionChosen, isDesc);
    }


    public static List<Film> FilterfilmByGenre(List<Film> toSort, string GenreSought, bool desc = false)
    {

        List<Film> toReturn = toSort.Where(q => q.Genres.Contains(GenreSought)).ToList();

        if (desc)
        {
            toReturn.Reverse();
        }

        return toReturn;
    }

    public static List<Film> SortFilmByDateAvailable(List<Film> ToSort, bool desc = false)
    {
// filter by is ever available
        List<Film> filmsAvailable = ToSort.Where(film => 
        {
            if (film.DateAndAuditorium is null) return false;
            else if (film.DateAndAuditorium.Count <= 0) return false;
            else return true;
        }).ToList();
// order by soonest available
        List<Film> sortedFilms = filmsAvailable.OrderBy(film => 
        {
            DateTime currentfirst = DateTime.MinValue;
            DateTime now = DateTime.Now;
            foreach (KeyValuePair<DateTime, string> dateavailaible in film.DateAndAuditorium)
            {
                if (dateavailaible.Key < currentfirst && dateavailaible.Key > now) currentfirst = dateavailaible.Key;
            }
            return currentfirst;
        }).ToList();
        return sortedFilms;
    }

    public static List<Film> FindFilmbyTitle(List<Film> ToSort, string Titlesearching, bool desc = false)
    {
        List<Film> toreturn = ToSort.Where(film => film.Title.Contains(Titlesearching)).ToList();
        if (desc) toreturn.Reverse();
        return toreturn;
    }

    public static List<Film> SortFilmByTitle(List<Film> Tosort, bool desc = false)
    {

        List<Film> toreturn = Tosort.OrderBy(q => q.Title).ToList();
        if (desc is true)
        {
            toreturn.Reverse();
        }

        return toreturn;
    }

    public static List<Film> SortFilmByReleaseYear(List<Film> Tosort, bool desc)
    {


        List<Film> toreturn = Tosort.OrderBy(q => q.ReleaseYear).ToList();
        if (desc is true)
        {
            toreturn.Reverse();
        }

        return toreturn;
    }


    public static void ViewSortOptions(Customer? currentcustomer, List<Film> ToView)
    {
        List<string> sortOptions = new() {
            "Een specifieke titel zoeken",
            "sorteer op titel",
            "sorteer op wanneer het eerst in de film komt",
            "sorteer op datum van uitkomst",
            "sorteer op genre"
        };

        (string? optionChosen, ConsoleKey keyLeaving) sortsChosen = BasicMenu.MenuBasic(sortOptions, "Sort Options");


        if (sortsChosen.keyLeaving is ConsoleKey.Escape)
        {
            Console.WriteLine("returning to movies unsorted");
            ChooseMovie.Films_kiezen(currentcustomer);
            return;
        }
        else if (sortsChosen.optionChosen is null)
        {
            ChooseMovie.Films_kiezen(currentcustomer);
            Console.WriteLine("returning to movies unsorted");
            return;
        }

        string sortFinal = sortsChosen.optionChosen;

        List<string> ascOrDesc = new List<string>() {
            "Ascending",
            "Descending"
        };

        (string? optionChosen, ConsoleKey keyLeaving) ascOrDescchosen = BasicMenu.MenuBasic(ascOrDesc, $"Want to sort by {sortFinal} ascending or descending?");


        if (ascOrDescchosen.keyLeaving is ConsoleKey.Escape)
        {
            Console.WriteLine("returning to movies unsorted");
            ChooseMovie.Films_kiezen(currentcustomer);
            return;
        }
        else if (ascOrDescchosen.optionChosen is null)
        {
            Console.WriteLine("returning to movies unsorted");
            ChooseMovie.Films_kiezen(currentcustomer);
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
            case "sorteer op titel":
                {
                    sortedFilm = SortFilmByTitle(ToView, chosenAscOrDesc);
                    DisplaySortedMovies(currentcustomer, sortedFilm);
                    break;
                }
            case "sorteer op datum van uitkomst":
                {
                    sortedFilm = SortFilmByReleaseYear(ToView, chosenAscOrDesc);
                    DisplaySortedMovies(currentcustomer, sortedFilm);
                    break;
                }
            case "sorteer op genre":
                {
                    sortedFilm = SortfilmByGenreQuestions(ToView, chosenAscOrDesc)!;
                    if (sortedFilm is null) return;
                    DisplaySortedMovies(currentcustomer, sortedFilm);
                    break;
                }
            case "Een specifieke titel zoeken":
                {
                    string ToSearch;
                    while (true)
                    {
                        Console.WriteLine("Schrijf de titel waarvoor u zoekt op. laat dit leeg om terug te gaan.");
                        ToSearch = Console.ReadLine()!;
                        if (ToSearch is null) DisplaySortedMovies(currentcustomer, ToView);
                        else if (ToSearch.Length == 0) DisplaySortedMovies(currentcustomer, ToView);
                        else break; 
                    }

                    sortedFilm = FindFilmbyTitle(ToView, ToSearch, chosenAscOrDesc)!;
                    if (sortedFilm is null) return;
                    DisplaySortedMovies(currentcustomer, sortedFilm);
                    break;
                }
            case "sorteer op wanneer het eerst in de film komt":
                {
                    sortedFilm = SortFilmByDateAvailable(ToView, chosenAscOrDesc)!;
                    if (sortedFilm is null) return;
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
            System.Console.WriteLine(@"gebruik WASD keys om je optie te selecteren druk daarna op Enter op je keuze te bevestigen
Druk op ESC om te vertrekken.
");
            keyInfo = Console.ReadKey();

            // update aan de controls uparrow and W gaan niet meer boven de begin optie of onder de laatste optie dit graag behouden aub
            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0 || keyInfo.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count || keyInfo.Key == ConsoleKey.DownArrow && selectedIndex < options.Count)
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
            Console.ReadKey();
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
            ChooseMovie.ConfirmMovieSelection(currentCustomer, movieTitle);
        }
    }
}