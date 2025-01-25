using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
  [BindProperty]
  public string? Text { get; set; }
  
  [BindProperty]
  public int? Number { get; set; }
  
  [BindProperty]
  public bool AddSpace { get; set; }

   

    public void OnGet()
    {
    }
}