namespace Prototype;

public interface IPrototype
{
    IPrototype Clone();
}

public class Prototype : IPrototype
{
    private string _name;
    private int _age;

    public Prototype(string name, int age)
    {
        _name = name;
        _age = age;
    }

    public IPrototype Clone()
    {
        // Perform a shallow copy by using the MemberwiseClone method
        return (IPrototype)this.MemberwiseClone();
    }

    public void PrintDetails()
    {
        Console.WriteLine($"Name: {_name}, Age: {_age}");
    }
}