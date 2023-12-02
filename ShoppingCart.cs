class Winkelwagen
{
    public List<Snack> winkelWagen;

    public Winkelwagen()
    {
        winkelWagen = new List<Snack>();
    }
    public void AddtoWinkelWagen(Snack snack)
    {
        winkelWagen.Add(snack);
    }

    public double WinkelWagenKosten()
    {
        double Total = 0;
        foreach (Snack snack in winkelWagen)
        {
            Total += snack.Price;
        }
        return Total;
    }

}