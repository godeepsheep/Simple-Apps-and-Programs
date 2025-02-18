using System;
using System.Diagnostics;
using guesser2.Models;
using Microsoft.AspNetCore.Mvc;

namespace guesser2.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            
            return View(new Guesser() {SecretNumber = GenerateNumber()});
        } 
        [HttpPost]
        public IActionResult Index(Guesser model)
        {
            model.Guesses +=1;
            if (model.SecretNumber == model.Guess)
                return View("Succes", model);
            if (model.Guesses >= 5)
                return Index();
            model.IsOver = model.Guess > model.SecretNumber;
            model.Guess = 0;
            return View(model); // Return updated model to the view


        }


        public int GenerateNumber()
        {
            var rand = new Random();
            return rand.Next(1, 10); // Generates random 
        }
    }
}
