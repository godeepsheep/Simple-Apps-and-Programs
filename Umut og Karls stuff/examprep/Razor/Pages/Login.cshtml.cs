using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Website.Pages;

public class LoginModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public LoginModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [TempData]
    public string ErrorMessage { get; set; } = "";

    public void OnGet()
    {
    }

    public ActionResult OnPost()
    {
        string? username = Request.Form["username"];
        string? password = Request.Form["password"];

        if (username == "admin" && password == "test") {
            HttpContext.Session.SetString("username", username);
            return RedirectToPage("/Index");
        } else {
            ErrorMessage = "Invalid username and/or password match";
            return RedirectToPage("/Login");
        }
    }
}
