namespace NyVersionRestaurantASPNET.Models;

public class Vare
{
    public string Navn { get; set; }
    public double Pris { get; set; }

    public double BeregnPris(int alder)
    {
        if (alder < 12)
        {
            return Pris / 2;
        }
        else if (alder > 65)
        {
            return Pris / 0.8;
        }
        return Pris;
    }
}