
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public  class CashBasketRepository (IBasketRepository basketRepository ,IDistributedCache cassh): IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            return await basketRepository.DeleteBasket(userName, cancellationToken);
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cashedBasket =await cassh.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cashedBasket))
            {
               return JsonSerializer.Deserialize<ShoppingCart>(cashedBasket);
            }
            var basket= await basketRepository.GetBasket(userName, cancellationToken); 
            await cassh.SetStringAsync(userName,JsonSerializer.Serialize(basket),cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasket(basket, cancellationToken);
            await cassh.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
    }
}
