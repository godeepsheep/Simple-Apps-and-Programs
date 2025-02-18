using Microsoft.AspNetCore.Mvc;

namespace LoginRegnestykkeASPNET.Models;

public class LoginModel
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public int Answer { get; set; }
    public string? Succes { get; set; }
    
    public int num1 { get; set; }
    public int num2 { get; set; }
    public int num3 { get; set; }
    
    [BindProperty] 
    public int CorrectAnswer { get; set; }
}