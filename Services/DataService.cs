using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Net.Mail;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MongoDB.Bson;
using WebApp.Models;

namespace WebApp.Services
{
    public class DataService
    {
        private readonly IMongoDatabase _mongoDatabase;

        public DataService(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public List<RecipeViewModel> FindAllRecipes(string variableName, string collectionName)
        {
            var collection = _mongoDatabase.GetCollection<RecipeViewModel>(collectionName);
            
            var filter = Builders<RecipeViewModel>.Filter.Or(
                Builders<RecipeViewModel>.Filter.Eq("Title", new BsonRegularExpression(".*{variableName}.*", "i")), 
                          Builders<RecipeViewModel>.Filter.Eq("Category", new BsonRegularExpression(".*{variableName}.*", "i")),
                          Builders<RecipeViewModel>.Filter.Eq("Content", new BsonRegularExpression(".*{variableName}.*", "i")));
            
            var allRecipe = collection.Find(filter);
            
            return allRecipe.ToList();
        }
        
        public T? FindVariable<T>(string variableName, string collectionName)
        {
            var recipeCollection = _mongoDatabase.GetCollection<T>(collectionName);
            
            var filter = Builders<T>.Filter.Eq("Title", new BsonRegularExpression(".*{variableName}.*", "i"));
            
            var allRecipe = recipeCollection.Find(filter).FirstOrDefault();
            
            return allRecipe;
        }
    }   
}