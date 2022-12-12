namespace BethanysPieShopAPI.Models
{
    public interface ICategoryRepositoryAPI
    {
        Task<IEnumerable<CategoryAPI>> GetAllCategoriesAsync();
        Task<CategoryAPI?> GetCategoryAsync(int categoryId);
        Task AddCategoryAsync(CategoryAPI category);
        Task UpdateCategory(CategoryAPI category);
        void DeleteCategory(CategoryAPI category);
        Task<bool> SaveChangesAsync();
    }
}
