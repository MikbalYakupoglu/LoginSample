using Business.Abstract;
using Business.Utils;
using Entity.DTOs;
using Core.Entity.Temp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSampleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
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

    }
}
