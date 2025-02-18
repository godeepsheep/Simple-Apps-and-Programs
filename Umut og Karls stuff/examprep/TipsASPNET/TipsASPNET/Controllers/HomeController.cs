using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TipsASPNET.Models;

namespace TipsASPNET.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(new TipsModel());
    }

    public IActionResult Beregning(TipsModel model)
    {
        if (model.Land == "USA")
        {
            model.TotalBeløb = model.Beløb + (model.Beløb * 0.20);
        }
        else if (model.Land == "Tyskland")
        {
            model.TotalBeløb = model.Beløb + (model.Beløb * 0.10);
        }
        else
        {
            model.TotalBeløb = model.Beløb;
        }
        return View("Index", model);
    }
}