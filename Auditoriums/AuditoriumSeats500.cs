public class AuditoriumMap500 : CinemaMap
{
    private const string GreenText = "\x1b[32m";

    public override void CreateCinemaMap()
    {
        List<string> alphabet = new List<string>();
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            alphabet.Add(letter.ToString());
        }
        alphabet.AddRange(new List<string> { "AA", "BB", "CC", "DD" });

        string ColumnSeatNumber;
        for (int column = 20; column >= 1; column--)
        {
            if (column < 9) ColumnSeatNumber = $"{column} ";
            else ColumnSeatNumber = $"{column}";

            List<string> RowSeats = new List<string>();
            switch (column)
            {
                case 20:
                    AddEmptySpace(RowSeats, 4);
                    AddSeats(RowSeats, ColumnSeatNumber, 22, alphabet);
                    AddEmptySpace(RowSeats, 4);
                    break;
                case 1:
                    AddEmptySpace(RowSeats, 8);
                    AddSeats(RowSeats, ColumnSeatNumber, 14, alphabet);
                    AddEmptySpace(RowSeats, 8);
                    break;
                case 2:
                    AddEmptySpace(RowSeats, 7);
                    AddSeats(RowSeats, ColumnSeatNumber, 16, alphabet);
                    AddEmptySpace(RowSeats, 7);
                    break;
                case 3:
                    AddEmptySpace(RowSeats, 5);
                    AddSeats(RowSeats, ColumnSeatNumber, 20, alphabet);
                    AddEmptySpace(RowSeats, 5);
                    break;
                case int col when (col <= 5 || col >= 16):
                    AddEmptySpace(RowSeats, 3);
                    AddSeats(RowSeats, ColumnSeatNumber, 24, alphabet);
                    AddEmptySpace(RowSeats, 3);
                    break;
                case int col when (col <= 7 || col == 15):
                    AddEmptySpace(RowSeats, 2);
                    AddSeats(RowSeats, ColumnSeatNumber, 26, alphabet);
                    AddEmptySpace(RowSeats, 2);
                    break;
                case int col when (col == 8 || col == 14):
                    if (column == 14) AddEmptyAisle();
                    AddEmptySpace(RowSeats, 1);
                    for (int seat = 1; seat <= 28; seat++)
                    {
                        if (RowSeats.Count == 11 || RowSeats.Count == 20)
                            RowSeats.Add("     ");

                        string seatNumber;
                        if (column == 8 && seat > 26)
                            seatNumber = GreenText + $"[{ColumnSeatNumber.Replace(" ", "")}{alphabet[seat - 1]}]" + resetText;
                        else
                            seatNumber = GreenText + $"[{ColumnSeatNumber}{alphabet[seat - 1]}]" + resetText;
                        RowSeats.Add(seatNumber);
                    }
                    AddEmptySpace(RowSeats, 1);
                    break;
                default:
                    if (column == 9) AddEmptyAisle();
                    for (int seat = 1; seat <= 30; seat++)
                    {
                        if (RowSeats.Count == 11 || RowSeats.Count == 20)
                            RowSeats.Add("     ");
                        
                        string seatNumber;
                        if (column == 9 && seat <= 26)                     
                            seatNumber = GreenText + $"[{ColumnSeatNumber.Replace(" ", "")} {alphabet[seat - 1]}]" + resetText;                       
                        else
                          seatNumber = GreenText + $"[{ColumnSeatNumber}{alphabet[seat - 1]}]" + resetText;
                        RowSeats.Add(seatNumber);
                    }
                    break;
            }
            CinemaMap1.Add(RowSeats);
            CinemaMapCopy.Add(new List<string>(RowSeats));
        }
    }


    private  void AddEmptyAisle()
    {
        List<string> Aisle = new();
        for (int i = 0; i < 30; i++)
        {
            Aisle.Add("     ");
        }
        CinemaMap1.Add(Aisle);
        CinemaMapCopy.Add(Aisle);
    }
    private static void AddEmptySpace(List<string> RowSeats, int count)
    {
        for (int seat = 1; seat <= count; seat++)
        {
            RowSeats.Add("     ");
        }
    }

    private void AddSeats(List<string> RowSeats, string ColumnSeatNumber, int Count, List<string> alphabet)
    {
        for (int seat = 1; seat <= Count; seat++)
        {
            if (RowSeats.Count == 11 || RowSeats.Count == 20)
            {
                RowSeats.Add("     ");
            }
            string SeatNumber = GreenText + $"[{ColumnSeatNumber}{alphabet[seat - 1]}]" + resetText;
            RowSeats.Add(SeatNumber);
        }
    }
}
