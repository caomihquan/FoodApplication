using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Basket.API.Models;

public class FoodCartItem
{
    public string FoodId { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string FoodName { get; set; } = default!;
}
