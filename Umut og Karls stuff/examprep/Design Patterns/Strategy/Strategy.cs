namespace Strategy;

public interface IStrategy
{
    void Execute();
}

public class StrategyA : IStrategy
{
    public void Execute()
    {
        Console.WriteLine("Executing strategy A.");
    }
}

public class StrategyB : IStrategy
{
    public void Execute()
    {
        Console.WriteLine("Executing strategy B.");
    }
}