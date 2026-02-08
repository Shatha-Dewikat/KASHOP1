using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart request);
        Task<List<Cart>> GetUserCartAsync(string userId);
        Task<Cart?> GetCartItemAsync(string userId, int productId);
        Task<Cart> UpdateAsync(Cart cart);
        Task ClearCartAsync(string userId);

        Task DeleteAsync(Cart cart);

    }
}
