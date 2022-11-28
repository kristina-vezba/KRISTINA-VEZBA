using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using KRISTINA_VEZBA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Logging;

namespace KRISTINA_VEZBA.Areas.Identity.Pages.Account.Manage
{
    public class OrderHistoryModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<OrderHistoryModel> _logger;
        private readonly IOrderRepository _orderRepository;
        


        public OrderHistoryModel(
            UserManager<IdentityUser> userManager,
            ILogger<OrderHistoryModel> logger,
            IOrderRepository orderRepository) 
        {
            _userManager = userManager;
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public string Username { get; set; }

        public string Id { get; set; }

        public Order Order { get; set; } = default!;

        public List<OrderDetail>? OrderDetails { get; set; }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            Username = userName;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
    }
}



