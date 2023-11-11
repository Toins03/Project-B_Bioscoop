using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Customer
{
    public int ID;
    public string Name;
    public string UserName;
    public string Password;
    public string Email;
    public static int Counter { get; private set; } = 1;


    [JsonConstructor]
    public Customer(int ID, string name, string username, string password, string email)
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        this.ID = ID;
    }

    public Customer(string name, string username = "none ", string password = "none", string email = "none")
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        ID = Counter;
        Counter++;
    }



    public void SaveToJsonFile()
    {
        // List<Customer> number = LoadFromJsonFile();
        List<Customer> customers = LoadFromJsonFile() ?? new List<Customer>();
        // ID = customers.Count() + 1;

        customers.Add(this); // Add the current customer to the list

        // Serialize the list of customers to JSON
        string json = JsonConvert.SerializeObject(customers);

        // Write the JSON to the file
        File.WriteAllText("Customer.json", json);
    }

    public static List<Customer> LoadFromJsonFile()
    {
        if (File.Exists("Customer.json"))
        {
            try
            {
                // Read the JSON from the file
                string json = File.ReadAllText("Customer.json");

                // Deserialize the JSON into a list of Customer objects
                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(json)!;

                return customers;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., JSON parsing errors)
                Console.WriteLine($"Error loading customer data: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File not found: {"Customer.json"}");
        }

        return null!;

    }


    public override bool Equals(object? obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        if (GetType() != obj.GetType()) return this.Equals(obj as Customer);
        // TODO: write your implementation of Equals() here
        return base.Equals (obj);
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return base.GetHashCode();
    }

    public bool Equals(Customer? Customer)
    {
        if (this is null && Customer is null) return true;
        else if (Customer is null ^ this is null) return false;
        else if (Customer!.Name == this.Name && Customer.ID == this.ID && Customer.UserName == this.UserName && Customer.Password == this.Password) return true;
        else return false;
    }

    public static bool operator ==(Customer? a1, Customer a2)
    {
        if (a1 is null && a2 is null) return true;
        else if (a1 is null || a2 is null) return false;
        else return a1.Equals(a2);
    }

    public static bool operator !=(Customer? a1, Customer a2)
    {
        return !(a1 == a2);
    }

}