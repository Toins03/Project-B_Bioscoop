using System.Linq.Expressions;

static class SortedMovies
{
    public static void NameSort(List<Film> Tosort)
    {

    }


    public static void ViewmoviesSorted(Customer currentcustomer, List<Film> ToView)
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

    }
}