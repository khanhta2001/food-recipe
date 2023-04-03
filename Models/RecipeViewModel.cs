using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodRecipe.Models
{
    public class RecipeViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        
        [Required]
        public string? Title { get; set; }
        
        [Required]
        public string? Category { get; set; }
        
        [Required]
        public string? Content { get; set; }
        
        public string? DietaryRestriction { get; set; }
    }   
}