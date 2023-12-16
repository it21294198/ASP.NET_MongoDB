using System;
using MongoDB.Driver;

namespace ASP.NET_MongoDB.Util
{
	public class MongoDBContext
	{

        private readonly IMongoDatabase _database;

        public MongoDBContext()
        {

            //var connectionString = $"mongodb://mongo:{Environment.GetEnvironmentVariable("DB_PASSWORD")}@monorail.proxy.rlwy.net:52075";
            var connectionString = $"mongodb://{Environment.GetEnvironmentVariable("DB_NAME")}:{Environment.GetEnvironmentVariable("DB_PASSWORD")}@monorail.proxy.rlwy.net:{Environment.GetEnvironmentVariable("DB_PORT")}";

            var client = new MongoClient(connectionString);

            _database = client.GetDatabase("test");
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

    }
}
