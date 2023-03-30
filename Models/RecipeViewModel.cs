


using System.ComponentModel.DataAnnotations;

namespace FoodRecipe.Models
{
    public class RecipeViewModel
    {
        [Required]
        public string? Title { get; set; }
        
        [Required]
        public string? Category { get; set; }
        
        [Required]
        public string? Content { get; set; }
        
        public string? DietaryRestriction { get; set; }
    }   
}