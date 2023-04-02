using System.ComponentModel.DataAnnotations;


namespace FoodRecipe.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "New Password")]
        public string? NewPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword")]
        public string? NewPasswordConfirm { get; set; }
    }   
}