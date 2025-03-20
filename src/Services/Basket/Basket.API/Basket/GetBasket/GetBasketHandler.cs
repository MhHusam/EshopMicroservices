
using Basket.API.Data;

namespace Basket.API.Basket.GetBasket
{

    public record GetBasketResult(ShoppingCart Cart);
    public record GetBasketQuery (string userName):IQuery<GetBasketResult>;

    public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQuereyHandler<GetBasketQuery, GetBasketResult>

    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket=await basketRepository.GetBasket(request.userName);
            return new GetBasketResult(basket);
        }
    }
}
