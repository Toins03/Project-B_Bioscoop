using System.Security.Cryptography.X509Certificates;
public class Snack
{
    public string Name { get; }
    public double Price { get; }



    public Snack(string name, double price)
    {
        Name = name;
        Price = price;
    }
}