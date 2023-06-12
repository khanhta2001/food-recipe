using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FoodRecipe.Models
{
    public class ReviewViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int ReviewId { get; set; }
        [Required]
        public string? Review { get; set; }
    }   
}