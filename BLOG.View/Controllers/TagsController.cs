namespace BLOG.View.Controllers
{
    using BLOG.Dtos.Tag;
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _service;

        public TagsController(ITagsService service)
        {
            this._service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            Result<List<TagDto>> res = await _service.List();
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }
    }
}
