using BethanysPieShopAPI.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAPI.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public CategoryRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext ?? throw new ArgumentNullException(
                nameof(bethanysPieShopDbContext));
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _bethanysPieShopDbContext.Categories.OrderBy(c => c.CategoryName).ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(int categoryId)
        {
            return await _bethanysPieShopDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
             _bethanysPieShopDbContext.Categories.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            _bethanysPieShopDbContext.Remove(category);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _bethanysPieShopDbContext.SaveChangesAsync() >= 0);
        }
    }
}
