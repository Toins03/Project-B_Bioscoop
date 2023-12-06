// namespace data_acces.Tests;

// using System.Data;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Newtonsoft.Json;

// [TestClass]
// public class FilmSaveTest
// {
//     private string test_json = "FilmSaveTest.json";

//     [TestInitialize]
//     public void remove_films()
//     {
//         FilmSave.PathName = test_json;
//         StreamWriter writer = new StreamWriter(test_json);
//         List<Film> films = new() {};
//         string list_to_json = JsonConvert.SerializeObject(films, Formatting.Indented);
//         writer.Write(list_to_json);
//         writer.Close();

//     }

//     [DataTestMethod]
//     [DataRow("Test film", 1, 1, 1, 2001)]
//     [DataRow("Test film2", 2, 3, 5, 2001)]

//     public void read_films_test(string title, int duration, double price, double rating, int ReleaseYear)
//     {
//         Film film1 = new Film(title, duration, price, rating, ReleaseYear);
//         List<Film> films = new() {film1};
//         FilmSave.AddToJson(film1);
//         List<Film> films_read = FilmSave.ReadFilms();
//         for (int i = 0; i < films.Count; i++)
//         {
//             Assert.AreEqual(films_read[i].Title, films[i].Title);
//             Assert.AreEqual(films_read[i].Director, films[i].Director);
//             Assert.AreEqual(films_read[i].FilmRunTime, films[i].FilmRunTime);
//             Assert.AreEqual(films_read[i].ReleaseYear, films[i].ReleaseYear);
//             Assert.AreEqual(films_read[i].FilmPrice, films[i].FilmPrice);
//             Assert.AreEqual(films_read[i].FilmRating, films[i].FilmRating);
//         }
//     }

//     [DataTestMethod]
//     [DataRow("Test film", 1, 1, 1, 2001, "dr. Good", "mr. Good", "hello", "Good@gmail.com")]
//     [DataRow("Test film2", 2, 3, 5, 2001, "dr. Evil", "mr. Evil", "hello", "Evil@gmail.com")]
//     public void AddCustomerToFilmTest(string title, int duration, double price, double rating, int ReleaseYear, string name, string username, string password, string email)
//     {
//         Customer to_add = new Customer(name, username, password, email);
//         Film film = new Film(title, duration, price, rating, ReleaseYear);
//         film.AddCinemaAudience(to_add);
//         FilmSave.AddToJson(film);
//         FilmSave.AddCustomerToFilm(film, to_add);
//         List<Film> films_read = FilmSave.ReadFilms();
//         System.Console.WriteLine(film.CinemaAudience[0]);

//         Assert.AreEqual(films_read[0].Title, film.Title);
//         Assert.AreEqual(films_read[0].Director, film.Director);
//         Assert.AreEqual(films_read[0].FilmRunTime, film.FilmRunTime);
//         Assert.AreEqual(films_read[0].ReleaseYear, film.ReleaseYear);
//         Assert.AreEqual(films_read[0].FilmPrice, film.FilmPrice);
//         Assert.AreEqual(films_read[0].FilmRating, film.FilmRating);
//         Assert.AreEqual(films_read[0].CinemaAudience[0], film.CinemaAudience[0]);
//     }
// }
