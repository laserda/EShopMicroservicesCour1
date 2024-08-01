using Mapster;

namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);
public class GetBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGet("/basket/{Username}", async (string Username, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(Username));
            var response =  result.Adapt<GetBasketResponse>();

            return Results.Ok(response);

        })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Basket")
            .WithDescription("Get Basket"); 
    }
}

