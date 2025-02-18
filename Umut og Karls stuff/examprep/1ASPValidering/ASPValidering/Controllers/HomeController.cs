using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPValidering.Models;

namespace ASPValidering.Controllers
{
    public class HomeController : Controller
    {
        private HashSet<int> _receivedNumbers = new HashSet<int>();

        public IActionResult Index()
        {
            return View(new ValiViewModel());
        }

        [HttpPost]
        public IActionResult Vali(ValiViewModel model)
        {
            // Tjek om inputtet kan konverteres til et heltal
            if (!int.TryParse(model.Tal, out int value))
            {
                model.Message = "Input skal være hele tal";
            }
            if (_receivedNumbers.Contains(value))
            {
                model.Message = "Tallet er set før!";
            }
            if (_receivedNumbers.Count > 0)
            {
                if (_receivedNumbers.Last() % 2 == value % 2)
                {
                    model.Message= "Tallet skal skifte mellem være lige og ulige";
                }
            }
            _receivedNumbers.Add(value);

            return View("Index", model);
        }
    }
}