namespace Basket.API.Entities
{
    public class ShopingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public ShopingCart()
        {

        }
        public ShopingCart(string username)
        {
            UserName= username;
        }
        public decimal TotalPrice { 
            get {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice = item.Price*item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
