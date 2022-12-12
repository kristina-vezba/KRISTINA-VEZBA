using BethanysPieShopAPI.Models;
using KRISTINA_VEZBA.Models;

namespace KRISTINA_VEZBA.ViewModels
{
    public class ManageCategoriesViewModel
    {
        //public Task<IEnumerable<CategoryAPI>> Categories { get; }

        //public ManageCategoriesViewModel(Task<IEnumerable<CategoryAPI>> categories)
        //{
        //    Categories = categories;
        //}

        public IEnumerable<Category> Categories { get; }

        public ManageCategoriesViewModel(IEnumerable<Category> categories)
        {
            Categories = categories;
        }
    }
}
