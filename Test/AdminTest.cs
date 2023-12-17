namespace data_acces.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

[TestClass]
public class AdminSaveTest
{
    private string test_json = "AdminsaveTest.json";

    [TestInitialize]
    public void remove_films()
    {
        AdminSave.PathName = test_json;
        StreamWriter writer = new StreamWriter(test_json);
        List<Admin> admins = new() {};
        string list_to_json = JsonConvert.SerializeObject(admins, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();
    }

    [DataTestMethod]
    [DataRow("Test Admin", "12345")]
    [DataRow("Test Admin2", "password")]

    public void read_admins_test(string name, string password)
    {
        Admin adminNew = new Admin(name, password);
        StreamWriter writer = new StreamWriter(test_json);
        List<Admin> admins = new() {adminNew};
        string list_to_json = JsonConvert.SerializeObject(admins, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();
        List<Admin> admins_read = AdminSave.GetAdmins();
        Assert.AreEqual(1, admins_read.Count);
        Assert.IsTrue(admins_read[0] == adminNew);
        Assert.AreEqual(admins_read[0].AdminID, adminNew.AdminID);
        Assert.AreEqual(admins_read[0].Name, adminNew.Name);
        Assert.AreEqual(admins_read[0].Password, adminNew.Password);
    }

    [DataTestMethod]
    [DataRow("Test Admin", "12345", "testadmin2", "54321")]
    [DataRow("Test Admin2", "password", "testadmin2", "54321")]
    public void AddAdminList(string name, string password, string name2, string password2)
    {
        Admin to_add1 = new Admin(name, password);
        Admin to_add2 = new Admin(name2, password2);
        List<Admin> admins = new List<Admin> {to_add1, to_add2};
        AdminSave.WriteAdminList(admins);
        List<Admin> TestList = AdminSave.GetAdmins();
        
        Assert.AreEqual(admins.Count, TestList.Count);
        for (int i = 0; i < TestList.Count; i++)
        {
        Assert.IsTrue(admins[i] == TestList[i]);
        Assert.AreEqual(admins[i].AdminID, TestList[i].AdminID);
        Assert.AreEqual(admins[i].Name, TestList[i].Name);
        Assert.AreEqual(TestList[i].Password, admins[i].Password);
        }
    }

    [DataTestMethod]
    [DataRow("Test Admin", "12345")]
    [DataRow("Test Admin2", "password")]
    public void RemoveAdmins(string AdminName, string password)
    {
        Admin adminNew = new Admin(AdminName, password);
        StreamWriter writer = new StreamWriter(test_json);
        List<Admin> admins = new() {adminNew};
        string list_to_json = JsonConvert.SerializeObject(admins, Formatting.Indented);
        writer.Write(list_to_json);
        writer.Close();
        List<Admin> admins_read = AdminSave.GetAdmins();
        Assert.AreEqual(1, admins_read.Count);
        Assert.IsTrue(admins_read[0] == adminNew);
        Assert.AreEqual(admins_read[0].AdminID, adminNew.AdminID);
        Assert.AreEqual(admins_read[0].Name, adminNew.Name);
        Assert.AreEqual(admins_read[0].Password, adminNew.Password);    }
}
