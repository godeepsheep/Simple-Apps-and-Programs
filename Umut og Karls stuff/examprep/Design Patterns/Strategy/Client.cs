namespace Strategy;

public class Program
{
    public static void Main()
    {
        // Creating strategies
        IStrategy strategyA = new StrategyA();
        IStrategy strategyB = new StrategyB();

        // Creating context with default strategy
        Context context = new Context(strategyA);

        // Executing default strategy
        context.ExecuteStrategy(); // Outputs: Executing strategy A.

        // Changing strategy at runtime
        context.SetStrategy(strategyB);
        context.ExecuteStrategy(); // Outputs: Executing strategy B.
    }
}
