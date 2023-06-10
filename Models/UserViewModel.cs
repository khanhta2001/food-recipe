using System.ComponentModel.DataAnnotations;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FoodRecipe.Models
{
    public class UserViewModel : MongoUser
    {
        public string? Password { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAdmin { get; set; }
    }   
}