using Newtonsoft.Json;
public static class AdminSave
{
    public static string PathName = "Admin_info.json";
    
    public static List<Admin> GetAdmins()
    {
        try
        {
            if (File.Exists(PathName))
            {
                StreamReader reader = new(PathName);
                string filefromjson = reader.ReadToEnd();
                List<Admin> admins = JsonConvert.DeserializeObject<List<Admin>>(filefromjson)!;
                reader.Close();
                if (admins is null) 
                {
                    List<Admin> to_make = new() {new Admin(name: "super", password: "12345", adminID: 1)};
                    WriteAdminList(to_make);
                    return to_make;
                }
                else 
                {
                    return admins;
                }
            }
            else 
            {
                Console.WriteLine("Adminfile does not exist");
                List<Admin> admins = new List<Admin>() {new Admin(name: "super", password: "12345", adminID: 0)};           
                WriteAdminList(admins);
                return admins;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
            return new();
        }
    }

    public static void AddAdmin(Admin newAdmin)
    {
        List<Admin> admins = GetAdmins();
        admins.Add(newAdmin);
        WriteAdminList(admins);
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
        
        else
        {
            admins.Remove(ToDelete);
            WriteAdminList(admins);
            Console.WriteLine("The admin with this ID has been deleted");
        }
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
            WriteAdminList(admins);
            Console.WriteLine("The admin with this name has been deleted");
        }
    }

    public static void WriteAdminList(List<Admin> ToWrite)
    {
        StreamWriter writer = new(PathName);
        string list_to_json = JsonConvert.SerializeObject(ToWrite, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();       
    }

}