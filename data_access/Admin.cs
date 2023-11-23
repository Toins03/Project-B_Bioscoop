using Newtonsoft.Json;

class Admin : IEquatable<Admin>, IComparable<Admin>
{
    public string Name;
    public string Password;
    private int _adminID;
    public int AdminID { get => _adminID; set => _adminID = (value < 0) ? GetNewID() : value; }

    public Admin(string name, string password)
    {
        this.Name = name;
        this.Password = password;
        this.AdminID = GetNewID();
    }

    [JsonConstructor]
    public Admin(string name, string password, int adminID)
    {
        this.Name = name;
        this.Password = password;
        this.AdminID = adminID;
    }

    public override string ToString()
    {
        string ToReturn = $"Name: ({this.Name}), Password: ({this.Password}), ID: {this.AdminID}";
        return ToReturn;
    }


    // override object.Equals
    public override bool Equals(object? obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        if (GetType() != obj.GetType()) return this.Equals(obj as Admin);
        // TODO: write your implementation of Equals() here
        return base.Equals(obj);
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return base.GetHashCode();
    }

    public bool Equals(Admin? admin)
    {
        if (this is null && admin is null) return true;
        else if (admin is null || this is null) return false;
        else if (admin!.Name == this.Name && admin.AdminID == this.AdminID && admin.Password == this.Password) return true;
        else return false;
    }

    public static bool operator ==(Admin? a1, Admin a2)
    {
        if (a1 is null && a2 is null) return true;
        else if (a1 is null ^ a2 is null) return false;
        else return a1!.Equals(a2);
    }

    public static bool operator !=(Admin? a1, Admin a2)
    {
        return !(a1 == a2);
    }

    public int CompareTo(Admin? a1)
    {
        if (a1 is null) return 1;
        else if (a1.AdminID > this.AdminID) return -1;
        else if (a1.AdminID < this.AdminID) return 1;
        else return 0;
    }

    private static int GetNewID()
    {
        List<Admin> admins = AdminSave.GetAdmins();
        List<int> TakenIDs = new();
        for (int i = 0; i < admins.Count; i++)
        {
            TakenIDs.Add(admins[i].AdminID);
        }
        int NewID = TakenIDs.Max() + 1;
        return NewID;
    }

}