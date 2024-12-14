namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<FoodCart> GetBasket(string userName, CancellationToken cancellationToken = default);
    Task<bool> RemoveItemBasket(string userName, string FoodId, CancellationToken cancellationToken = default);
    Task<bool> AddItemToBasket(string userName,FoodCartItem foodCartItem, CancellationToken cancellationToken = default);
    Task<bool> RemoveItemQuantityBasket(string userName, string FoodId, CancellationToken cancellationToken = default);
    Task<bool> AddItemQuantityBasket(string userName, string FoodId, CancellationToken cancellationToken = default);
    Task<FoodCart> StoreBasket(FoodCart basket, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
