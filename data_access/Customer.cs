using Newtonsoft.Json;

public class Customer
{
    public static string CustomerPath = "Customer.json";
    public int ID;
    public string Name;
    public string UserName;
    public string Password {get;}
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

    public Customer(string name, string confirmationcode, string username = "none ", string password = "none", string email = "none")
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        ConfirmationCode = confirmationcode;
        ID = Counter;
        Counter++;
    }

    public Customer(string name, string username, string password, string email)
    {
        Name = name;
        UserName = username;
        Password = password;
        Email = email;
        ConfirmationCode = "none";
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
        File.WriteAllText(CustomerPath, json);
    }

    public static void AddCustomerToJson(Customer? toAdd)
    {
        if (File.Exists(CustomerPath) && toAdd is not null)
        {
            List<Customer> customers = LoadFromJsonFile();
            customers.Add(toAdd);
            StreamWriter writer = new StreamWriter(CustomerPath);
            string toJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            writer.Write(toJson);
            writer.Close();
        }
    }

    public static List<Customer> LoadFromJsonFile()
    {
        if (File.Exists(CustomerPath))
        {
            try
            {
                // Read the JSON from the file
                string json = File.ReadAllText(CustomerPath);

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
            Console.WriteLine($"File not found: {CustomerPath}");
        }

        return null!;

    }
}
