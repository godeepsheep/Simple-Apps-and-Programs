using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simple_Web_App_1_MVC.Models;

namespace Simple_Web_App_1_MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Validate(ValidationModel digits)
    {
        
        var errors = new List<string>();
        
        if (digits == null || string.IsNullOrWhiteSpace(digits.InputText))
        {
            errors.Add("Input text is required");
        }
        else
        {
            if (!IsWholeNumber(digits.InputText))
            {
                errors.Add("Input text must be a whole number");
            }
                
            if (HasRepeatableDigits(digits.InputText))
            {
                errors.Add("No digits should be repeated");
            }

            if (!HasAlternatingOddEvenDigits(digits.InputText))
            {
                errors.Add("Digits must alternate between odd and even");
            }
        }

        if (errors.Count == 0)
        {
            ViewBag.SuccessMessage = "Success! The input meets all the validation rules.";
        }
        else
        {
            ViewBag.Errors = errors;
        }
        
        return View("Index");
    }

    public bool IsWholeNumber(string inputText)
    {
        return inputText.All(char.IsDigit);
    }

    public bool HasRepeatableDigits(string inputText)
    {
        return inputText.Distinct().Count() != inputText.Length;
    }

    public bool HasAlternatingOddEvenDigits(string inputText)
    {
        for (int i = 0; i < inputText.Length - 1; i++)
        {
            //inputText[i] is a CHAR, since the input is a string. It means that each digit is stores as a char.
            //If we want to use mathematical operations like % modulus then we need to convert them to int first.
            //char
            int current = int.Parse(inputText[i].ToString()); //inputText[i] converts the CHAR to a String. int.Parse converts that string into an int.
            int next = int.Parse(inputText[i + 1].ToString());

            if ((current % 2) == (next % 2)) //% modulus gives the remainder if we divide by 2. 
            //Even numbers always have a remainder of 0 when divided by 2.
            //Odd numbers always have a remainder of 1 when divided by 2.
            //We check if the two numbers have the same remainder.
            //That means if both numbers are even the condition is TRUE same for odd numbers. Cause it will look like (0 == 0) or (1 == 1)
                return false;
        }
        return true;
    }
    
   
}