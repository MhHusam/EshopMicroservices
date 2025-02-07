
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{

   // public record GetProdictRequest()
    public record GetProdictRespone(IEnumerable<Product> Products);

    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = result.Adapt<GetProdictRespone>();

                return Results.Ok(response);
            }).WithName("GetProducts")
            .Produces<CreateProdictResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Product")

                ;
             
        }
    }
}
