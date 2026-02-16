using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe.Checkout;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public CheckoutService(ICartRepository cartRepository,IOrderRepository orderRepository,
            UserManager<ApplicationUser> userManager, IEmailSender emailSender,IOrderItemRepository orderItemRepository,
            IProductRepository productRepository

)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public async Task<CheckoutResponse> ProcessPaymentAsync(CheckoutRequest request, string userId)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);

            if (!cartItems.Any())
            {
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "cart is empty"
                };
            }

            decimal totalAmount = 0;



            foreach (var cart in cartItems)
            {
                if (cart.Product.Quantity < cart.Count)
                {
                    return new CheckoutResponse
                    {
                        Success = false,
                        Message = "not enough stock"
                    };
                }

                totalAmount += cart.Product.Price * cart.Count;
            }


            Order order = new Order
            {
                UserId = userId,
                PaymentMethod = request.PaymentMethod,
                AmountPaid = totalAmount,
                PaymentStatus = PaymentStatusEnum.UnPaid

            };

            if (request.PaymentMethod == PaymentMethodEnum.Cash)
            {
                return new CheckoutResponse
                {
                    Success = true,
                    Message = "cash"
                };
            }


            else if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",
                    SuccessUrl = $"http://localhost:5162/checkout/success?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"http://localhost:5162/checkout/cancel",
                    Metadata = new Dictionary<string, string>
{
    { "UserId", userId },


},
                     LineItems = new List<SessionLineItemOptions>()

                };

                foreach (var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Translations
                                    .FirstOrDefault(t => t.Language == "en")?.Name
                            },
                            UnitAmount = (long)item.Product.Price * 100,
                        },
                        Quantity = item.Count,
                    });
                }

                var service = new SessionService();
                var session = service.Create(options);
                order.SessionId = session.Id;
                order.PaymentStatus = PaymentStatusEnum.Paid;

                await _orderRepository.CreateAsync(order);

                return new CheckoutResponse
                {
                    Success = true,
                    Message = "payment session created",
                    Url = session.Url
                };


            }

            else
            {
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "invalid payment method"
                };
            }



        }

        public async Task<CheckoutResponse> HandleSuccessAsync(string sessionId)
        {
            var service = new SessionService();
            var session = service.Get(sessionId);
            var userId = session.Metadata["UserId"];

            var order = await _orderRepository.GetBySessionIdAsync(sessionId);

            order.PaymentId = session.PaymentIntentId;
            order.OrderStatus = OrderStatusEnum.Approved;

            await _orderRepository.UpdateAsync(order);

            var user = await _userManager.FindByIdAsync(userId);

            var cartItems = await _cartRepository.GetUserCartAsync(userId);
            var orderItems = new List<OrderItem>();
            var productUpdated = new List<(int productId, int quantity)>();

            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    UnitPrice = cartItem.Product.Price,
                    Quantity = cartItem.Count,
                    TotalPrice = cartItem.Product.Price * cartItem.Count,
                };

                orderItems.Add(orderItem);
                productUpdated.Add((cartItem.ProductId, cartItem.Count));
            }

            await _orderItemRepository.CreateRangeAsync(orderItems);
            await _cartRepository.ClearCartAsync(userId);
            await _productRepository.DecreaseQuantitesAsync(productUpdated);
            await _emailSender.SendEmailAsync(
                user.Email,
                "Payment successful",
                "<h2>thank you ... </h2>");

            return new CheckoutResponse
            {
                Success = true,
                Message = "payment completed succesfully"
            };

        }

        public async Task<BaseResponse> UpdateOrderStatusAsync(int orderId, OrderStatusEnum newStatus)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            order.OrderStatus = newStatus;

            if (newStatus == OrderStatusEnum.Delivered)
            {
                order.PaymentStatus = PaymentStatusEnum.Paid;
            }

            //else if (newStatus == OrderStatusEnum.Cancelled)
            //{
            //    if (order.OrderStatus == OrderStatusEnum.Shipped)
            //    {
            //        return new BaseResponse
            //        {
            //            Success = false,
            //            Message = "can't cancelled this order"
            //        };
            //    }
            //}

            await _orderRepository.UpdateAsync(order);

            return new BaseResponse
            {
                Success = true,
                Message = "order status updated"
            };
        }


    }
}
