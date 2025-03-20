﻿
 
namespace Basket.API.Basket.GetBasket
{


    record GetBasketRequest(string UserName);
    record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);

            }).WithName("GetBaskt")
           .Produces<GetBasketResponse>(StatusCodes.Status201Created)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("GetBaskt Basket")
           .WithDescription("GetBaskt Basket");
        }
    }
}
