using Newtonsoft.Json;

public abstract class CinemaMap
{
    private static int selectedRow = 0;
    private static int selectedColumn = 0;
    private static List<List<string>> CinemaMap1Json = new();
    protected static List<List<string>> CinemaMap1 = new();
    protected static List<List<string>> CinemaMapCopy = new();
    private static string Guide = "Gebruik de pijltjes of 'WASD' om te navigeren. \nToets de 'spatie' om een stoel te selecteren. \nToets Enter wanneer je klaar bent met het selecteren van stoelen en wil afrekenen. \nToets 'esc' wanneer je terug wil gaan naar de vorige pagina.\n";
    private static string ReservingSeats = "plekken die je hebt geselecteerd: ";
    private static ConsoleKeyInfo keyInfo;
    private static string purpleColor = "\u001b[35m";
    protected static string resetText = "\x1b[0m";
    private static string FileName = "CinemaMaps.json";
    public static List<string> ListReservedSeats = new();

    protected abstract void CreateCinemaMap();

    public void TakeSeats()
    {
        CreateCinemaMap();
        LoadCinemaMapFromJson();
        do
        {
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
            Keyboard_input(keyInfo);
        }
        while (keyInfo.Key != ConsoleKey.Enter);
        Console.WriteLine($"je hebt de zitplaatsen: ");
        foreach (var seat in ListReservedSeats)
        {
            Console.Write(seat);
        }
        System.Console.WriteLine("Gereserveerd");

        WriteCinemaMapToJson();
    }

    private static void Keyboard_input(ConsoleKeyInfo keyInfo)
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

    private static void LoadCinemaMapFromJson()
    {
        string jsonData;
        using (StreamReader reader = new StreamReader(FileName))
        {
            jsonData = reader.ReadToEnd();
        }

        CinemaMap1Json = JsonConvert.DeserializeObject<List<List<string>>>(jsonData)!;
        if (CinemaMap1Json != null)
        {
            if (CinemaMap1Json.Count == CinemaMap1.Count)
            {
                if (CinemaMap1Json != CinemaMap1)
                    CinemaMap1 = CinemaMap1Json;
            }
        }
    }


    private static void WriteCinemaMapToJson()
    {
        for (int column = 0; column < CinemaMap1.Count; column++)
        {
            for (int row = 0; row < CinemaMap1[column].Count; row++)
            {
                if (CinemaMap1[column][row] == purpleColor + "[SEL]" + resetText)
                {
                    CinemaMap1[column][row] = "\x1b[31m" + "[BEZ]" + resetText;
                }
            }
        }
        using (StreamWriter writer = new StreamWriter(FileName))
        {
            string List2Json = JsonConvert.SerializeObject(CinemaMap1, Formatting.Indented);
            writer.Write(List2Json);
        }
    }

    private static void SelectedSeats(int row, int column)
    {
        if (!ReservingSeats.Contains($"{CinemaMap1[row][column]}") && $"{CinemaMap1[row][column]}" != purpleColor + "[SEL]" + resetText)
        {

            ListReservedSeats.Add($"{CinemaMap1[row][column]}");
            CinemaMap1[row][column] = purpleColor + "[SEL]" + resetText;

        }
    }

    private static void DeselectSeat(int row, int column, string seat)
    {
        if (!CinemaMap1[row][column].Contains("[SEL]") || !CinemaMap1[row][column].Contains("[BEZ]"))
        {
            ListReservedSeats.Remove($"{CinemaMapCopy[row][column]}");
            CinemaMap1[row][column] = CinemaMapCopy[row][column];
            ReservingSeats = ReservingSeats.Replace($"{seat}", "");
        }
    }

    private static void PrintSelectedSeatsLegenda()
    {
        Console.SetCursorPosition(0, 0);
        System.Console.WriteLine($"{Guide}");
        Console.SetCursorPosition(0, CinemaMap1.Count + 10);
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
