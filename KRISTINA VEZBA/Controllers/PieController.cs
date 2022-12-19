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
            {
                return NotFound();
            }

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

        public ViewResult AddPie()
        {
            List<Category> categories;
            categories = _categoryRepository.AllCategories.ToList();
            AddEditPieViewModel vm = new AddEditPieViewModel();
            vm.Categories = categories;

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddPie(AddEditPieViewModel pie)
        {
            Pie p = new Pie()
            {
                PieId = pie.PieId,
                Name = pie.Name,
                ShortDescription = pie.ShortDescription,
                LongDescription = pie.LongDescription,
                AllergyInformation = pie.AllergyInformation,
                Price = pie.Price,
                ImageUrl = pie.ImageUrl,
                ImageThumbnailUrl = pie.ImageThumbnailUrl,
                IsPieOfTheWeek = pie.IsPieOfTheWeek,
                InStock = pie.InStock,
                CategoryId = pie.CategoryId,
                Category = pie.Category
            };

            _pieRepository.CreatePie(p);

            return RedirectToAction("List");
        }

        public IActionResult EditPie(int pieId)
        {
            var selectedPie = _pieRepository.GetPieById(pieId);

            if (selectedPie == null)
            {
                return NotFound();
            }

            List<Category> categories;
            categories = _categoryRepository.AllCategories.ToList();
           
            AddEditPieViewModel vm = new AddEditPieViewModel
            {
                PieId = pieId,
                Name = selectedPie.Name,
                ShortDescription = selectedPie.ShortDescription,
                LongDescription = selectedPie.LongDescription,
                AllergyInformation = selectedPie.AllergyInformation,
                Price = selectedPie.Price,
                ImageUrl = selectedPie.ImageUrl,
                ImageThumbnailUrl = selectedPie.ImageThumbnailUrl,
                IsPieOfTheWeek = selectedPie.IsPieOfTheWeek,
                InStock = selectedPie.InStock,
                CategoryId = selectedPie.CategoryId,
                Category = selectedPie.Category,
                Categories = categories
            };

            return View(vm);     
        }

        [HttpPost]
        public IActionResult EditPie(AddEditPieViewModel pie)
        {
            Pie p = new Pie()
            {
                PieId = pie.PieId,
                Name = pie.Name,
                ShortDescription = pie.ShortDescription,
                LongDescription = pie.LongDescription,
                AllergyInformation = pie.AllergyInformation,
                Price = pie.Price,
                ImageUrl = pie.ImageUrl,
                ImageThumbnailUrl = pie.ImageThumbnailUrl,
                IsPieOfTheWeek = pie.IsPieOfTheWeek,
                InStock = pie.InStock,
                CategoryId = pie.CategoryId,
                Category = pie.Category
            };

            _pieRepository.UpdatePie(p);

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
