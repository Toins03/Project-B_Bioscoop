using Newtonsoft.Json;

public abstract class CinemaMap
{
    private int selectedRow { get; set; }
    private int selectedColumn { get; set; }
    private List<List<string>> CinemaMap1Json { get; set; } = new();
    protected List<List<string>> CinemaMap1 { get; set; } = new();
    protected List<List<string>> CinemaMapCopy { get; set; } = new();
    private string Guide { get; set; } = "Gebruik de pijltjes of 'WASD' om te navigeren. \nToets de 'spatie' om een stoel te selecteren. \nToets Enter wanneer je klaar bent met het selecteren van stoelen en wil afrekenen. \nToets 'esc' wanneer je terug wil gaan naar de vorige pagina.\n";
    private string ReservingSeats { get; set; } = "plekken die je hebt geselecteerd: ";
    private ConsoleKeyInfo keyInfo { get; set; }
    private string purpleColor { get; set; } = "\u001b[35m";
    protected string resetText { get; set; } = "\x1b[0m";
    private string FileName { get; set; } = "CinemaMaps.json";
    public List<string> ListReservedSeats { get; set; } = new();

    private string ReservedString { get; set; } = "je hebt de zitplaatsen: \n";

    protected string ChosenMovie { get; set; } = "";


    protected abstract void CreateCinemaMap();

