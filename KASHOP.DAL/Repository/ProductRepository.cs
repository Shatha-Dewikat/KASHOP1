using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
            _context = context;
        }

        public async Task<Product> AddAsync(Product request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }
    }
}
