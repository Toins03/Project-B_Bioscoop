namespace data_acces.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SeatPricingManagerTest
{
    private List<List<string>> auditorium150 = new();
    private List<List<string>> auditorium300 = new();
    private List<List<string>> auditorium500 = new();


    [TestInitialize]
    public void CreateAuditorium()
    {
        for (int i = 1; i <= 14; i++)
            auditorium150.Add(new List<string>());
        
        for (int i = 1; i <= 19; i++)
            auditorium300.Add(new List<string>());
        
        for (int i = 1; i <= 22; i++)
            auditorium500.Add(new List<string>());
    }
    
    [DataTestMethod]
    [DataRow(5, 5)]
    [DataRow(6, 6)]
    [DataRow(8, 6)]
    public void TestSeatsPriceMiddleAuditorium150(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium150, column, row);
        Assert.AreEqual(14.4, price);
    }

    [DataTestMethod]
    [DataRow(4, 7)]
    [DataRow(5, 8)]
    [DataRow(6, 8)]
    public void TestSeatsPriceInbetweenAuditorium150(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium150, column, row);
        Assert.AreEqual(13.2, price);
    }

    [DataTestMethod]
    [DataRow(13, 8)]
    [DataRow(1, 7)]
    [DataRow(6, 11)]
    public void TestSeatsPriceOuterAuditorium150(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium150, column, row);
        Assert.AreEqual(12, price);
    }

    [DataTestMethod]
    [DataRow(6, 8)]
    [DataRow(10, 7)]
    [DataRow(8, 12)]
    public void TestSeatsPriceMiddleAuditorium300(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium300, column, row);
        Assert.AreEqual(14.4, price);
    }

    [DataTestMethod]
    [DataRow(13, 5)]
    [DataRow(15, 9)]
    [DataRow(6, 3)]
    public void TestSeatsPriceInbetweenAuditorium300(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium300, column, row);
        Assert.AreEqual(13.2, price);
    }

    [DataTestMethod]
    [DataRow(1, 5)]
    [DataRow(12, 16)]
    [DataRow(18, 17)]
    public void TestSeatsPriceOuterAuditorium300(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium300, column, row);
        Assert.AreEqual(12, price);
    }

    [DataTestMethod]
    [DataRow(7, 12)]
    [DataRow(9, 18)]
    [DataRow(14, 14)]
    public void TestSeatsPriceMiddleAuditorium500(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium500, column, row);
        Assert.AreEqual(14.4, price);
    }

    [DataTestMethod]
    [DataRow(11, 6)]
    [DataRow(5, 19)]
    [DataRow(10, 22)]
    public void TestSeatsPriceInbetweenAuditorium500(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium500, column, row);
        Assert.AreEqual(13.2, price);
    }

    [DataTestMethod]
    [DataRow(5, 2)]
    [DataRow(18, 3)]
    [DataRow(13, 31)]
    public void TestSeatsPriceOuterAuditorium500(int row, int column)
    {
        double price = SeatPricingManager.IdentyfyAuditorium(auditorium500, column, row);
        Assert.AreEqual(12, price);
    }
}
