using Basket.API.Data;
using MongoDB.Driver;
using StackExchange.Redis;

namespace BasketAPI.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly MongoDbContext dbContext;
        public BasketRepository(MongoDbContext FoodCarts)
        {
            dbContext = FoodCarts;
        }

        public async Task<bool> AddItemQuantityBasket(string userName, string FoodId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<FoodCart>.Filter.Eq(u => u.UserName, userName) &
                 Builders<FoodCart>.Filter.ElemMatch(cart => cart.Items, Builders<FoodCartItem>.Filter.Eq(item => item.FoodId, FoodId));
            var update = Builders<FoodCart>.Update.Inc(cart => cart.Items[-1].Quantity, 1);
            var options = new UpdateOptions { IsUpsert = false };
            await dbContext.FoodCarts.UpdateOneAsync(filter, update, options, cancellationToken);
            return true;
        }

        public async Task<bool> AddItemToBasket(string userName, FoodCartItem foodCartItem, CancellationToken cancellationToken = default)
        {
            var filter = Builders<FoodCart>.Filter.Eq(u => u.UserName, userName);
            var update = Builders<FoodCart>.Update.AddToSet(cart => cart.Items,foodCartItem);
            var options = new UpdateOptions { IsUpsert = false };
            await dbContext.FoodCarts.UpdateOneAsync(filter, update, options, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            var deleteFilter = Builders<FoodCart>.Filter.Eq(u => u.UserName, "userName");
            await dbContext.FoodCarts.DeleteOneAsync(deleteFilter);
            return true;
        }

        public async Task<FoodCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var filter = Builders<FoodCart>.Filter.Eq(fc => fc.UserName, userName);
            var existingCart = await dbContext.FoodCarts.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return existingCart;
        }

        public async Task<bool> RemoveItemBasket(string userName, string FoodId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<FoodCart>.Filter.Eq(u => u.UserName, userName);
            var update = Builders<FoodCart>.Update.PullFilter(cart => cart.Items,Builders<FoodCartItem>.Filter.Eq(item => item.FoodId, FoodId));
            var options = new UpdateOptions { IsUpsert = false };
            await dbContext.FoodCarts.UpdateOneAsync(filter, update, options, cancellationToken);
            return true;
        }

        public async Task<bool> RemoveItemQuantityBasket(string userName, string FoodId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<FoodCart>.Filter.Eq(u => u.UserName, userName) &
                Builders<FoodCart>.Filter.ElemMatch(cart => cart.Items, Builders<FoodCartItem>.Filter.Eq(item => item.FoodId, FoodId));
            var update = Builders<FoodCart>.Update.Inc(cart => cart.Items[-1].Quantity, -1);
            var options = new UpdateOptions { IsUpsert = false };
            await dbContext.FoodCarts.UpdateOneAsync(filter, update, options, cancellationToken);
            return true;
        }

        public async Task<FoodCart> StoreBasket(FoodCart basket, CancellationToken cancellationToken = default)
        {
            var filter = Builders<FoodCart>.Filter.Eq(fc => fc.UserName, basket.UserName);
            var existingCart = await dbContext.FoodCarts.Find(filter).FirstOrDefaultAsync(cancellationToken);
            if (existingCart == null)
            {
                await dbContext.FoodCarts.InsertOneAsync(basket, cancellationToken: cancellationToken);
                return basket;
            }
            return basket;
        }
    }
}
