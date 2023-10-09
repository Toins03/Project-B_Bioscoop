public class AuditoriumMap500 : CinemaMap
{
    protected override void CreateCinemaMap()
    {
        List<string> alphabet = new List<string>();
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            alphabet.Add(letter.ToString());
        }
        alphabet.Add("AA");
        alphabet.Add("BB");
        alphabet.Add("CC");
        alphabet.Add("DD");

        string greenText = "\x1b[32m";
        string CollumnSeat;
        for (int collumn = 20; collumn >= 1; collumn--)
        {
            if (collumn == 9)
            {
                CollumnSeat = $"{collumn}";
            }
            else if (collumn < 9)
            {
                CollumnSeat = $"{collumn} ";
            }
            else
            {
                CollumnSeat = $"{collumn}";
            }

            List<string> RowSeats = new List<string>();
            if (collumn == 20)
            {
                int emptySeatsOnLeft = 4;
                int emptySeatsOnRight = 4;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 22; seat++)
                {
                    if (RowSeats.Count == 11 || RowSeats.Count == 20)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{alphabet[seat - 1]}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }

            else if (collumn == 1)
            {
                int emptySeatsOnLeft = 8;
                int emptySeatsOnRight = 8;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 14; seat++)
                {
                    if (RowSeats.Count == 11 || RowSeats.Count == 20)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{alphabet[seat - 1]}]" + resetText;
                    RowSeats.Add(seatNumber);
                }
                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn == 2)
            {
                int emptySeatsOnLeft = 7;
                int emptySeatsOnRight = 7;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 16; seat++)
                {
                    if (RowSeats.Count == 11 || RowSeats.Count == 20)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{alphabet[seat - 1]}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn == 3)
            {
                int emptySeatsOnLeft = 5;
                int emptySeatsOnRight = 5;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 20; seat++)
                {
                    if (RowSeats.Count == 11 || RowSeats.Count == 20)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{alphabet[seat - 1]}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn <= 5 || collumn >= 16)
            {
                int emptySeatsOnLeft = 3;
                int emptySeatsOnRight = 3;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 24; seat++)
                {
                    if (RowSeats.Count == 11 || RowSeats.Count == 20)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{alphabet[seat - 1]}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn <= 7 || collumn == 15)
            {
                int emptySeatsOnLeft = 2;
                int emptySeatsOnRight = 2;

                for (int seat = 1; seat <= emptySeatsOnLeft; seat++)
                {
                    RowSeats.Add("     ");
                }

                for (int seat = 1; seat <= 26; seat++)
                {
                    if (RowSeats.Count == 11 || RowSeats.Count == 20)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber = greenText + $"[{CollumnSeat}{alphabet[seat - 1]}]" + resetText;
                    RowSeats.Add(seatNumber);
                }

                for (int seat = 1; seat <= emptySeatsOnRight; seat++)
                {
                    RowSeats.Add("     ");
                }
            }
            else if (collumn == 8 || collumn == 14)
            {
                if (collumn == 14)
                {
                    List<string> skip = new();
                    for (int i = 0; i < 30; i++)
                    {
                        skip.Add("     ");
                    }
                    CinemaMap1.Add(skip);
                }
                RowSeats.Add("     ");

                for (int seat = 1; seat <= 28; seat++)
                {
                    if (RowSeats.Count == 11 || RowSeats.Count == 20)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber;
                    if (collumn == 8 && seat > 26)
                    {
                        seatNumber = greenText + $"[{CollumnSeat.Replace(" ", "")}{alphabet[seat - 1]}]" + resetText;
                    }
                    else
                    {
                        seatNumber = greenText + $"[{CollumnSeat}{alphabet[seat - 1]}]" + resetText;
                    }
                    RowSeats.Add(seatNumber);
                }

                RowSeats.Add("     ");
            }
            else if (collumn <= 13)
            {
                if (collumn == 9)
                {
                    List<string> skip = new();
                    for (int i = 0; i < 30; i++)
                    {
                        skip.Add("     ");
                    }
                    CinemaMap1.Add(skip);
                }
                for (int seat = 1; seat <= 30; seat++)
                {
                    if (RowSeats.Count == 11 || RowSeats.Count == 20)
                    {
                        RowSeats.Add("     ");
                    }
                    string seatNumber;
                    if (collumn == 9 && seat <= 26)
                    {
                        seatNumber = greenText + $"[{CollumnSeat.Replace(" ", "")} {alphabet[seat - 1]}]" + resetText;
                    }
                    else
                    {
                        seatNumber = greenText + $"[{CollumnSeat}{alphabet[seat - 1]}]" + resetText;
                    }
                    RowSeats.Add(seatNumber);
                }
            }

            CinemaMap1.Add(RowSeats);
            CinemaMapCopy.Add(new List<string>(RowSeats));
        }
    }
}
