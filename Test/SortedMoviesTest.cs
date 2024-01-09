namespace data_acces.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;


[TestClass]
public class SortMovieTest
{
    [TestInitialize]
    public void TestStart()
    { }

    [DataTestMethod]
    [DataRow()]
    public void FilterfilmByGenreTest()
    {
        //Arrange
        List<Film> films = new List<Film>
        {
            new Film("Apple", 120, 9.99, 4.5, 2022, new List<string> { "Drama", "Comedy" }),
            new Film("Banana", 105, 8.99, 4.0, 2023, new List<string> { "Adventure", "Family" }),
            new Film("Cucumber", 95, 7.99, 3.8, 2024, new List<string> { "Thriller", "Mystery" })
        };
        //Act
        List<Film> FilteredMovies = SortedMovies.FilterfilmByGenre(films, "Drama");

        //Assert
        Assert.AreEqual(FilteredMovies[0].Title, "Apple");



    }

    public void SortFilmByDateAvailableTest()
    {
        //Arrange
        /*string title,
    int runtime,
    double price,
    double filmrating,
    int ReleaseYear,
    string director,
    List<string> genres,
    List<string> cinemaAudience = null!,
    Dictionary<DateTime, string> DateAndAuditorium = null!)*/
        List<Film> films = new List<Film>
        {
            new Film("Apple", 120, 9.99, 4.5, 2022, "Hancock", new List<string> { "Adventure", "Family" }, new List<string> { "audience1" }, new Dictionary<DateTime, string> { {  DateTime.Today.AddDays(2), "2" } }),
            new Film("Banana", 120, 9.99, 4.5, 2022, "Hancock", new List<string> { "Adventure", "Family" }, new List<string> { "audience1" }, new Dictionary<DateTime, string> { {  DateTime.Today.AddDays(1), "1" } }),
            new Film("Cucumber", 120, 9.99, 4.5, 2022, "Hancock", new List<string> { "Adventure", "Family" }, new List<string> { "audience1" }, new Dictionary<DateTime, string> { {  DateTime.Today.AddDays(3), "3" } }),
        };
        //Act
        List<Film> FilteredMoves = SortedMovies.SortFilmByDateAvailable(films);
        //Assert
        Assert.AreEqual(FilteredMoves[0].Title, "Banana");
        Assert.AreEqual(FilteredMoves[1].Title, "Apple");
        Assert.AreEqual(FilteredMoves[2].Title, "Cucumber");


    }

    public void FindFilmbyTitleTest()
    {
        //Arrange
        List<Film> films = new List<Film>
        {
            new Film("Apple", 120, 9.99, 4.5, 2022, new List<string> { "Drama", "Comedy" }),
            new Film("Banana", 105, 8.99, 4.0, 2023, new List<string> { "Adventure", "Family" }),
            new Film("Cucumber", 95, 7.99, 3.8, 2024, new List<string> { "Thriller", "Mystery" })
        };
        //Act
        List<Film> Filteredfilms = SortedMovies.FindFilmbyTitle(films, "Cucumber");
        //Assert
        Assert.AreEqual(Filteredfilms[0], "Cucumber");
    }

    public void SortFilmByTitleTest()
    {
        //Arrange
        List<Film> films = new List<Film>
        {
            new Film("Apple", 120, 9.99, 4.5, 2022, new List<string> { "Drama", "Comedy" }),
            new Film("Banana", 105, 8.99, 4.0, 2023, new List<string> { "Adventure", "Family" }),
            new Film("Cucumber", 95, 7.99, 3.8, 2024, new List<string> { "Thriller", "Mystery" })
        };
        //Act
        List<Film> Filteredfilms = SortedMovies.SortFilmByTitle(films, true);

        //Assert
        Assert.AreEqual(Filteredfilms[2].Title, "Cucumber");
        Assert.AreEqual(Filteredfilms[1].Title, "Banana");
        Assert.AreEqual(Filteredfilms[0].Title, "Apple");


    }

    public void SortFilmByReleaseYearTest()

    {
        //Arrange
        List<Film> films = new List<Film>
        {
            new Film("Apple", 120, 9.99, 4.5, 2002, new List<string> { "Drama", "Comedy" }),
            new Film("Banana", 105, 8.99, 4.0, 2000, new List<string> { "Adventure", "Family" }),
            new Film("Cucumber", 95, 7.99, 3.8, 2024, new List<string> { "Thriller", "Mystery" })
        };
        //Act
        List<Film> SortedFilms = SortedMovies.SortFilmByReleaseYear(films, false);
        //Assert
        Assert.AreEqual(SortedFilms[0].Title, "Banana");
        Assert.AreEqual(SortedFilms[1].Title, "Apple");
        Assert.AreEqual(SortedFilms[2].Title, "Cucumber");

    }
}
