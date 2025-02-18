namespace FjernmetodekaldStub.MaskineA;

public class Caller
{
    private readonly Stub _stub;

    public Caller(Stub stub)
    {
        _stub = stub;
    }

    public void CallRemoteMethod()
    {
        // Kald en metode via stubben (fjerner netværkskompleksitet)
        var result = _stub.InvokeRemoteMethod(2, 2, "+");
        Console.WriteLine($"Result from remote method: {result}");
    }
}