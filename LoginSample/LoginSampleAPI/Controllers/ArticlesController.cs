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
        public IActionResult Create(ArticleDto articleDto)
        {
            var result = _articleService.Create(articleDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = $"{AuthorizationRoles.Admin},{AuthorizationRoles.Writer}")]
        public IActionResult Delete(int articleId)
        {
            var result = _articleService.Delete(articleId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("update")]
        [Authorize(Roles = $"{AuthorizationRoles.Admin},{AuthorizationRoles.Writer}")]
        public IActionResult Update(int articleId,ArticleDto updatedArticle)
        {
            var result = _articleService.Update(articleId, updatedArticle);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        [AllowAnonymous]
        public IActionResult Get(int articleId)
        {
            var result = _articleService.Get(articleId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("getall")]
        [AllowAnonymous]
        public IActionResult GetAll(int page = 0, int size = 10)
        {
            var result = _articleService.GetAll(page,size);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
