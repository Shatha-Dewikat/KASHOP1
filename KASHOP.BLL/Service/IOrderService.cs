using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetOrdersAsync(OrderStatusEnum status);

        Task<BaseResponse> UpdateOrderStatusAsync(int orderId, OrderStatusEnum newStatus);

        Task<Order?> GetOrderByIdAsync(int orderId);

    }
}
