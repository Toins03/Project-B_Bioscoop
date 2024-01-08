public class Snack: IEquatable<Snack>
{
    public string Name { get; }
    public double Price { get; }



    public Snack(string name, double price)
    {
        Name = name;
        Price = price;
    }
    
    public override bool Equals(object? obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        if (GetType() != obj.GetType()) return this.Equals(obj as Customer);
        // TODO: write your implementation of Equals() here
        return base.Equals(obj);
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return base.GetHashCode();
    }

    public bool Equals(Snack? snack)
    {
        if (this is null && snack is null) return true;
        else if (snack is null ^ this is null) return false;
        else if (snack!.Name == this.Name && snack.Price == this.Price) return true;
        else return false;
    }

    public static bool operator ==(Snack? a1, Snack? a2)
    {
        if (a1 is null && a2 is null) return true;
        else if (a1 is null || a2 is null) return false;
        else return a1.Equals(a2);
    }

    public static bool operator !=(Snack? a1, Snack? a2)
    {
        return !(a1 == a2);
    }
    public override string ToString()
    {
        return $"{this.Name}: {this.Price}";
    }
}