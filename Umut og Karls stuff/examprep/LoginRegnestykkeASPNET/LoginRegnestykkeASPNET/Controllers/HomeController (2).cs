using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginRegnestykkeASPNET.Models;

namespace LoginRegnestykkeASPNET.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index(LoginModel model)
    {
        if (model.CorrectAnswer == 0)
        {
            var random = new Random();
            model.num1 = random.Next(1, 9);
            model.num2 = random.Next(1, 9);
            model.num3 = random.Next(1, 9);
            model.CorrectAnswer = model.num1 + (model.num2 * model.num3);
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (model.Username == "admin" && model.Password == "123" && model.CorrectAnswer == model.Answer)
        {
            model.Succes = "Login successful";
        }
        else
        {
            model.Succes = "Wrong username, password or math";
        }

        return View("Succes", model);
    }
}
    
