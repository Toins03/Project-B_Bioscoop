public static class CinemaInfo
{
    public static void PrintCinemaInfo()
    {
        CinemaInfo.PrintLogo();
        CinemaInfo.AddressAndContact();
        CinemaInfo.OpeningClosingTime();
        CinemaInfo.Availability();
        CinemaInfo.AboutTheCinema();
        Console.WriteLine("Druk een knop om terug te gaan naar de hoofdpagina.");
        Console.ReadKey();
    }

    public static void PrintLogo()
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        Console.WriteLine(line);
        FrontPage.CreateTitleASCII();
        Console.WriteLine(line);
    }
    public static void OpeningClosingTime()
    {
        Console.WriteLine("\nOpeningstijden");
        Console.WriteLine("Maandag: 8:30 - 1:30");
        Console.WriteLine("Dinsdag: 8:30 - 1:30");
        Console.WriteLine("Woensdag: 8:30 - 1:30");
        Console.WriteLine("Donderdag: 8:30 - 1:30");
        Console.WriteLine("Vrijdag: 8:30 - 1:30");
        Console.WriteLine("Zaterdag: 8:30 - 1:30");
        Console.WriteLine("Zondag: 9:30 - 23:30");
    }

    public static void AddressAndContact()
    {
        Console.WriteLine("Adres & contact");
        Console.WriteLine("Adres: Wijnhaven 107, 3011 WN in Rotterdam");
        Console.WriteLine("Contact: TheMandem@gmail.com");
    }

    public static void AboutTheCinema()
    {
        Console.WriteLine("\nAlgemene informatie");
        Console.WriteLine("Stoelen: 950");
        Console.WriteLine("Grootste zaal: zaal 3 (500)");
        Console.WriteLine("Zalen: 3");
        Console.WriteLine("Geluidssysteem: Dolby Digital 7.1");
        Console.WriteLine("Rolstoelmogelijkheden: Alle zalen zijn rolstoeltoegankelijk en gelijkvloers (via lift) te bereiken. Mindervalide toilet aanwezig.");
    }

    public static void Availability()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("\nBereikbaarheid");
        Console.WriteLine("Auto");
        Console.WriteLine("-------");
        Console.WriteLine("Parkeergarage ParkBee");
        Console.WriteLine("Adres: Wijnstraat 100");
        Console.WriteLine("Tarief: €4,70\n");
        Console.WriteLine("Parkeergarage ParkBee");
        Console.WriteLine("Adres: Wijnstraat 78");
        Console.WriteLine("Tarief: €5,40");
        Console.WriteLine("\nOV");
        Console.WriteLine("-------");
        Console.WriteLine("Bij Beurs uitstappen");
    }
}
