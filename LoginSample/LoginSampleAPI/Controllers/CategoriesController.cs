using Business.Abstract;
using Business.Utils;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSampleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Category category)
        {
            var result = await _categoryService.CreateAsync(category);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var result = await _categoryService.DeleteAsync(categoryId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[HttpPatch("update")]
        //public async Task<IActionResult> Update(int categoryId, Category newCategory)
        //{
        //    var result = await _categoryService.UpdateAsync(categoryId, newCategory);

        //    if (!result.Success)
        //        return BadRequest(result);

        //    return Ok(result);
        //}

        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int categoryId)
        {
            var result = await _categoryService.GetByIdAsync(categoryId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}