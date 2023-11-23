
class registreren
{
    public static string customerPath {get => Customer.CustomerPath;}

    public static Customer RegistreerMenu()
    {
        BasicMenu.ShowBasics();

        List<string> ConfirmationData = new List<string>();


        Console.WriteLine("Please input your real name. To go back to the front Page keep this line empty.");
        string RealName = Console.ReadLine()!;
        if (RealName is null) return null!;
        else if (RealName.Length == 0) return null!;
        ConfirmationData.Add($"Real Name: {RealName}");

        Console.WriteLine("Please input your username. To go back to the front Page keep this line empty.");
        string userName = Console.ReadLine()!;
        if (userName is null) return null!;
        else if (userName.Length == 0) return null!;
        ConfirmationData.Add($"Username: {userName}");

        Console.WriteLine("Please input your Password. To go back to the front Page keep this line empty.");
        string Password = Console.ReadLine()!;
        if (Password is null) return null!;
        else if (Password.Length == 0) return null!;
        ConfirmationData.Add($"Password: {Password}");


        Console.WriteLine("Please input your Email address. To go back to the front Page keep this line empty.");
        string Email = Console.ReadLine()!;
        if (Email is null) return null!;
        else if (Email.Length == 0) return null!;
        ConfirmationData.Add($"Email: {Email}");



        Customer new_customer = new Customer(name: RealName, username: userName, password: Password, email:Email);

        return new_customer!;
    }
}