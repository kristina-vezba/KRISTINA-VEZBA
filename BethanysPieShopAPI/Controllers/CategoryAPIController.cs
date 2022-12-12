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
        private readonly IMapper _mapper;

        public CategoryAPIController(ICategoryRepositoryAPI categoryRepositoryApi, IMapper mapper)
        {
            _categoryRepositoryApi = categoryRepositoryApi ?? 
                throw new ArgumentNullException(nameof(categoryRepositoryApi));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryAPI>>> GetAllCategories()
        {
            var categories = await _categoryRepositoryApi.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryAPI>> GetCategory(int id)
        {
            var category = await _categoryRepositoryApi.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryAPI>> CreateCategory(CategoryAPI category)
        {
            await _categoryRepositoryApi.AddCategoryAsync(category);
            await _categoryRepositoryApi.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult> UpdateCategory(int categoryId, CategoryAPI category)
        {
            //var categoryEntity = await _categoryRepositoryApi.GetCategoryAsync(categoryId);
            //if (categoryEntity == null)
            //{
            //    return NotFound();
            //}

            //_mapper.Map(category, categoryEntity);
            await _categoryRepositoryApi.UpdateCategory(category);

            await _categoryRepositoryApi.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var categoryEntity = await _categoryRepositoryApi.GetCategoryAsync(categoryId);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _categoryRepositoryApi.DeleteCategory(categoryEntity);
            await _categoryRepositoryApi.SaveChangesAsync();

            return NoContent();
        }



        //[HttpPatch]
        //public async Task<ActionResult> PartiallyUpdateCategory(int categoryId, 
        //    JsonPatchDocument<CategoryAPI> patchDocument)
        //{
        //    var categoryEntity = await _categoryRepositoryApi.GetCategoryAsync(categoryId);
        //    if (categoryEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    var categoryToPatch = _mapper.Map<CategoryAPI>(categoryEntity);

        //    patchDocument.ApplyTo(categoryToPatch, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (!TryValidateModel(categoryToPatch))
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _mapper.Map(categoryToPatch, categoryEntity);
        //    await _categoryRepositoryApi.SaveChangesAsync();

        //    return NoContent();
        //}

    }

}
