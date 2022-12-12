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

        public async Task<ActionResult> ManageCategories()
        {
            List<Category> categories = null;
            HttpResponseMessage response = await _client.GetAsync("api/categories");
            if (response.IsSuccessStatusCode)
            {
                categories = await response.Content.ReadAsAsync<List<Category>>();
            }
            return View(categories);  
        }

        
        public async Task<ActionResult> GetCategory(int id)
        {
            Category category = new Category();

            HttpResponseMessage response = await _client.GetAsync("api/categories/{id}");
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
        public async Task<ActionResult> CreateCategory(Category category)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/categories", category);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("ManageCategories");
        }

        public async Task<ActionResult> UpdateCategory(int categoryId)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/categories/{categoryId}");
            response.EnsureSuccessStatusCode();
            Category category = response.Content.ReadAsAsync<Category>().Result;
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCategory( Category category)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/categories/{category.CategoryId}", category);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("ManageCategories");
        }

        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/categories/{categoryId}");
            return RedirectToAction("ManageCategories");
        }

        // ++++ Ova radi ++++
        //public async Task<IActionResult> ManageCategories()
        //{
        //    List<Category> categories = null;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:5002/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage Res = await client.GetAsync("api/categories/");
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var CatRsponse = Res.Content.ReadAsStringAsync().Result;
        //            categories = JsonConvert.DeserializeObject<List<Category>>(CatRsponse);
        //        }
        //        return View(categories);
        //    }
        //}







    }

}
