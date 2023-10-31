<<<<<<< Updated upstream
public class AuditoriumMap300 : CinemaMap
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

        for (int collumn = 19; collumn >= 1; collumn--)
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
            if (collumn <= 2)
            {
                int emptySeatsOnLeft = 3;
                int emptySeatsOnRight = 3;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 12; seat++)
                {
                    if (RowSeats.Count == 6 || RowSeats.Count == 13)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn <= 5)
            {
                int emptySeatsOnLeft = 2;
                int emptySeatsOnRight = 2;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 14; seat++)
                {
                    if (RowSeats.Count == 6 || RowSeats.Count == 13)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn <= 8 || collumn >= 14)
            {
                int emptySeatsOnLeft = 1;
                int emptySeatsOnRight = 1;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 16; seat++)
                {
                    if (RowSeats.Count == 6 || RowSeats.Count == 13)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else
            {

                for (int seat = 1; seat <= 18; seat++)
                {
                    if (RowSeats.Count == 6 || RowSeats.Count == 13)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

            }
            CinemaMap1.Add(RowSeats);
            CinemaMapCopy.Add(new List<string>(RowSeats));
        }

    }
=======
public class AuditoriumMap300 : CinemaMap
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

        for (int collumn = 19; collumn >= 1; collumn--)
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
            if (collumn <= 2)
            {
                int emptySeatsOnLeft = 3;
                int emptySeatsOnRight = 3;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 12; seat++)
                {
                    if (RowSeats.Count == 6 || RowSeats.Count == 13)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn <= 5)
            {
                int emptySeatsOnLeft = 2;
                int emptySeatsOnRight = 2;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 14; seat++)
                {
                    if (RowSeats.Count == 6 || RowSeats.Count == 13)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn <= 8 || collumn >= 14)
            {
                int emptySeatsOnLeft = 1;
                int emptySeatsOnRight = 1;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 16; seat++)
                {
                    if (RowSeats.Count == 6 || RowSeats.Count == 13)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else
            {

                for (int seat = 1; seat <= 18; seat++)
                {
                    if (RowSeats.Count == 6 || RowSeats.Count == 13)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{(char)('A' + seat - 1)}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

            }
            CinemaMap1.Add(RowSeats);
            CinemaMapCopy.Add(new List<string>(RowSeats));
        }

    }
>>>>>>> Stashed changes
}