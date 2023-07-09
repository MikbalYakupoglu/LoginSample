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
        public IActionResult Create(Category category)
        {
            var result = _categoryService.Create(category);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int categoryId)
        {
            var result = _categoryService.Delete(categoryId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("update")]
        public IActionResult Update(int categoryId, Category newCategory)
        {
            var result = _categoryService.Update(categoryId, newCategory);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        public IActionResult Get(int categoryId)
        {
            var result = _categoryService.GetById(categoryId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}