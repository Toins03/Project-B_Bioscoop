using Newtonsoft.Json;
class AdminSave
{
    public string PathName = "Admin_info.json";
    public AdminSave(string json_path)
    {
        this.PathName = json_path;
    }
    
    public List<Admin> GetAdmins()
    {
        if (File.Exists(this.PathName))
        {
            StreamReader reader = new(this.PathName);
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
                StreamWriter writer = new(this.PathName);
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
            StreamWriter writer = new(this.PathName);
            string list_to_json = JsonConvert.SerializeObject(admins, Formatting.Indented);
            writer.Write(list_to_json);
            writer.Close();
            return new List<Admin> {};
        }
    }

    public void AddAdmin(Admin newAdmin)
    {
        List<Admin> admins = this.GetAdmins();
        admins.Add(newAdmin);
        StreamWriter writer = new(this.PathName);
        string list_to_json = JsonConvert.SerializeObject(admins, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();
    }

}