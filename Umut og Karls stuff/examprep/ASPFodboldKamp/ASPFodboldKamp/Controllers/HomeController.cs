using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPFodboldKamp.Models;

namespace ASPFodboldKamp.Controllers;

public class HomeController : Controller
{
    private static int teamAScore = 0;
    private static int teamBScore = 0;

    public IActionResult Index()
    {
        ViewBag.TeamAScore = teamAScore;
        ViewBag.TeamBScore = teamBScore;
        return View();
    }

    [HttpPost]
    public IActionResult ScoreGoal(string team)
    {
        if (team == "A") teamAScore++;
        if (team == "B") teamBScore++;

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ResetMatch()
    {
        teamAScore = 0;
        teamBScore = 0;
        return RedirectToAction("Index");
    }
}