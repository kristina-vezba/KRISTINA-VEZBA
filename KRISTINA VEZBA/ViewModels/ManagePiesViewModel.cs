using KRISTINA_VEZBA.Models;
namespace KRISTINA_VEZBA.ViewModels
{
    public class ManagePiesViewModel
    {
        public IEnumerable<Pie> Pies { get; }
        //public string? CurrentCategory { get; }
        public ManagePiesViewModel(IEnumerable<Pie> pies)
        {
            Pies = pies;
            //CurrentCategory = currentCategory;
        }
    }
}
