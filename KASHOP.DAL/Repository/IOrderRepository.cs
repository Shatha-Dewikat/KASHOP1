using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order request);

        Task<Order> GetBySessionIdAsync(string sessionId);

        Task<Order> UpdateAsync(Order order);

        Task<List<Order>> GetOrdersByStatusAsync(OrderStatusEnum status);

        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<bool> HasUserDelivredOrderForProduct(string userId, int productId);


    }

}
