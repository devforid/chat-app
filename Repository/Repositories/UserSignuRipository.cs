using MongoDB.Driver;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class UserSignuRipository
    {
        private IMongoDatabase _db;
        public UserSignuRipository()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            _db = client.GetDatabase("UserDB");
        }

        public Users Get(string id)
        {
            return _db.GetCollection<Users>("Users").Find(u => u.Id == id).FirstOrDefault();
        } 
        public Users Insert(Users User)
        {
            try
            {
                _db.GetCollection<Users>("Users").InsertOne(User);
                return User;
            }
            catch
            {
                Console.WriteLine("Exception caught");
                return null;
            }
        }
        public Users Update(Users user)
        {
            try
            {
                var update = Builders<Users>.Update
                    .Set(u => u.isLoggedin, user.isLoggedin);
                var updateOption = new UpdateOptions { IsUpsert = true };
                _db.GetCollection<Users>("Users").UpdateOne(u => u.Id == user.Id, update, updateOption);
                return user;
            }
            catch
            {
                Console.WriteLine("Exception Caught");
                return null;
            }
        }
        public List<Users> GetLoggedInUsers()
        {
            return _db.GetCollection<Users>("Users").Find(u => u.isLoggedin == true).ToList();
        }
    }
}
