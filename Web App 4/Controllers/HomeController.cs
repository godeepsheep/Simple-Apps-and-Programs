using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Web_App_4.Models;

namespace Web_App_4.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new Tips());
    }

    [HttpPost]
    public IActionResult Index(Tips model, string submitButton)
    {

        if (!ModelState.IsValid)

            return View(model);

        double percentage = 0;

        if (submitButton == "CustomAmount")
        {
            if (model.CustomAmount.HasValue)
            {
                Console.WriteLine("CustomAmount");
                percentage = model.CustomAmount.Value / 100;
            }

        }
        else if (!string.IsNullOrEmpty(submitButton)) 
        {
                model.Country = submitButton; // Store the selected country
                if (model.Country == "USA")
                    percentage = 0.20;
                else if (model.Country == "Germany")
                    percentage = 0.10;
                else if (model.Country == "Japan")
                    percentage = 0.00;
        }
        
        
                
        model.Description =
            $"Total Amount {model.Amount * (percentage + 1)}"; //$ makes you able make expressions inside a string without format. 
        

        return View(model);
    } 
   
    
}