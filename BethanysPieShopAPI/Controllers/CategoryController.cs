using AutoMapper;
using BethanysPieShopAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BethanysPieShopAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? 
                throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync(); 
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            await _categoryRepository.AddCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return Ok(category);
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult> UpdateCategory(int categoryId, Category category)
        {
            var categoryEntity = await _categoryRepository.GetCategoryAsync(categoryId);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(category, categoryEntity);

            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult> PartiallyUpdateCategory(int categoryId, 
            JsonPatchDocument<Category> patchDocument)
        {
            var categoryEntity = await _categoryRepository.GetCategoryAsync(categoryId);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            var categoryToPatch = _mapper.Map<Category>(categoryEntity);

            patchDocument.ApplyTo(categoryToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(categoryToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(categoryToPatch, categoryEntity);
            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var categoryEntity = await _categoryRepository.GetCategoryAsync(categoryId);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _categoryRepository.DeleteCategory(categoryEntity);
            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
