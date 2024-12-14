using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Basket.API.Models;

public class FoodCart
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string UserName { get; set; } = default!;
    public List<FoodCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public FoodCart(string userName)
    {
        UserName = userName;
    }
    public FoodCart()
    {
    }
}
