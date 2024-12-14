
namespace BasketAPI.Basket.GetBasket;
public record GetBasketResponse(FoodCart Cart);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            var respose = result.Adapt<GetBasketResponse>();
            return Results.Ok(respose);
        })
        .WithName("GetCardByUserName")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Card By UserName")
        .WithDescription("Get Card By UserName");
    }
}
