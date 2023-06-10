using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodRecipe.Models
{
    public class VerificationViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        public string? OTP { get; set; }
        public string? Verification { get; set; }
    }
}