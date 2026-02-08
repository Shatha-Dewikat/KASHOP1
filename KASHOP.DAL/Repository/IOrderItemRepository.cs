using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public interface IOrderItemRepository
    {
        Task CreateRangeAsync(List<OrderItem> orderItems);
    }
}
