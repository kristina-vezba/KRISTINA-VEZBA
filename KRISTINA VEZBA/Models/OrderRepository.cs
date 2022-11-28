
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KRISTINA_VEZBA.Models

{
    public class OrderRepository : IOrderRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        private readonly IShoppingCart _shoppingCart;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderRepository(BethanysPieShopDbContext bethanysPieShopDbContext, IShoppingCart shoppingCart, UserManager<IdentityUser> userManager)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }

        public void CreateOrder(Order order)
        {
           order.OrderPlaced = DateTime.Now;
            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            order.OrderDetails = new List<OrderDetail>();
            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            _bethanysPieShopDbContext.Orders.Add(order);
            _bethanysPieShopDbContext.SaveChanges();
        }

        public Order? GetOrderById(int orderId)
        {
            return _bethanysPieShopDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IdentityUser? GetUserById(string userId)
        {
            var user = _bethanysPieShopDbContext.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        public List<Order> GetAllOrders(string Username)
        {
            var orders = _bethanysPieShopDbContext.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Pie).Where(o => o.Email == Username).ToList();
            return orders;
            
        }
    }
}
