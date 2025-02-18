namespace FjernmetodekaldStub.MaskineA;

public class Stub
{
    private readonly string _serverUrl;

    public Stub(string serverUrl)
    {
        _serverUrl = serverUrl;
    }

    public async Task<string> InvokeRemoteMethod(int num1, int num2, string operation)
    {
        var xmlPayload = $"<request><num1>{num1}</num1><num2>{num2}</num2><operator>{operation}</operator></request>";

        
        using (var client = new HttpClient())
        {
            
            // Send XML til serveren (Skeleton på maskine B)
            var content = new StringContent(xmlPayload, System.Text.Encoding.UTF8, "application/xml");
            var response = await client.PostAsync(_serverUrl, content);
            response.EnsureSuccessStatusCode();
            
            // Læs svaret fra serveren (Skeleton)
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        
        }
    }
}