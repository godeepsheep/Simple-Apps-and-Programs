using Microsoft.AspNetCore.Mvc;
using NyVersionRestaurantASPNET.Models;
using System.Collections.Generic;
using System.Linq;

namespace NyVersionRestaurantASPNET.Controllers;

public class HomeController : Controller
{
    public IActionResult Bestil()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Bestil(int alder, string ret)
    {
        return RedirectToAction("Index", new { alder = alder, ret = ret });
    }

    public IActionResult Index(int alder, string ret)
    {
        var menu = new List<Vare>
        {
            new Vare {Navn = "Pasta", Pris = 90},
            new Vare {Navn = "Pizza", Pris = 70},
            new Vare {Navn = "Ris", Pris = 50}
        };
        var valgret = menu.FirstOrDefault(v => v.Navn == ret);

        if (valgret != null)
        {
            var pris = valgret.BeregnPris(alder);
            
            ViewData["Pris"] = pris; // Gem den beregnede pris
            ViewData["Ret"] = valgret.Navn;
            return View("Bekræftelse"); // Korrekt visning
        }
        
        return View();
    }

    [HttpGet]
    public IActionResult Bekræftelse()
    {
        return View();
    }
}