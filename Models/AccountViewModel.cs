using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodRecipe.Models
{
    public class AccountViewModel
    {
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Summary { get; set; } = "Add it here!";
        public string? DateOfBirth { get; set; } = null;
    }    
}
