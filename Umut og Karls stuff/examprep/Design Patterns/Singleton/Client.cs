namespace Singleton;

public class Program
{
    public static void Main()
    {
        Singleton instance1 = Singleton.GetInstance();
        instance1.DisplayMessage();

        Singleton instance2 = Singleton.GetInstance();
        instance2.DisplayMessage();

        // Both instances point to the same Singleton object
        Console.WriteLine($"Are both instances the same? {object.ReferenceEquals(instance1, instance2)}");
    }
}
