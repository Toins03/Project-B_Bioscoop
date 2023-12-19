
public class AuditoriumMap150 : CinemaMap
{
    private const string GreenText = "\x1b[32m";
    public override void CreateCinemaMap()
    {
        for (int column = 14; column >= 1; column--)
        {
            string ColumnSeatNumber = (column < 10) ? $"{column} " : $"{column}";

            List<string> RowSeats = new List<string>();

            switch (column)
            {
                case int col when (col <= 2 || col == 14):
                    AddEmptySpace(RowSeats, 2);
                    AddSeats(RowSeats, ColumnSeatNumber, 8);
                    AddEmptySpace(RowSeats, 2);
                    break;
                case int col when (col == 3 || col >= 12):
                    AddEmptySpace(RowSeats, 1);
                    AddSeats(RowSeats, ColumnSeatNumber, 10);
                    AddEmptySpace(RowSeats, 1);
                    break;
                default:
                    AddSeats(RowSeats, ColumnSeatNumber, 12);
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
            string SeatNumber = GreenText + $"[{ColumnSeatNumber}{(char)('A' + seat - 1)}]" + resetText;
            RowSeats.Add(SeatNumber);
        }
    }

}