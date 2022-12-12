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

        public Task AddCategoryAsync(CategoryAPI category)
        {
            _bethanysPieShopDbContext.Categories.Add(category);
            return Task.CompletedTask;
        }
        public Task UpdateCategory(CategoryAPI category)
        {
            _bethanysPieShopDbContext.Categories.Update(category);
            return Task.CompletedTask;
        }

        public void DeleteCategory(CategoryAPI category)
        {
            _bethanysPieShopDbContext.Remove(category);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _bethanysPieShopDbContext.SaveChangesAsync() >= 0);
        }
    }
}
