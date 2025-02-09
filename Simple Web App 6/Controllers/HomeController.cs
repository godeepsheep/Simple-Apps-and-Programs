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

        if (checker == null || string.IsNullOrWhiteSpace(checker.Password))
        {
            errors.Add("Password cannot be empty.");
        }
        else
        {
            if (checker.Password.Length < 8)
                errors.Add("Password must be at least 8 characters long.");
            if (!checker.Password.Any(char.IsUpper))
                errors.Add("Password must contain at least one uppercase letter.");
            if (!checker.Password.Any(char.IsLower))
                errors.Add("Password must contain at least one lowercase letter.");
            if (!checker.Password.Any(char.IsDigit))
                errors.Add("Password must contain at least one number.");
            if (!checker.Password.Any(c => "!@#$%^&*()".Contains(c)))
                errors.Add("Password must contain at least one special character (!@#$%^&*).");
        }

        // If there are errors, send them all to the view
        if (errors.Count > 0)
        {
            ViewBag.Errors = errors;
            return View("Index"); // Reload the form with errors
        }

        // If no errors, show success message
        ViewBag.Success = "Password is valid!";
        return View("Index"); 
    }
    

}