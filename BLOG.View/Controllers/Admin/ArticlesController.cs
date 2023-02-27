namespace BLOG.View.Controllers.Admin
{
    using BLOG.Dtos.Article;
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService _service;

        public ArticlesController(IArticlesService service)
        {
            this._service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(CreateOrUpdateArticleDto createDto)
        {
            Result<ArticleDto> res = await _service.Create(createDto);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<IActionResult> Put(CreateOrUpdateArticleDto updateDto, int id)
        {
            Result<ArticleDto> res = await _service.Update(updateDto, id);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpGet]
        [Route("{slug}")]
        public async Task<IActionResult> Get(string slug)
        {
            Result<ArticleDto> res = await _service.Get(slug);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("pagination/{pageNumber}")]
        public async Task<IActionResult> GetPaginated(int pageNumber)
        {
            Result<AdminPaginationArticlesDto> res = await this._service.ListWithPagination(pageNumber);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }
    }
}