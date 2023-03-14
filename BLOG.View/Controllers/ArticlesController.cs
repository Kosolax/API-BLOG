namespace BLOG.View.Controllers
{
    using BLOG.Dtos.Article;
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService _service;

        public ArticlesController(IArticlesService service)
        {
            this._service = service;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetPaginatedWithSearchAndTags([FromBody] PostViewerPaginationRequest postViewerPaginationRequest)
        {
            if (postViewerPaginationRequest == null || !this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            Result<ViewerPaginationArticlesDto> res = await _service.ListWithPaginationWithSearchAndTags(postViewerPaginationRequest.PageNumber, postViewerPaginationRequest.Search, postViewerPaginationRequest.TagsId);
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
    }
}