namespace guesser2.Models
{
    public class Guesser
    {
        public int Guess { get; set; } 
        public bool IsOver { get; set; } = false;
        public int Guesses { get; set; }
        public int SecretNumber { get; set; }
    }
}
