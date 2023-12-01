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

}