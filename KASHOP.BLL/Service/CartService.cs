using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartService(IProductRepository productRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }


        public async Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);

            if (product is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "product not found"
                };
            }

            var cartItem = await _cartRepository.GetCartItemAsync(userId, request.ProductId);


            var existingCount = cartItem?.Count ?? 0;

            if (product.Quantity < request.Count)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Not enough stock"
                };
            }

            

            if (cartItem is not null)
            {
                cartItem.Count += request.Count;
                await _cartRepository.UpdateAsync(cartItem);
            }
            else
            {
                var cart = request.Adapt<Cart>();
                cart.UserId = userId;

                await _cartRepository.CreateAsync(cart);
            }


            return new BaseResponse
            {
                Success = true,
                Message = "Product Added successfully"
            };


        }

        public async Task<BaseResponse> RemoveFromCartAsync(string userId, int productId)
        {
            var cartItem = await _cartRepository.GetCartItemAsync(userId, productId);

            if (cartItem is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "cart item not found"
                };
            }

            await _cartRepository.DeleteAsync(cartItem);

            return new BaseResponse
            {
                Success = true,
                Message = "item removed from cart"
            };
        }


        public async Task<CartSummaryResponse> GetUserCartAsync(string userId, string lang = "en")
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);

            //var response = cartItems.Adapt

            var response = cartItems.Adapt<CartResponse>();

            var items = cartItems.Select(c => new CartResponse
            {
                ProductId = c.ProductId,
                ProductName = c.Product.Translations.FirstOrDefault(t => t.Language == lang).Name,
                Count = c.Count,
                Price = c.Product.Price,
            }).ToList();

            return new CartSummaryResponse
            {
                Items = items,
            };
        }

        public async Task<BaseResponse> ClearCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);

            return new BaseResponse
            {
                Success = true,
                Message = "cart cleared successfully"
            };
        }


    }
}
