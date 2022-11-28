using KRISTINA_VEZBA.Models;

namespace KRISTINA_VEZBA.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel(IShoppingCart shoppingCart, decimal shoppingCartTotal)
        {
            ShoppingCart = shoppingCart;
            ShoppingCartTotal = shoppingCartTotal;
        }
        public IShoppingCart ShoppingCart { get; }
        public decimal ShoppingCartTotal { get; }  
    }
}
