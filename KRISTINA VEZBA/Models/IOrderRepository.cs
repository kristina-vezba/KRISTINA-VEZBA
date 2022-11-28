namespace KRISTINA_VEZBA.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Order? GetOrderById(int orderId);
        List<Order> GetAllOrders(string Email); 
    }
}
