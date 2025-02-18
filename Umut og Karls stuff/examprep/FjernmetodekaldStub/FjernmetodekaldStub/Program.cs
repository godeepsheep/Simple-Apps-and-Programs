using FjernmetodekaldStub.MaskineA;
using FjernmetodekaldStub.MaskineB;

namespace FjernmetodekaldStub;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Stubben på maskine A kommunikerer med Skeleton på maskine B
        var stub = new Stub("http://localhost:8080/");
        var caller = new Caller(stub);

        // Kalder remote-metoden
        caller.CallRemoteMethod();
        
        // Starter Skeleton (serveren)
        var skeleton = new Skeleton();
        skeleton.Start();
    }
}