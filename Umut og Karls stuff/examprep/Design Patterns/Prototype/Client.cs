namespace Prototype;

public class Program
{
    public static void Main()
    {
        Prototype original = new Prototype("Alice", 30);
        original.PrintDetails();

        Prototype cloned = (Prototype)original.Clone();
        cloned.PrintDetails();

        // Modifying the cloned object doesn't affect the original
        cloned = new Prototype("Bob", 25);
        cloned.PrintDetails();
    }
}
