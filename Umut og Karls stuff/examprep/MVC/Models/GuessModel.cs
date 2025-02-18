namespace MVC.Models;

public class GuessModel {
    public int? Number { get; set; }
    public bool AllowGuesses { get; set; }
    public bool CorrectAnswer { get; set; }
    public bool Above { get; set; }
    public bool Below { get; set; }
    public int AnswersLeft { get; set; }
}
