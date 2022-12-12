using KRISTINA_VEZBA.Models;
using Microsoft.AspNetCore.Mvc;


namespace KRISTINA_VEZBA.Components
{
    public class AdminMenu: ViewComponent
    {
        private readonly IPieRepository _pieRepository;
       
        public AdminMenu(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
           
        }   
        public IViewComponentResult Invoke()
        {
            var pies = _pieRepository.AllPies;
            return View(pies);
        }
    }
}
