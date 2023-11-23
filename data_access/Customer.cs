using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class Customer
{
    public int ID;
    public string Name;
    public string UserName;
    private string Password;
    public string Email;
    public string ConfirmationCode;
    public static int Counter { get; private set; } = 1;


    [JsonConstructor]
    public Customer(int ID, string name, string username, string password, string email, string confirmationcode)
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        ConfirmationCode = confirmationcode;
        this.ID = ID;
    }

    // public Customer(string name, string confirmationcode, string username = "none ", string password = "none", string email = "none")
    // {
    //     Name = name;
    //     UserName = username;
    //     Password = password;
    //     Email = email;
    //     ConfirmationCode = confirmationcode;
    //     ID = Counter;
    //     Counter++;
    // }

    public Customer(string name, string confirmationcode, string email, string username = "none ", string password = "none")
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        ConfirmationCode = confirmationcode;
        ID = Counter;
        Counter++;
    }


    // alle eigenschappen van customer word naar Customer.json geschreven
    public void SaveToJsonFile()
    {
        // List<Customer> number = LoadFromJsonFile();
        List<Customer> customers = LoadFromJsonFile() ?? new List<Customer>();
        // RAAK DIT NIET AAN AUB
        ID = customers.Count() + 1;

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
}