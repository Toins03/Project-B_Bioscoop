public class AuditoriumMap300 : CinemaMap
{
    private const string GreenText = "\x1b[32m";

    public override void CreateCinemaMap()
    {
        for (int column = 19; column >= 1; column--)
        {
            string ColumnSeatNumber = (column < 10) ? $"{column} " : $"{column}";

            List<string> RowSeats = new List<string>();
            switch (column)
            {
                case int col when col <= 2:
                    AddEmptySpace(RowSeats, 3);
                    AddSeats(RowSeats, ColumnSeatNumber, 12);
                    AddEmptySpace(RowSeats, 3);
                    break;
                case int col when col <= 5:
                    AddEmptySpace(RowSeats, 2);
                    AddSeats(RowSeats, ColumnSeatNumber, 14);
                    AddEmptySpace(RowSeats, 2);
                    break;
                case int col when col <= 8 || col >= 14:
                    AddEmptySpace(RowSeats, 1);
                    AddSeats(RowSeats, ColumnSeatNumber, 16);
                    AddEmptySpace(RowSeats, 1);
                    break;
                default:
                    AddSeats(RowSeats, ColumnSeatNumber, 18);
                    break;
            }
            CinemaMap1.Add(RowSeats);
            CinemaMapCopy.Add(new List<string>(RowSeats));
        }
    }

    private void AddEmptySpace(List<string> RowSeats, int count)
    {
        for (int seat = 1; seat <= count; seat++)
        {
            RowSeats.Add("     ");
        }
    }

    private void AddSeats(List<string> RowSeats, string ColumnSeatNumber, int Count)
    {
        for (int seat = 1; seat <= Count; seat++)
        {
            if (RowSeats.Count == 6 || RowSeats.Count == 13)
            {
                RowSeats.Add("     ");
            }
            string SeatNumber = GreenText + $"[{ColumnSeatNumber}{(char)('A' + seat - 1)}]" + resetText;
            RowSeats.Add(SeatNumber);
        }
    }
}