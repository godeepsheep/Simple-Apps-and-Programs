using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
  [BindProperty]
  public string? Text { get; set; }
  
  [BindProperty]
  public int? Number { get; set; } //Set default value to 1 if you are short on time. It prevents issues that I had to deal with.
  
  [BindProperty]
  public bool AddSpace { get; set; } //if bind doesnt work add it manually in cshtml

  public string Result { get; set; } = string.Empty;

  public void OnPost()
  {
      if (string.IsNullOrEmpty(Text))
      {
          Result = "Enter some text.";
          return; //Important to do returns or it will continue to the next if statement. If both statements fail, it will only show the last error message. 
          //Return ensures the code stops if there is an error. 
      }

      if (Number == null || Number == 0)
      {
          Result = "Enter a positive number."; 
          return;
      }
      // Manually retrieve AddSpace from Request.Form
      bool addSpace = Request.Form.TryGetValue("AddSpace", out var spaceValue) && spaceValue == "true";
      
      // Debugging: Print AddSpace value
      Console.WriteLine($"Checkbox value: {AddSpace}");

      if (AddSpace)
      {
          Result = string.Join(" ", Enumerable.Repeat(Text, Number.Value)); //We use.Value because int? allows nullable. So we extract the value if there is one.
          //If it was just int, then you wouldn't need the .Value
          //If value is null then it will cause an error. You could avoid it by giving a value of 1 or 0 in your properties since 0 isn't null.
      }
      else
      {
          Result = string.Concat(Enumerable.Repeat(Text, Number.Value)); //Concat joins multiple strings together without seperator.
      }
  }

    public void OnGet()
    {
    }
}