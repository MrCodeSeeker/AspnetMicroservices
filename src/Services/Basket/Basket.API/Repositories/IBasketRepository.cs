using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShopingCart> GetBasket(string userName);
        Task<ShopingCart> UpdateBasket(ShopingCart basket);
        Task DeleteBasket(string userName);
    }
}
