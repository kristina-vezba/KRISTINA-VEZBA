using BethanysPieShopAPI.Models;
using KRISTINA_VEZBA.Models;
using KRISTINA_VEZBA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KRISTINA_VEZBA.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _client;

        public CategoryController(HttpClient client)
        {
            _client = client;
            client.BaseAddress = new Uri("http://localhost:5002/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> ManageCategories()
        {
            List<Category> categories = new List<Category>();
            HttpResponseMessage response = await _client.GetAsync("api/categories");

            if (response.IsSuccessStatusCode)
            {
                categories = await response.Content.ReadAsAsync<List<Category>>();
            }

            return View(categories);  
        }

        public async Task<IActionResult> GetCategory(int id)
        {
            Category category = new Category();
            HttpResponseMessage response = await _client.GetAsync($"api/categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<Category>();
            }

            return View(category);
        }

        public ViewResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/categories", category);

            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<Category>();
            }

            return RedirectToAction("ManageCategories");
        }

        public async Task<IActionResult> UpdateCategory(int categoryId)
        {
            Category category = new Category();
            HttpResponseMessage response = await _client.GetAsync($"api/categories/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<Category>();
            }
            
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory( Category category)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/categories", category);

            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<Category>();
            }

            return RedirectToAction("ManageCategories");
        }
       
        public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
        {
            Category category = new Category();
            HttpResponseMessage response = await _client.DeleteAsync($"api/categories/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<Category>();
            }

            return RedirectToAction("ManageCategories");
        }
    }
}
