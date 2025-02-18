namespace GuesserGameASP.Models;

public class GuessModel
{
    public int Guess { get; set; }
    public string Message { get; set; }
    public int TargetNumber { get; set; }

    public int GuessAmount { get; set; } = 3;

}