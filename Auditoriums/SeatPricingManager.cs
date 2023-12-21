public class SeatPricingManager
{
    private static double CalculateSeatPrice(string category)
    {
        double defaultPrice = 12;
        switch (category)
        {
            case "middle":
                return Math.Round(defaultPrice * 1.20, 2);
            case "inbetween":
                return Math.Round(defaultPrice * 1.10, 2);
            case "outer":
                return defaultPrice;
            default: 
                return defaultPrice;
        }
    }

    public static double IdentyfyAuditorium(List<List<string>> Auditorium, int column, int row)
    {
        // Dit kijkt welke auditorium het is door het aantal rijen in de lijst te tellen en aan de hand van de hoeveelheid rijen gebruikt het een bepaalde method 
        switch (Auditorium.Count) 
        { 
            case 14:
                return DetermineSeatCategoryForAuditorium150(Auditorium, column, row);
            case 19:
                return DetermineSeatCategoryForAuditorium300(Auditorium, column, row);
            case 22:
                return DetermineSeatCategoryForAuditorium500(Auditorium, column, row);
            default:
                return 0;
        }
    }

    private static double DetermineSeatCategoryForAuditorium150(List<List<string>> Auditorium, int column, int row)
    {
        if (row >= 0 && row < Auditorium.Count && column >= 0 && column < Auditorium[row].Count)
        {
            if ((Auditorium[row][column]) == "     ")
                return 0;
        }

        // Dit kijkt op welke rij en kolom de zitplek is in de lijs en aan de hand daarvan zegt in welke categorie de zitplek behoord
        switch (row)
        {
            case int Row when Row >= 5 && Row <= 8 && (column == 5 || column == 6):
                return CalculateSeatPrice("middle");
            case int Row when (Row == 3 || Row == 10) && (column == 5 || column == 6):
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 4 || Row == 9) && column >= 4 && column <= 7:
                return CalculateSeatPrice("inbetween");
            case int Row when Row >= 5 && Row <= 8 && column >= 3 && column <= 8:
                return CalculateSeatPrice("inbetween");
            default:
                return CalculateSeatPrice("outer");
        }      
    }
    private static double DetermineSeatCategoryForAuditorium300(List<List<string>> Auditorium, int column, int row)
    {
        if (row >= 0 && row < Auditorium.Count && column >= 0 && column < Auditorium[row].Count)
        {
            if ((Auditorium[row][column]) == "     ")
                return 0;
        }

        // Dit kijkt op welke rij en kolom de zitplek is in de lijs en aan de hand daarvan zegt in welke categorie de zitplek behoord
        switch (row)
        {
            case int Row when (Row == 5 || Row == 12) && (column == 9 || column == 10):
                return CalculateSeatPrice("middle");
            case int Row when (Row == 6 || Row == 11) && column >= 8 && column <= 11:
                return CalculateSeatPrice("middle");
            case int Row when Row >= 7 && Row <= 10 && column >= 7 && column <= 12:
                return CalculateSeatPrice("middle");
            case int Row when (Row == 14 || Row == 15 || Row == 1) && column >= 7 && column <= 12:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 13 || Row == 3 || Row == 2) && column >= 5 && column <= 14:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 12 || Row == 5 || Row == 4) && column >= 4 && column <= 15:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 11 || Row == 7 || Row == 6) && column >= 3 && column <= 16:
                return CalculateSeatPrice("inbetween");
            case int Row when Row >= 8 && Row <= 10 && column >= 2 && column <= 17:
                return CalculateSeatPrice("inbetween");
            default:
                return CalculateSeatPrice("outer");
        }
    }
    private static double DetermineSeatCategoryForAuditorium500(List<List<string>> Auditorium, int column, int row)
    {
        if (row >= 0 && row < Auditorium.Count && column >= 0 && column < Auditorium[row].Count)
        {
            if ((Auditorium[row][column]) == "     ")
                return 0;
        }

        // Dit kijkt op welke rij en kolom de zitplek is in de lijs en aan de hand daarvan zegt in welke categorie de zitplek behoord
        switch (row)
        {
            case int Row when (Row == 4 || Row == 14) && column >= 14 && column <= 17:
                return CalculateSeatPrice("middle");
            case int Row when (Row == 5) && column >= 13 && column <= 18:
                return CalculateSeatPrice("middle");
            case int Row when Row >= 7 && Row <= 13 && column >= 12 && column <= 19:
                return CalculateSeatPrice("middle");
            case int Row when (Row == 18) && column >= 13 && column <= 18:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 17) && column >= 10 && column <= 21:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 1 || Row == 16) && column >= 9 && column <= 22:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 2 || Row == 3 || Row == 15 || Row == 14) && column >= 8 && column <= 23:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 4 || Row == 5 || Row == 13) && column >= 7 && column <= 24:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 7 || Row == 8 || Row == 11) && column >= 6 && column <= 25:
                return CalculateSeatPrice("inbetween");
            case int Row when (Row == 9 || Row == 10) && column >= 5 && column <= 26:
                return CalculateSeatPrice("inbetween");
            default:
                return CalculateSeatPrice("outer");
        }
    }
}
