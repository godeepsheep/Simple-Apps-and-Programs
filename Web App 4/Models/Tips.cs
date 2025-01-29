namespace Web_App_4.Models;

public class Tips
{
    public double Amount { get; set; }
    
    public string Country { get; set; }

    public string Description { get; set; } = string.Empty;

    public double? CustomAmount { get; set; }
}