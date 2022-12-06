namespace BethanysPieShopAPI.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        void DeleteCategory(Category category);
        Task<bool> SaveChangesAsync();
    }
}
