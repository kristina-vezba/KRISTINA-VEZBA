using KRISTINA_VEZBA.Models;
using KRISTINA_VEZBA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KRISTINA_VEZBA.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }
        //public IActionResult List()
        //{
        // ViewBag.CurrentCategory = "Cheese cakes";
        // return View(_pieRepository.AllPies);
        // PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "All pies");
        //return View(pieListViewModel);
        //}
        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category).OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }
            return View(new PieListViewModel(pies, currentCategory));
        }
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }
        public IActionResult Search()
        {
            return View();
        }
        public ViewResult ManagePies()
        {
            IEnumerable<Pie> pies;

            pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
            return View(new ManagePiesViewModel(pies));
        }

        public IActionResult AddPie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPie(Pie pie)
        {
          
                _pieRepository.CreatePie(pie);
                return RedirectToAction("List");
        }

        public IActionResult EditPie(int pieId)
        { 
            var selectedPie = _pieRepository.GetPieById(pieId);
            if (selectedPie == null)
            {
                return NotFound();
            }
            return View(selectedPie);     
        }

        [HttpPost]
        public IActionResult EditPie(Pie pie)
        {
            _pieRepository.UpdatePie(pie);
            return RedirectToAction("List");
        }
         
        public IActionResult DeletePie(int pieId)
        {
            var selectedPie = _pieRepository.GetPieById(pieId);
            if (selectedPie == null) return NotFound();
            return View();
        }

        [HttpPost]
        public IActionResult DeletePie(Pie pie)
        {
            var selectedPie = _pieRepository.GetPieById(pie.PieId);
            if (selectedPie != null)
            {
                _pieRepository.RemovePie(selectedPie);
            }
            return RedirectToAction("List");
        }
    }
}
