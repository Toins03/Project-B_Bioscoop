using Newtonsoft.Json;

class Admin
{
    public string Name;
    public string Password;
    public readonly int AdminID;
    private static int AdminCounter = 1;


    public Admin(string name, string password)
    {
        this.Name = name;
        this.Password = password;
        this.AdminID = AdminCounter;
        Admin.AdminCounter++;
    }
    [JsonConstructor]    
    public Admin(string name, string password, int adminID)
    {
        this.Name = name;
        this.Password = password;
        this.AdminID = adminID;
    }


}