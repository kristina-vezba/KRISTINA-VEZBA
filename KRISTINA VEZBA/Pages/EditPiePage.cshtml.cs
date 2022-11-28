using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KRISTINA_VEZBA.Models;

namespace KRISTINA_VEZBA.Pages
{
    public class EditPiePageModel : PageModel
    {
        private readonly IPieRepository _pieRepository;

        public EditPiePageModel(IPieRepository pieRepository)
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
            _pieRepository.UpdatePie(Pie);
            return RedirectToPage("List");
        }
    }
}