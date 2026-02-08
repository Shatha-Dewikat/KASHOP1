using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public interface IReviewRepository
    {
        Task<bool> HasUserReviewdProduct(string userId, int productId);
        Task<Review> CreateAsync(Review request);

    }

}
