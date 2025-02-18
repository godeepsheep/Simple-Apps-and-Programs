using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Website.Pages;

public class LogoutModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public LogoutModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        HttpContext.Session.Remove("username");
    }

    public void OnPost()
    {
    }
}
