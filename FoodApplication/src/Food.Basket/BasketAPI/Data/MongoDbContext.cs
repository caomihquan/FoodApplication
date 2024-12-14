using MongoDB.Driver;

namespace BasketAPI.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<FoodCart> FoodCarts => _database.GetCollection<FoodCart>("FoodCarts");
    }
}
