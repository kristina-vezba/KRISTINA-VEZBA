using AutoMapper;
using BethanysPieShopAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;
using System.Runtime.InteropServices;

namespace BethanysPieShopAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryAPIController : ControllerBase
    {
        private readonly ICategoryRepositoryAPI _categoryRepositoryApi;

        public CategoryAPIController(ICategoryRepositoryAPI categoryRepositoryApi)
        {
            _categoryRepositoryApi = categoryRepositoryApi ?? 
                throw new ArgumentNullException(nameof(categoryRepositoryApi));
        }

        [ProducesResponseType(typeof(List<CategoryAPI>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepositoryApi.GetAllCategoriesAsync();

            return Ok(categories);
        }

        [ProducesResponseType(typeof(CategoryAPI), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var category = await _categoryRepositoryApi.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [ProducesResponseType(typeof(CategoryAPI), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryAPI category)
        {
            await _categoryRepositoryApi.AddCategoryAsync(category);

            return Ok();
        }

        [ProducesResponseType(typeof(CategoryAPI), StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryAPI category)
        {
             await _categoryRepositoryApi.UpdateCategory(category);

            return Ok();
        }

        [ProducesResponseType(typeof(CategoryAPI), StatusCodes.Status200OK)]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var categoryEntity = await _categoryRepositoryApi.GetCategoryAsync(categoryId);

            if (categoryEntity == null)
            {
                return NotFound();
            }

            await _categoryRepositoryApi.DeleteCategory(categoryEntity);
            
            return Ok();
        }
    }
}
