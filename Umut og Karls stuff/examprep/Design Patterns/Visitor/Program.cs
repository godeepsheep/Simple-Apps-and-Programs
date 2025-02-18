namespace Visitor;

public class Program
{
    public static void Main()
    {
        List<IElement> elements = new List<IElement>
        {
            new ElementA(),
            new ElementB()
        };

        IVisitor visitor = new Visitor();

        // Perform operations on elements without modifying their structure
        foreach (var element in elements)
        {
            element.Accept(visitor);
        }
    }
}
