using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Simple_Web_App_1.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string? Number { get; set; }
    public string Message { get; set; } = string.Empty;

    public void OnPost()
    {
        if (string.IsNullOrEmpty(Number))
        {
            Message = "The input must have value.";
            return;
        }
        
        
        //out let's you return more than 1 value. otherwise you would need isValid for tryparsenumber and isValid converttoint.
        //Check if integer
        if (int.TryParse(Number, out int num))
        {
            var digits = Number.ToCharArray();
            
            //Check for repeatable digits
            if (HasRepeatableDigits(digits))
            {
                Message = "The Input must not contain repeatable digits";
                return;
            }
            
            //Check for alternating odd/even
            if (!HasAlternatingOddEvenDigits(digits))
            {
                Message = "The input must alternate between odd and even digits";
            }
            
            //If all pass
            Message = "Success";
        }

        else
        {
            Message = "The input must be a whole number";
        }
        
    }
    public void OnGet()
    {
    }

    public bool HasRepeatableDigits(char[] digits)
    {
        var ourDigits = new HashSet<char>(); //HashSet does not allow duplicate elements.
        foreach (var digit in digits)
        {
            if (ourDigits.Contains(digit))
                return true;
            ourDigits.Add(digit);
        }

        return false;
    }

    public bool HasAlternatingOddEvenDigits(char[] digits)
    {
        for (int i = 0; i < digits.Length - 1; i++)
        {
            int current = digits[i] - '0'; //Convert char to int
            int next = digits[i + 1] - '0';
            
            //Check if current and next digits alternate
            if ((current % 2 == 0 && next % 2 == 0) || (current % 2 != 0 && next % 2 != 0))
                return false;
        }

        return true;
    }
}
