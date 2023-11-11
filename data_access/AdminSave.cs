using Newtonsoft.Json;
static class AdminSave
{
    public static string PathName = "Admin_info.json";
    
    public static List<Admin> GetAdmins()
    {
        if (File.Exists(PathName))
        {
            StreamReader reader = new(PathName);
            string filefromjson = reader.ReadToEnd();
            List<Admin> admins = JsonConvert.DeserializeObject<List<Admin>>(filefromjson)!;
            reader.Close();
            if (admins is not null) return admins;
            else 
            {
                AddAdmin(new Admin(name: "super", password: "12345", adminID: 0));
                admins = new List<Admin>() {new Admin(name: "super", password: "12345", adminID: 0)};           
                Admin superadmin = new Admin(name: "super", password: "12345", adminID: 0);
                admins.Add(superadmin);
                StreamWriter writer = new(PathName);
                string list_to_json = JsonConvert.SerializeObject(admins, Formatting.Indented);
                writer.Write(list_to_json);
                writer.Close();
                return admins;
            }
        }
        else 
        {
            Console.WriteLine("Adminfile does not exist");
            List<Admin> admins = new List<Admin>() {new Admin(name: "super", password: "12345", adminID: 0)};           
            StreamWriter writer = new(PathName);
            string list_to_json = JsonConvert.SerializeObject(admins, Formatting.Indented);
            writer.Write(list_to_json);
            writer.Close();
            return new List<Admin> {};
        }
    }

    public static void AddAdmin(Admin newAdmin)
    {
        List<Admin> admins = GetAdmins();
        admins.Add(newAdmin);
        StreamWriter writer = new(PathName);
        string list_to_json = JsonConvert.SerializeObject(admins, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();
    }

    public static void AddAdmin(string name, string password)
    {
        Admin ToAdd = new Admin(name, password);
        AddAdmin(ToAdd);
    }

    public static void RemoveAdmin(int AdminID)
    {
        List<Admin> admins = GetAdmins();
        Admin ToDelete = null!;
        foreach (Admin admin in admins)
        {
            if (admin.AdminID == AdminID)
            {
                ToDelete = admin;
                break;
            }
        }

        if (ToDelete is null)
        {
            Console.WriteLine("The admin with this ID does not exist!");
            return;
        }
        
        admins.Remove(ToDelete);
    }

    public static void RemoveAdmin(string AdminName)
    {
        List<Admin> admins = GetAdmins();
        Admin ToDelete = null!;
        foreach (Admin admin in admins)
        {
            if (admin.Name == AdminName)
            {
                ToDelete = admin;
                break;
            }
        }
        
        if (ToDelete is null)
        {
            Console.WriteLine("The admin with this Name does not exist!");
            return;
        }
        else
        {
        admins.Remove(ToDelete);
        Console.WriteLine("The admin with this name has been deleted");
        }
    }

}