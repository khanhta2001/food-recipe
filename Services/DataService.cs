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
using FoodRecipe.Models;

namespace FoodRecipe.Services
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
        
        public T? FindVariable<T>(string variableName, string collectionName, string findPart)
        {
            var variableCollection = _mongoDatabase.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq(findPart, variableName);
            var variable = variableCollection.Find(filter).FirstOrDefault();
            
            return variable;
        }
        
        public void AddModel<T>(T model, string collectionName)
        {
            var modelCollection = _mongoDatabase.GetCollection<T>(collectionName);

            modelCollection.InsertOne(model);
        }
        
        public void ChangeModel<T>(string variableName, string collectionName, string findPart, string changePart, string changeVariable)
        {
            var modelCollection = _mongoDatabase.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq(findPart, variableName);
            var update = Builders<T>.Update.Set(changePart, changeVariable);
            modelCollection.UpdateOne(filter, update);
        }
    }   
}