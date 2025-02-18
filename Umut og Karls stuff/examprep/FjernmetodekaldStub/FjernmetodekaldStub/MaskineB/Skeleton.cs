namespace FjernmetodekaldStub.MaskineB;

// Maskine B - Skeleton.cs
using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

// Maskine B - Skeleton.cs
using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

public class Skeleton
{
    private readonly CalledObject _calledObject;

    public Skeleton()
    {
        _calledObject = new CalledObject();
    }

    public void Start()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/"); // Lytter på http://localhost:8080/
        listener.Start();

        Console.WriteLine("Server started...");
        while (true)
        {
            var context = listener.GetContext();
            ProcessRequest(context);
        }
    }

    private void ProcessRequest(HttpListenerContext context)
    {
        // Læs XML fra Stubben
        string xmlRequest;
        using (var reader = new StreamReader(context.Request.InputStream))
        {
            xmlRequest = reader.ReadToEnd();
        }

        // Deserialiser XML til et regnestykke
        string result = InvokeRemoteMethodFromXml(xmlRequest);

        // Send resultatet tilbage til Stubben
        byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(result);
        context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
        context.Response.Close();
    }

    private string InvokeRemoteMethodFromXml(string xml)
    {
        var xmlSerializer = new XmlSerializer(typeof(ArithmeticRequest));
        using (var reader = new StringReader(xml))
        {
            var request = (ArithmeticRequest)xmlSerializer.Deserialize(reader);

            // Kald den rigtige metode på CalledObject
            return _calledObject.PerformArithmetic(request.Num1, request.Num2, request.Operator);
        }
    }
}

public class ArithmeticRequest
{
    public int Num1 { get; set; }
    public int Num2 { get; set; }
    public string Operator { get; set; }
}
