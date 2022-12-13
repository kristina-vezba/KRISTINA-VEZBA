using BethanysPieShopAPI.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAPI.Models
{
    public class CategoryRepositoryAPI : ICategoryRepositoryAPI
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public CategoryRepositoryAPI(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext ?? throw new ArgumentNullException(
                nameof(bethanysPieShopDbContext));
        }

        public async Task<IEnumerable<CategoryAPI>> GetAllCategoriesAsync()
        {
            return await _bethanysPieShopDbContext.Categories.OrderBy(c => c.CategoryName).ToListAsync();
        }

        public async Task<CategoryAPI?> GetCategoryAsync(int categoryId)
        {
            return await _bethanysPieShopDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task AddCategoryAsync(CategoryAPI category)
        {
            _bethanysPieShopDbContext.Categories.Add(category);
            await _bethanysPieShopDbContext.SaveChangesAsync();
        }
        public async Task UpdateCategory(CategoryAPI category)
        {
            _bethanysPieShopDbContext.Categories.Update(category);
            await _bethanysPieShopDbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(CategoryAPI category)
        {
            _bethanysPieShopDbContext.Remove(category);
             await _bethanysPieShopDbContext.SaveChangesAsync();
        }
    }
}
