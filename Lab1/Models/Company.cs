using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Years in Business")]
        public int YearsInBusiness { get; set; }

        [Required]
        [Display(Name = "Website")]
        [Url]
        public string Website { get; set; } = string.Empty;

        [Display(Name = "Province")]
        public string? Province { get; set; }
    }
}