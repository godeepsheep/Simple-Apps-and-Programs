using System.ComponentModel.DataAnnotations;

namespace TipsCalculator.Models
{
    public class Tips
    {
        public int Amount {  get; set; }
        [Required]
        public string Country {  get; set; } = string.Empty;
        public string TipsDescription {  get; set; } = string.Empty;
       
    }
}
