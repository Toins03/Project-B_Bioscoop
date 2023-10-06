
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
        Film film1 = new Film(title, duration, price, rating);
        List<Film> films = new() {film1};
        FilmSave filmsave = new(this.test_json);
        filmsave.AddToJson(film1);
        List<Film> films_read = filmsave.ReadFilms();        
        for (int i = 0; i < films.Count; i++)
        {
            Assert.AreEqual(films[i].Title, films_read[i].Title);
            Assert.AreEqual(films[i].FilmRunTime, films_read[i].FilmRunTime);
            Assert.AreEqual(films[i].FilmRating, films_read[i].FilmRating);
            Assert.AreEqual(films[i].FilmPrice, films_read[i].FilmPrice);
        }
    }

}