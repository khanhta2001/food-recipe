using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FoodRecipe.Models
{
    public class UserViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Verification { get; set; }
        
        public bool IsOwner { get; set; }
        
        public bool IsAdmin { get; set; }
    }   
}