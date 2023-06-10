using AspNetCore.Identity.Mongo.Model;


namespace FoodRecipe.Models
{
    public class UserViewModel : MongoUser
    {
        public string? Password { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAdmin { get; set; }
    }   
}