public class AuditoriumMap150 : CinemaMap
{
    protected override void CreateCinemaMap()
    {
        List<char> alphabet = new List<char>();
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            alphabet.Add(letter);
        }

        string greenText = "\x1b[32m";
        string CollumnSeat;

        for (int collumn = 14; collumn >= 1; collumn--)
        {
            if (collumn < 10)
            {
                CollumnSeat = $"{collumn} ";
            }
            else
            {
                CollumnSeat = $"{collumn}";
            }

            List<string> RowSeats = new List<string>();
            if (collumn <= 2 || collumn == 14)
            {
                int emptySeatsOnLeft = 2;
                int emptySeatsOnRight = 2;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 8; seat++)
                {
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }

            }
            else if (collumn == 3 || collumn >= 12)
            {
                RowSeats.Add("     ");

                for (int seat = 1; seat <= 10; seat++)
                {
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }
                RowSeats.Add("     ");
            }
            else
            {
                for (int seat = 1; seat <= 12; seat++)
                {
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }
            }
            CinemaMap1.Add(RowSeats);
            CinemaMapCopy.Add(new List<string>(RowSeats));
        }
    }
}