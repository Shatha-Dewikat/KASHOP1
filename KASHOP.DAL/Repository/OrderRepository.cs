using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order Request)
        {
            await _context.AddAsync(Request);
            await _context.SaveChangesAsync();
            return Request;
        }
        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetOrdersByStatusAsync(OrderStatusEnum status)
        {
            return await _context.Orders
                .Where(o => o.OrderStatus == status)
                .Include(o => o.User)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrederItems)
                    .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<bool> HasUserDelivredOrderForProduct(string userId, int productId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId && o.OrderStatus == OrderStatusEnum.Delivered)
                .SelectMany(o => o.OrederItems)
                .AnyAsync(oi => oi.ProductId == productId);
        }

        public Task<Order> GetBySessionIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }
    }

}
