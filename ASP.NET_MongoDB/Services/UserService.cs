using System;
using ASP.NET_MongoDB.Models;
using ASP.NET_MongoDB.Util;
using MongoDB.Driver;

namespace ASP.NET_MongoDB.Services
{
	public class UserService
	{

		public void CreateUser()
		{
            var context = new MongoDBContext();
            var collection = context.GetCollection<User>("User");

            int fakeId = DateTime.Now.Millisecond % 10000;
            User document = new User(fakeId, "Hello", "world");

            collection.InsertOne(document);
        }

        public User GetUser(int id)
        {
            var context = new MongoDBContext();
            var collection = context.GetCollection<User>("User");

            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return collection.Find(filter).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            var context = new MongoDBContext();
            var collection = context.GetCollection<User>("User");

            var filter = Builders<User>.Filter.Empty; // Empty filter for retrieving all documents
            var usersCursor = collection.FindSync(filter);

            return usersCursor.ToList();
        }

        public void UpdateUser(int userId, User updatedUser)
        {
            var context = new MongoDBContext();
            var collection = context.GetCollection<User>("User");

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update
                .Set(u => u.Name, updatedUser.Name)
                .Set(u => u.Email, updatedUser.Email);

            collection.UpdateOne(filter, update);
        }

        // Delete User by Id
        public void DeleteUser(int userId)
        {
            var context = new MongoDBContext();
            var collection = context.GetCollection<User>("User");

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            collection.DeleteOne(filter);
        }

    }
}

