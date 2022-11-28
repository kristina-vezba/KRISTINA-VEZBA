using KRISTINA_VEZBA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KRISTINA_VEZBA.Pages
{
    public class AddPiePageModel : PageModel
    {
        private readonly IPieRepository _pieRepository;

        public AddPiePageModel(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [BindProperty]
        public Pie Pie { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
             _pieRepository.CreatePie(Pie);
             return RedirectToPage("List");
        }
    }
}
