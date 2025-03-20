

using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{

    public record GetProductsQuery(int? pageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    public class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger) : IQuereyHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler with {@Query}", query);

           var products=await session.Query<Product>().ToPagedListAsync(query.pageNumber?? 1 ,query.PageSize ??10,cancellationToken);

            return new GetProductResult(products);
        }
    }
}
