using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }
       

        public async Task<ShopingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if(string.IsNullOrEmpty(basket))  return null;
            return JsonConvert.DeserializeObject<ShopingCart>(basket);
        }

        public async Task<ShopingCart> UpdateBasket(ShopingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
         await _redisCache.RemoveAsync(userName);
        } 
    }
}
