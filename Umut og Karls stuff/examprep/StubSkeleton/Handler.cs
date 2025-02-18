public class Handler
{
    public static string Execute(string method, List<string> args)
    {
        switch (method)
        {
            case "Reverse":
                char[] characters = args[0].ToCharArray();
                Array.Reverse(characters);
                return new string(characters);
            case "Concat":
                return string.Join("", args);
            default:
                return "none";
        }
    }
}