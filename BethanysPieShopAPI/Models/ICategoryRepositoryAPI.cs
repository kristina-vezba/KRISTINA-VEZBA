namespace BethanysPieShopAPI.Models
{
    public interface ICategoryRepositoryAPI
    {
        Task<IEnumerable<CategoryAPI>> GetAllCategoriesAsync();
        Task<CategoryAPI?> GetCategoryAsync(int categoryId);
        Task AddCategoryAsync(CategoryAPI category);
        Task UpdateCategory(CategoryAPI category);
        Task DeleteCategory(CategoryAPI category);    
    }
}
