using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repository
{
    public class CategoryRespository : ICategoryRespository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRespository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Create(Category Request)
        {
            _context.Add(Request);
            _context.SaveChanges();
            return Request;
        }

        public List<Category> GetAll()
        {
            return _context.categories.Include(c => c.Translations).ToList();
        }

        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.categories
                .Include(c => c.Translations)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task DeleteAsync(Category category)
        {
            _context.categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            _context.categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }


    }
}
