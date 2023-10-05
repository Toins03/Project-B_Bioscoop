
namespace data_acces.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

[TestClass]
public class FilmSaveTest
{
    private string test_json = "FilmSaveTest.json";

    [TestInitialize]
    public void remove_films()
    {
        StreamWriter writer = new StreamWriter(test_json);
        List<Film> films = new() {};
        string list_to_json = JsonConvert.SerializeObject(films, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();

    }

    [DataTestMethod]
    [DataRow("Test film", 1, 1, 1)]
    [DataRow("Test film2", 2, 3, 5)]

    public void read_films_test(string title, int duration, double price, double rating)
    {
        Film film1 = new Film("test film", 1, 1, 1);
        List<Film> films = new() {film1};
        FilmSave filmsave = new(this.test_json);
        filmsave.AddToJson(film1);
        Assert.AreEqual(films, filmsave.ReadFilms());
    }

}