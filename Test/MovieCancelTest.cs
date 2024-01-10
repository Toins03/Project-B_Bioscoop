namespace Cancel.Tests;

using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


[TestClass]
public class SortMovieTest
{
    [TestInitialize]
    public void TestStart()
    { }

    [DataTestMethod]
    [DataRow()]

    public void MoviesOfCustomerTest()
    {

        //Arrange
        List<RentedMovieInfo> rentedMoviesInfoShouldbeTrue = new List<RentedMovieInfo>
        {
            new RentedMovieInfo("Example True Movie", new List<string> { "a1", "a2" },DateTime.Now.AddHours(2))
        };
        List<RentedMovieInfo> rentedMoviesInfoShouldbeFalse = new List<RentedMovieInfo>
        {
            new RentedMovieInfo("Example False Movie", new List<string> { "a1", "a2" },DateTime.Now.AddHours(1))
        };

        List<Snack> snacksReserved = new List<Snack> { };

        Customer customer = new Customer(
            ID: Customer.Counter, // Assigning the ID using the Counter property
            name: "John Doe",
            username: "john_doe",
            password: "password123",
            email: "john.doe@example.com",
            rentedMoviesInfo: rentedMoviesInfoShouldbeTrue,
            snacksReserved: snacksReserved
        );
        Customer customer1 = new Customer(
            ID: Customer.Counter, // Assigning the ID using the Counter property
            name: "John Doe",
            username: "john_doe",
            password: "password123",
            email: "john.doe@example.com",
            rentedMoviesInfo: rentedMoviesInfoShouldbeFalse,
            snacksReserved: snacksReserved
        );
        //Act
        List<string> films = MovieCancel.MoviesOfCustomer(customer);
        List<string> films1 = MovieCancel.MoviesOfCustomer(customer1);


        //Assert
        Assert.AreEqual(films.Count, 1);
        Assert.AreEqual(films1.Count, 0);




    }
}