using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GuesserGameASP.Models;

namespace GuesserGameASP.Controllers;

public class HomeController : Controller
{
    private static Random random = new Random();
    private static int targetNumber;


    [HttpGet]
    public IActionResult Index()
    {
        targetNumber = random.Next(1, 10);
  
        return View(new GuessModel());
    }
    
    [HttpPost]
    public IActionResult Guess(GuessModel model)
    {
        if (model.GuessAmount == 0)
        {
            model.Message = "Ingen gæt tilbage!";
            model.TargetNumber = targetNumber; // Vis det rigtige nummer
            return View("Index", model);
        }

        // Giv feedback baseret på gættet
        if (model.Guess < targetNumber)
        {
            model.Message = "Lavt! Prøv igen.";
        }
        else if (model.Guess > targetNumber)
        {
            model.Message = "Højt! Prøv igen.";
        }
        else
        {
            model.Message = "Korrekt! Du gættede nummeret!";
        }

        model.GuessAmount--; // Reducer antal gæt
        return View("Index", model);
    }
}