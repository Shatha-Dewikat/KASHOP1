using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.Service
{
    public interface IReviewService
    {
        Task<BaseResponse> AddReviewAsync(string userId, int productId, CreateReviewRequest request);

    }
}