    public void TakeSeats(string MovieTitle, Customer currentCustomer, bool IsAddmin)
    {

        ChosenMovie = MovieTitle;
        CreateCinemaMap();
        LoadCinemaMapFromJson();
        Console.Clear();
        do
        {
            System.Console.WriteLine($"Movie: {MovieTitle}");
            System.Console.WriteLine($"{Guide}");
            Console.SetCursorPosition(0, 7);
            for (int row = 0; row < CinemaMap1.Count; row++)
            {
                for (int column = 0; column < CinemaMap1[row].Count; column++)
                {

                    if (row == selectedRow && column == selectedColumn)
                    {
                        Console.Write($"\x1b[37m[POS]\x1b[0m");
                    }
                    else
                    {
                        Console.Write(CinemaMap1[row][column]);
                    }
                }
                Console.WriteLine();
            }
            PrintSelectedSeatsLegenda();
            keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                FrontPage.MainMenu(currentCustomer);
            }
            Keyboard_input(keyInfo, IsAddmin);
        }
        while (keyInfo.Key != ConsoleKey.Enter);
        if (ListReservedSeats.Count == 0) FrontPage.MainMenu(currentCustomer);
        System.Console.WriteLine($"{ReservedString} {GenerateConfirmationCode()}");
        WriteCinemaMapToJson();
        Customer.CreateCustomer(MovieTitle, GenerateConfirmationCode(), currentCustomer);
        Console.WriteLine("\n\nWil je terug naar de hoofdpagina toets 'enter' wil je stoppen toets een willekeurig knop\n");
        keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            FrontPage.MainMenu(currentCustomer);
        }
        else
            return;
    }
    public void TakeSeats(bool IsAddmin)
    {

        CreateCinemaMap();
        LoadCinemaMapFromJson();
        Console.Clear();
        do
        {
            System.Console.WriteLine($"{Guide}");
            Console.SetCursorPosition(0, 7);
            for (int row = 0; row < CinemaMap1.Count; row++)
            {
                for (int column = 0; column < CinemaMap1[row].Count; column++)
                {

                    if (row == selectedRow && column == selectedColumn)
                    {
                        Console.Write($"\x1b[37m[POS]\x1b[0m");
                    }
                    else
                    {
                        Console.Write(CinemaMap1[row][column]);
                    }
                }
                Console.WriteLine();
            }
            PrintSelectedSeatsLegenda();
            keyInfo = Console.ReadKey();
            Keyboard_input(keyInfo, IsAddmin);
        }
        while (keyInfo.Key != ConsoleKey.Enter);
        WriteCinemaMapToJson();
        Console.WriteLine("\n\nWil je terug naar de hoofdpagina toets 'enter'. \nwil je stoppen met het programma toets een willekeurig knop\n");
        keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            FrontPage.MainMenu(null!);
        }
        else
            return;
    }

    private void Keyboard_input(ConsoleKeyInfo keyInfo, bool IsAddmin)
    {
        Console.Clear();
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow or ConsoleKey.W:
                if (selectedRow > 0)
                {
                    selectedRow--;
                }
                break;
            case ConsoleKey.DownArrow or ConsoleKey.S:
                if (selectedRow < CinemaMap1.Count - 1)
                {
                    selectedRow++;
                }
                break;
            case ConsoleKey.LeftArrow or ConsoleKey.A:
                if (selectedColumn > 0)
                {
                    selectedColumn--;
                }
                break;
            case ConsoleKey.RightArrow or ConsoleKey.D:
                if (selectedColumn < CinemaMap1[0].Count - 1)
                {
                    selectedColumn++;
                }
                break;
            case ConsoleKey.Spacebar:
                if (CinemaMap1[selectedRow][selectedColumn] != "     ")
                {

                    if (CinemaMap1[selectedRow][selectedColumn] != purpleColor + "[SEL]" + resetText && CinemaMap1[selectedRow][selectedColumn] != "\x1b[31m" + "[BEZ]" + resetText)
                        SelectedSeats(selectedRow, selectedColumn);
                    else
                    {
                        if (IsAddmin)
                            DeselectSeat(selectedRow, selectedColumn, CinemaMapCopy[selectedRow][selectedColumn], true);
                        else
                            DeselectSeat(selectedRow, selectedColumn, CinemaMapCopy[selectedRow][selectedColumn]);
                    }
                }
                break;
            case ConsoleKey.Escape:
                WriteCinemaMapToJson();
                Console.ReadLine();
                break;
        }
    }

    private void LoadCinemaMapFromJson()
    {
        string jsonData;
        using (StreamReader reader = new StreamReader("MovieScheduleInformation.json"))
        {
            jsonData = reader.ReadToEnd();
        }

        List<MovieScheduleInformation> ExistingData = JsonConvert.DeserializeObject<List<MovieScheduleInformation>>(jsonData)!;
        if (ExistingData == null)
        {
            return;
        }
        foreach (MovieScheduleInformation movie in ExistingData)
        {
            if (movie.Title == ChosenMovie)
            {
                CinemaMap1Json = movie.ScreeningTimeAndAuditorium["11-11-2023"];
            }
        }

        if (CinemaMap1Json != null)
        {

            if (CinemaMap1Json.Count == CinemaMap1.Count)
            {
                if (CinemaMap1Json != CinemaMap1)
                    CinemaMap1 = CinemaMap1Json;
            }
        }
    }


    private void WriteCinemaMapToJson()
    {
        for (int column = 0; column < CinemaMap1.Count; column++)
        {
            for (int row = 0; row < CinemaMap1[column].Count; row++)
            {
                if (CinemaMap1[column][row] == purpleColor + "[SEL]" + resetText)
                    CinemaMap1[column][row] = "\x1b[31m" + "[BEZ]" + resetText;

            }
        }
        MovieScheduleInformation movieSchedule = new MovieScheduleInformation();
        movieSchedule.AddTitleAndScreeningTimeAndAuditorium(ChosenMovie, "11-11-2023", CinemaMap1, GenerateConfirmationCode());

        using (StreamWriter writer = new StreamWriter(FileName))
        {
            string List2Json = JsonConvert.SerializeObject(CinemaMap1, Formatting.Indented);
            writer.Write(List2Json);
        }
    }



    private void SelectedSeats(int row, int column)
    {
        if (!ReservingSeats.Contains($"{CinemaMap1[row][column]}") && $"{CinemaMap1[row][column]}" != purpleColor + "[SEL]" + resetText)
        {

            ListReservedSeats.Add($"{CinemaMap1[row][column]}");
            CinemaMap1[row][column] = purpleColor + "[SEL]" + resetText;

        }
    }

    private void DeselectSeat(int row, int column, string seat)
    {
        if (!CinemaMap1[row][column].Contains("[SEL]") && !CinemaMap1[row][column].Contains("[BEZ]"))
        {
            ListReservedSeats.Remove($"{CinemaMapCopy[row][column]}");
            CinemaMap1[row][column] = CinemaMapCopy[row][column];
            ReservingSeats = ReservingSeats.Replace($"{seat}", "");
        }
    }

    private void DeselectSeat(int row, int column, string seat, bool IsAdd)
    {
        if (!CinemaMap1[row][column].Contains("[SEL]"))
        {
            ListReservedSeats.Remove($"{CinemaMapCopy[row][column]}");
            CinemaMap1[row][column] = CinemaMapCopy[row][column];
            ReservingSeats = ReservingSeats.Replace($"{seat}", "");
        }
    }

    private string GetReservedSeats()
    {
        string placeholder = "";
        foreach (string seat in ListReservedSeats)
        {
            placeholder += $"{seat}";
        }
        return placeholder;
    }

    private string GetAuditoriumnumber()
    {
        string className = this.GetType().Name;
        switch (className)
        {
            case "AuditoriumMap150":
                return "1";
            case "AuditoriumMap300":
                return "2";
            case "AuditoriumMap500":
                return "3";
            default:
                return "";
        }
    }

    public string GenerateConfirmationCode()
    {
        // Get the current date and time
        DateTime currentDateTime = DateTime.Now;

        // You can also access individual components like year, month, day, hour, minute, second, etc.
        int year = currentDateTime.Year - 2000;
        int month = currentDateTime.Month;
        int day = currentDateTime.Day;
        int hour = currentDateTime.Hour;
        int minute = currentDateTime.Minute;
        int second = currentDateTime.Second;
        string ConfirmationCode = "";
        if (ListReservedSeats.Count != 0)
        {
            foreach (var seat in ListReservedSeats)
            {
                ReservedString += $"{seat} ";
            }
            ReservedString += " Gereserveerd \nBevestegingscode voor alle stoelen die je hebt gereserveerd: \n";
            ConfirmationCode += $"Z{GetAuditoriumnumber()}S{GetReservedSeats()}D{day}-{month}-{year}T{hour}:{minute}";
            ConfirmationCode = CleanUpString(ConfirmationCode);
            return ConfirmationCode;
        }
        else
            return "Je hebt geen stoelen geselecteerd";
    }

    private string CleanUpString(string input)
    {
        string cleanString = input.Replace("\x1b[37m", "");
        return cleanString;
    }
    private void PrintSelectedSeatsLegenda()
    {
        System.Console.WriteLine($"\n{ReservingSeats}");
        foreach (var seat in ListReservedSeats)
        {
            System.Console.Write(seat);
        }
        System.Console.WriteLine("\nLegenda\n");
        System.Console.WriteLine("Huidige Positie       \x1b[37m[POS]\x1b[0m");
        System.Console.WriteLine("Beschikbaar           \x1b[32m[***]\x1b[0m");
        System.Console.WriteLine("Bezet                 \x1b[31m[BEZ]\x1b[0m");
        System.Console.WriteLine("Geselecteerd          \u001b[35m[SEL]\x1b[0m");
    }
}