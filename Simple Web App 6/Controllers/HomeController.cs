using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Simple_Web_App_6.Models;

namespace Simple_Web_App_6.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Validate(CheckerModel checker)
    {
        var errors = new List<string>();
        
        if (String.IsNullOrWhiteSpace(checker.Password))
            errors.Add("Password cannot be empty");
        if(checker.Password.Length < 8)
            errors.Add("Password must be at least 8 characters");
        if(!checker.Password.Any(char.IsLower))
            errors.Add("Password must contain at least one lowercase letter");
        if (!checker.Password.Any(char.IsUpper))
            errors.Add("Password must contain at least one uppercase letter");
        if (!checker.Password.Any(char.IsDigit))
            errors.Add("Password must contain at least one digit");
        if (!checker.Password.Any(c => "!@#$%^&*()".Contains(c)))
            errors.Add("Password must contain at least one special character");
        
        ViewBag.Errors = errors;
       
        return View("Index");
    }
    
    

}