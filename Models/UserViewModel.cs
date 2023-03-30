using System.ComponentModel.DataAnnotations;


namespace FoodRecipe.Models
{
    public class UserViewModel
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Verified { get; set; }
    }   
}