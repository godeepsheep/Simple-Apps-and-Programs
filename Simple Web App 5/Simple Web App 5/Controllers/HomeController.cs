using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Simple_Web_App_5.Models;

namespace Simple_Web_App_5.Controllers;

public class HomeController : Controller
{
    private static List<string> validUsers = new List<string> { "Sab", "Flemming", "Warwick" }; // Example usernames
    private static int _a, _b, _c, _correctAnswer;

    public IActionResult Index()
    {
        // Generate a new math problem
        Random rand = new Random();
        _a = rand.Next(1, 10);
        _b = rand.Next(1, 10);
        _c = rand.Next(1, 10);
        _correctAnswer = _a + _b * _c;

        ViewBag.MathQuestion = $"{_a} + {_b} * {_c} = ?";
        return View();
    }
   
    [HttpPost]
    public IActionResult Validate(UserModel user)
    {
        bool isUsernameValid = validUsers.Contains(user.Username);
        bool isCaptchaCorrect = user.Answer == _correctAnswer;
        
        if (isUsernameValid && isCaptchaCorrect)
        {
            return View("Success");
        }
        
        ViewBag.Error = "Invalid username or incorrect captcha.";

        // Generate a new problem again when failed
        Random rand = new Random();
        _a = rand.Next(1, 10);
        _b = rand.Next(1, 10);
        _c = rand.Next(1, 10);
        _correctAnswer = _a + _b * _c;

        ViewBag.MathQuestion = $"{_a} + {_b} * {_c} = ?";
        return View("Index");
    }

}