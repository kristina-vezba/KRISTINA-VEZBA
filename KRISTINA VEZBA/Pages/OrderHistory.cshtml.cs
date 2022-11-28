using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KRISTINA_VEZBA.Models;

namespace KRISTINA_VEZBA.Pages
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public OrderHistoryModel(IOrderRepository orderRepository)
        {
           _orderRepository = orderRepository;
        }

        public string UserId { get; set; }
        public Order Order { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string Username)
        {
            _orderRepository.GetAllOrders(Username);
            return Page();
        }
    }
}
