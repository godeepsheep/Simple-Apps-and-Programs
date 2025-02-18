namespace FjernmetodekaldStub.MaskineB;

public class CalledObject
{
    public string PerformArithmetic(int num1, int num2, string operatorType)
    {
        var result = 0;
        
        // Udfør operationen baseret på operatoren
        switch (operatorType)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                else
                {
                    return "Error: Division by zero!";
                }
                break;
            default:
                return "Error: Unknown operator!";
        }
        
        return $"Result: {result}";
    }
}
