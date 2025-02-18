using System.ComponentModel.DataAnnotations;

namespace TextRepeat.Models;

public class Repeat
{
    public string Tekst { get; set; }
    
    public int Antal { get; set; }
    
    public string? Result { get; set; }
}