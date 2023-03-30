using System.ComponentModel.DataAnnotations;


namespace FoodRecipe.Models
{
    public class ReviewViewModel
    {
        [Required]
        public string? Review { get; set; }
    }   
}