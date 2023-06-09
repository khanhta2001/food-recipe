using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FoodRecipe.Models
{
    public class UserLikeRecipeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public string? UserId { get; set; }
        
        public List<string>? RecipeId { get; set; }
        
    }   
}