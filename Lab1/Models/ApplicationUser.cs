using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Lab1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "City")]
        public string? City { get; set; }
    }
}