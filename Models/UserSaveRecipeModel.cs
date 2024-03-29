﻿using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FoodRecipe.Models
{
    public class UserSaveRecipeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        
        public string? UserId { get; set; }
        
        public List<string>? RecipeId { get; set; }
        
    }   
}