using Business.Abstract;
using Business.Utils;
using Entity.DTOs;
using Core.Entity.Temp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Concrete;
using Entity.Concrete;

namespace LoginSampleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;

        public ArticlesController(IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
        }

        [HttpPost("create")]
        [Authorize(Roles = $"{AuthorizationRoles.Admin},{AuthorizationRoles.Writer}")]
        public async Task<IActionResult> Create(ArticleDto articleDto)
        {
            var result = await _articleService.CreateAsync(articleDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = $"{AuthorizationRoles.Admin},{AuthorizationRoles.Writer}")]
        public async Task<IActionResult> Delete(int articleId)
        {
            var result = await _articleService.DeleteAsync(articleId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("update")]
        [Authorize(Roles = $"{AuthorizationRoles.Admin},{AuthorizationRoles.Writer}")]
        public async Task<IActionResult> Update(int articleId, ArticleDto updatedArticle)
        {
            var result = await _articleService.UpdateAsync(articleId, updatedArticle);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int articleId)
        {
            var result = await _articleService.GetAsync(articleId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int page = 0, int size = 10)
        {
            var result = await _articleService.GetAllAsync(page, size);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("addcategory")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> AddCategory(int articleId, int[] categoryIds)
        {
            var result = await _articleCategoryService.AddCategoryToArticleAsync(articleId, categoryIds.ToList());

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("removecategory")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> RemoveCategory(int articleId, int[] categoryIds)
        {
            var result = await _articleCategoryService.RemoveCategoryFromArticleAsync(articleId, categoryIds.ToList());

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
