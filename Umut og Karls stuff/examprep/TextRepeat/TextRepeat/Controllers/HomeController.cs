using Microsoft.AspNetCore.Mvc;
using TextRepeat.Models;

namespace TextRepeat.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult TextRepeat(Repeat model)
    {
        if (ModelState.IsValid)
        {
            // Generer resultatet
            var result = "";
            var mellemrum = Request.Form["Mellemrum"].Count > 0;

            for (var i = 0; i < model.Antal; i++)
            {
                result += model.Tekst;
                if (mellemrum && i < model.Antal - 1)
                {
                    result += " ";
                }
            }

            model.Result = result.Trim();

            return View("Index", model); // Returnér til Index View med resultat
        }

        return View("Index", model); // Returnér til Index View ved valideringsfejl
    }
}