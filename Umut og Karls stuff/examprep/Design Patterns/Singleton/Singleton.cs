namespace Singleton;

public class Singleton
{
    private static Singleton? _instance;
    private string _message = "Hello, I am a Singleton instance!";

    // Private constructor to prevent external instantiation
    private Singleton() { }

    // Method to access the Singleton instance
    public static Singleton GetInstance()
    {
        _instance ??= new Singleton();
        return _instance;
    }

    // Method to demonstrate the Singleton functionality
    public void DisplayMessage()
    {
        Console.WriteLine(_message);
    }
}