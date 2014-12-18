using System.ComponentModel.DataAnnotations;
using CoreFramework4;

namespace jclitenet.Models
{
    public class ContactModels
    {
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(AppConstants.EMAILREGEX, ErrorMessage = "Invalid Email Address!")]
        public string Email { get; set; }

        [Required(ErrorMessage="Invalid Message!")]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}