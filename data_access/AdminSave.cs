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
            return admins;
        }
        else return new List<Admin> {};
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