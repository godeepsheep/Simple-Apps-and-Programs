using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TipsCalculator.Models;

namespace TipsCalculator.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View(new Tips());
        }
        [HttpPost]
        public IActionResult Index(Tips model)
        {
            if (!ModelState.IsValid)
                return View(model);
            double procentage = 0;
            if (model.Country == "USA")
                procentage = 0.20;
            else if (model.Country == "Tyskland")
                procentage = 0.10;
            model.TipsDescription = $"amount: {model.Amount} land: {model.Country} " + 
                $"procent: {procentage * 100}% tips: {model.Amount * procentage} totalamount {model.Amount * (procentage + 1)}";
            return View(model);
        }

 
    }
}
